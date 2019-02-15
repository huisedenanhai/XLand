using System.Reflection;
using UnityEngine;

namespace XLand
{
    public class NodeEntryFactory : MonoBehaviour
    {
        [SerializeField] private NodeEntry m_NodeEntryPrefab;
        [SerializeField] private UIControlledNodeField m_InputFieldPrefab;
        [SerializeField] private UIControlledNodeField m_AttributeFieldPrefab;
        [SerializeField] private UIControlledNodeField m_OutputFieldPrefab;
        public Connection connectionPrefab;

        private void AddField(NodeEntry entry, UIControlledNodeField prefab, NodeInfo nodeInfo,
            FieldInfo fieldInfo, Node nodeInstance)
        {
            var field = Instantiate(prefab);
            field.Init(nodeInfo, fieldInfo, nodeInstance);
            entry.AddField(field.transform);
            foreach (var knob in field.GetComponentsInChildren<Knob>())
            {
                knob.connectionPrefab = connectionPrefab;
            }
        }

        public NodeEntry CreateNodeEntry(NodeInfo nodeInfo, Node nodeInstance)
        {
            var entry = Instantiate(m_NodeEntryPrefab);
            entry.NodeInstance = nodeInstance;
            entry.Name = nodeInfo.type.GetDisplayName();
            entry.CanDelete = nodeInfo.type.CanBeDeleted();
            foreach (var outputField in nodeInfo.outputFields)
            {
                AddField(entry, m_OutputFieldPrefab, nodeInfo, outputField, nodeInstance);
            }

            foreach (var attributeField in nodeInfo.attributeFields)
            {
                AddField(entry, m_AttributeFieldPrefab, nodeInfo, attributeField, nodeInstance);
            }

            foreach (var inputField in nodeInfo.inputFields)
            {
                AddField(entry, m_InputFieldPrefab, nodeInfo, inputField, nodeInstance);
            }


            return entry;
        }
    }
}