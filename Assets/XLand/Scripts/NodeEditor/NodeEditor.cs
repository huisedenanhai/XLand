using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace XLand
{
    public class NodeEditor : MonoBehaviour
    {
        [SerializeField] private Text m_CurrentParamNameLabel;
        [SerializeField] private Transform m_GraphContent;
        [SerializeField] private Transform m_NodesParent;
        [SerializeField] private Transform m_EdgesParent;
        [SerializeField] private NodeEntryFactory m_NodeEntryFactory;
        private IParamHandler m_CurrentParamHandler;
        private DataFlowGraph m_CurrentDataFlowGraph;

        public Transform NodesParent
        {
            get { return m_NodesParent; }
        }

        public Transform EdgesParent
        {
            get { return m_EdgesParent; }
        }

        private void Awake()
        {
            // make current param name invalid when active model changed 
            ModelManager.Instance.CurrentActiveModelChangedEvent += name => CurrentParamHandler = null;
        }

        private void LoadNewGraph()
        {
            if (CurrentParamHandler == null)
            {
                m_CurrentDataFlowGraph = null;
                return;
            }

            // fetch new DataFlowGraph
            var model = ModelManager.Instance.CurrentActiveModel;
            if (model == null)
            {
                m_CurrentDataFlowGraph = null;
                return;
            }

            var xLander = model.GetComponent<XLander>();
            Assert.IsNotNull(xLander);
            m_CurrentDataFlowGraph = xLander.GetGraph(CurrentParamHandler.Name);
        }

        private DataFlowGraph CurrentDataFlowGraph
        {
            get
            {
                if (m_CurrentDataFlowGraph == null) LoadNewGraph();
                return m_CurrentDataFlowGraph;
            }
        }

        private bool m_NeedRefresh;

        public IParamHandler CurrentParamHandler
        {
            get { return m_CurrentParamHandler; }
            set
            {
                if (m_CurrentParamHandler == value) return;
                m_CurrentParamHandler = value;
                m_CurrentParamNameLabel.text = value == null ? "Non Param Selected" : value.Name;
                m_CurrentDataFlowGraph = null; // require graph reload
                NeedRefresh();
            }
        }

        private NodeEntry AddNodeDisplay(NodeInfo nodeInfo, Node node, bool newlyCreated = false)
        {
            var entry = m_NodeEntryFactory.CreateNodeEntry(nodeInfo, node);
            var t = entry.transform;
            t.SetParent(NodesParent);
            if (newlyCreated)
            {
                var position = m_GraphContent.position;
                t.position = position;
                node.position = NodesParent.InverseTransformPoint(position);
            }
            else
            {
                t.localPosition = node.position;
            }

            t.localScale = Vector3.one;
            return entry;
        }

        public void AddNode(NodeInfo nodeInfo)
        {
            var node = CurrentDataFlowGraph.AddNode(nodeInfo);
            AddNodeDisplay(nodeInfo, node, true);
        }

        public void RemoveNode(NodeEntry entry)
        {
            CurrentDataFlowGraph.RemoveNode(entry.NodeInstance);
            Destroy(entry.gameObject);
        }

        public void AddEdge(UIControlledNodeField from, UIControlledNodeField to)
        {
            CurrentDataFlowGraph.AddEdge(from.NodeInstance, from.FieldInfo, to.NodeInstance, to.FieldInfo);
        }

        public void RemoveEdge(UIControlledNodeField from, UIControlledNodeField to)
        {
            CurrentDataFlowGraph.RemoveEdge(from.NodeInstance, from.FieldInfo, to.NodeInstance, to.FieldInfo);
        }

        private void NeedRefresh()
        {
            m_NeedRefresh = true;
        }

        private void ClearContent()
        {
            m_EdgesParent.DestroyAllChildren();
            m_NodesParent.DestroyAllChildren();
        }

        private IEnumerator RefreshGraphCoroutine()
        {
            ClearContent();
            var entries = new List<NodeEntry>();
            foreach (var node in CurrentDataFlowGraph.nodes)
            {
                entries.Add(AddNodeDisplay(Node.GetNodeInfoByName(node.GetType().FullName), node));
            }

            yield return null;

            foreach (var toEntry in entries)
            {
                foreach (var edge in toEntry.NodeInstance.inputEdges)
                {
                    foreach (var fromEntry in entries)
                    {
                        if (edge.FromNode != fromEntry.NodeInstance) continue;
                        var fromKnob = fromEntry.GetKnobByName(edge.FromField.Name);
                        var toKnob = toEntry.GetKnobByName(edge.ToField.Name);
                        var connection = Instantiate(m_NodeEntryFactory.connectionPrefab, m_EdgesParent, false);
                        fromKnob.AddConnection(connection);
                        toKnob.AddConnection(connection);
                        connection.transform.position = Vector3.zero;
                        connection.transform.localScale = Vector3.one;
                        connection.InputKnob = fromKnob;
                        connection.OutputKnob = toKnob;
                    }
                }
            }
        }

        private void RefreshGraph()
        {
            StartCoroutine(RefreshGraphCoroutine());
        }

        private void LateUpdate()
        {
            if (m_NeedRefresh)
            {
                RefreshGraph();
                m_NeedRefresh = false;
            }
        }
    }
}