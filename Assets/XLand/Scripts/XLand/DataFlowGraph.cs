using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.Assertions;

namespace XLand
{
    public class DataFlowGraph
    {
        public readonly List<Node> nodes = new List<Node>();

        public void LoadData(DataFlowGraphData data)
        {
            try
            {
                nodes.Clear();
                foreach (var nodeData in data.nodes)
                {
                    nodes.Add(Node.CreateFromData(nodeData));
                }

                foreach (var edgeData in data.edges)
                {
                    var fromNode = nodes[edgeData.fromIndex];
                    var toNode = nodes[edgeData.toIndex];
                    var fromField = fromNode.GetType().GetField(edgeData.fromField);
                    var toField = toNode.GetType().GetField(edgeData.toField);
                    if (fromField == null || toField == null) continue;
                    AddEdge(fromNode, fromField, toNode, toField);
                }
            }
            catch (Exception e)
            {
                nodes.Clear();
            }
        }

        public DataFlowGraphData GetData()
        {
            var data = new DataFlowGraphData {nodes = new List<NodeData>(), edges = new List<EdgeData>()};
            foreach (var n in nodes)
            {
                data.nodes.Add(n.GetData());
            }

            foreach (var n in nodes)
            {
                foreach (var e in n.inputEdges)
                {
                    data.edges.Add(new EdgeData
                    {
                        fromIndex = nodes.IndexOf(e.FromNode),
                        fromField = e.FromField.Name,
                        toIndex = nodes.IndexOf(e.ToNode),
                        toField = e.ToField.Name
                    });
                }
            }

            return data;
        }

        public Node AddNode(NodeInfo node)
        {
            var n = Activator.CreateInstance(node.type) as Node;
            nodes.Add(n);
            return n;
        }

        public void RemoveNode(Node node)
        {
            foreach (var e in node.inputEdges.ToArray())
            {
                RemoveEdge(e);
            }

            foreach (var e in node.outputEdges.ToArray())
            {
                RemoveEdge(e);
            }

            nodes.Remove(node);
            node.OnDeleted();
        }

        private void AddEdge(Edge e)
        {
            Assert.IsTrue(nodes.Contains(e.FromNode) && nodes.Contains(e.ToNode));
            e.FromNode.AddOutputEdge(e);
            e.ToNode.AddInputEdge(e);
        }

        public void AddEdge(Node fromNode, FieldInfo fromField, Node toNode, FieldInfo toField)
        {
            AddEdge(new Edge(fromNode, fromField, toNode, toField));
        }

        private void RemoveEdge(Edge e)
        {
            Assert.IsTrue(nodes.Contains(e.FromNode) && nodes.Contains(e.ToNode));
            e.FromNode.RemoveOutputEdge(e);
            e.ToNode.RemoveInputEdge(e);
        }

        public void RemoveEdge(Node fromNode, FieldInfo fromField, Node toNode, FieldInfo toField)
        {
            RemoveEdge(new Edge(fromNode, fromField, toNode, toField));
        }

        public void Evaluate()
        {
            var toEvaluate = new Queue<Node>();
            foreach (var n in nodes)
            {
                var inCnt = n.inputEdges.Count;
                n.unEvaluatedInDegree = inCnt;
                if (inCnt == 0)
                {
                    toEvaluate.Enqueue(n);
                }
            }

            while (toEvaluate.Count != 0)
            {
                var n = toEvaluate.Dequeue();
                n.Evaluate();
                foreach (var e in n.outputEdges)
                {
                    e.PassValue();
                    var to = e.ToNode;
                    to.unEvaluatedInDegree -= 1;
                    if (to.unEvaluatedInDegree == 0)
                    {
                        toEvaluate.Enqueue(to);
                    }
                }
            }
        }
    }
}