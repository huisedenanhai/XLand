using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

namespace XLand
{
    internal static class SaveSupportedFieldType
    {
        public static readonly Type[] Types =
        {
            typeof(float), typeof(string)
        };
    }

    public abstract class Node
    {
        public Vector2 position;

        public readonly List<Edge> inputEdges = new List<Edge>();
        public readonly List<Edge> outputEdges = new List<Edge>();

        public int unEvaluatedInDegree;

        public static Node CreateFromData(NodeData data)
        {
            var nodeInfo = GetNodeInfoByName(data.type);
            Assert.IsNotNull(nodeInfo);
            var node = Activator.CreateInstance(nodeInfo.type) as Node;
            node.position.x = data.position.x;
            node.position.y = data.position.y;
            foreach (var f in data.fields)
            {
                var fieldName = f.Key;
                var valueString = f.Value;
                var field = nodeInfo.type.GetField(fieldName);
                if (field == null) continue;
                if (field.FieldType == typeof(float))
                {
                    float value;
                    if (float.TryParse(valueString, out value))
                    {
                        field.SetValue(node, value);
                    }
                }
                else if (field.FieldType == typeof(string))
                {
                    field.SetValue(node, valueString);
                }
                else if (field.FieldType.IsEnum)
                {
                    try
                    {
                        field.SetValue(node, Enum.Parse(field.FieldType, valueString));
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }
            }

            return node;
        }

        public bool IsConnectedInputField(string fieldName)
        {
            foreach (var e in inputEdges)
            {
                if (e.ToField.Name == fieldName) return true;
            }

            return false;
        }

        private void SaveField(NodeData data, FieldInfo field)
        {
            if (SaveSupportedFieldType.Types.Any(type => type == field.FieldType))
            {
                data.fields.Add(field.Name, field.GetValue(this).ToString());
            }
            else if (field.FieldType.IsEnum)
            {
                data.fields.Add(field.Name, field.GetValue(this).ToString());
            }
        }

        public NodeData GetData()
        {
            var data = new NodeData();
            var type = GetType();
            data.type = type.FullName;
            data.fields = new Dictionary<string, string>();
            data.position = new Vector2Data
            {
                x = position.x, y = position.y
            };
            foreach (var field in type.GetFields())
            {
                var attrs = field.GetCustomAttributes(typeof(MarkAsNodeField), true);
                if (attrs.Length == 0) continue;
                var attr = attrs[0] as MarkAsNodeField;
                var fieldName = field.Name;
                switch (attr.Type)
                {
                    case NodeFieldType.Input:
                        if (!IsConnectedInputField(fieldName)) SaveField(data, field);
                        break;
                    case NodeFieldType.Output:
                        break;
                    case NodeFieldType.Attribute:
                        SaveField(data, field);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return data;
        }

        private static void AddEdge(List<Edge> edges, Edge edge)
        {
            foreach (var e in edges)
            {
                if (e.Equals(edge)) return;
            }

            edges.Add(edge);
        }

        private static void RemoveEdge(List<Edge> edges, Edge edge)
        {
            edges.RemoveAll(e => e.Equals(edge));
        }

        public void AddInputEdge(Edge edge)
        {
            AddEdge(inputEdges, edge);
        }

        public void RemoveInputEdge(Edge edge)
        {
            RemoveEdge(inputEdges, edge);
        }

        public void AddOutputEdge(Edge edge)
        {
            AddEdge(outputEdges, edge);
        }

        public void RemoveOutputEdge(Edge edge)
        {
            RemoveEdge(outputEdges, edge);
        }

        private static Dictionary<string, NodeInfo> m_NodeInfos;

        private static void FetchNodeInfoDict()
        {
            var childClazz = typeof(Node).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Node)));
            m_NodeInfos = new Dictionary<string, NodeInfo>();
            foreach (var type in childClazz)
            {
                m_NodeInfos.Add(type.FullName, new NodeInfo(type));
            }
        }

        public virtual void OnDeleted()
        {
        }

        public static List<NodeInfo> NodeInfos
        {
            get
            {
                if (m_NodeInfos == null)
                {
                    FetchNodeInfoDict();
                }

                return m_NodeInfos.Values.ToList();
            }
        }

        public static NodeInfo GetNodeInfoByName(string name)
        {
            if (m_NodeInfos == null) FetchNodeInfoDict();
            NodeInfo info;
            m_NodeInfos.TryGetValue(name, out info);
            return info;
        }

        public abstract void Evaluate();
    }
}