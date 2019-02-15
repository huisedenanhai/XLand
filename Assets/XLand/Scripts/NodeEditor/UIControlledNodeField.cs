using System.Reflection;
using UnityEngine;

namespace XLand
{
    public class UIControlledNodeField : MonoBehaviour
    {
        private NodeInfo m_NodeInfo;
        private FieldInfo m_FieldInfo;
        private Node m_NodeInstance;

        public NodeInfo NodeInfo
        {
            get { return m_NodeInfo; }
        }

        public FieldInfo FieldInfo
        {
            get { return m_FieldInfo; }
        }

        public Node NodeInstance
        {
            get { return m_NodeInstance; }
        }

        public void Init(NodeInfo nodeInfo, FieldInfo field, Node nodeInstance)
        {
            m_NodeInfo = nodeInfo;
            m_FieldInfo = field;
            m_NodeInstance = nodeInstance;
        }
    }
}