using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XLand
{
    public class AddNodeMenuEntry : MonoBehaviour, IPointerClickHandler
    {
        private NodeInfo m_NodeInfo;

        public NodeInfo NodeInfo
        {
            set
            {
                m_NodeInfo = value;
                m_NodeTypeLabel.text = value != null
                    ? value.type.GetDisplayName()
                    : "Node Type";
            }
            get { return m_NodeInfo; }
        }

        [SerializeField] private Text m_NodeTypeLabel;

        public void OnPointerClick(PointerEventData eventData)
        {
            GetComponentInParent<NodeEditor>().AddNode(NodeInfo);
        }
    }
}