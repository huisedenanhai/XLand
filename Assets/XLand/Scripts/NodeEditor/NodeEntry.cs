using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XLand
{
    public class NodeEntry : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Transform m_Fields;
        [SerializeField] private Text m_TypeNameLabel;
        [SerializeField] private Transform m_DeleteButton;
        public Node NodeInstance { get; set; }

        public bool CanDelete
        {
            get { return m_DeleteButton.gameObject.activeSelf; }
            set { m_DeleteButton.gameObject.SetActive(value); }
        }

        public string Name
        {
            set { m_TypeNameLabel.text = value; }
        }

        private Vector3 m_MouseDisplacement;
        private ScrollController m_ScrollController;
        private bool m_IsDragged;

        private ScrollController ScrollController
        {
            get
            {
                if (m_ScrollController == null)
                {
                    m_ScrollController = GetComponentInParent<ScrollController>();
                }

                return m_ScrollController;
            }
        }

        private bool IsDragged
        {
            get { return m_IsDragged; }
            set
            {
                m_IsDragged = value;
                ScrollController.CanScroll = !value;
            }
        }

        private void Start()
        {
            transform.localPosition = NodeInstance.position;
        }

        private Dictionary<string, Knob> m_Knobs;

        private Dictionary<string, Knob> Knobs
        {
            get
            {
                if (m_Knobs == null)
                {
                    m_Knobs = new Dictionary<string, Knob>();
                    var knobs = GetComponentsInChildren<Knob>();
                    foreach (var knob in knobs)
                    {
                        m_Knobs.Add(knob.Field.FieldInfo.Name, knob);
                    }
                }

                return m_Knobs;
            }
        }

        public Knob GetKnobByName(string fieldName)
        {
            Knob k;
            Knobs.TryGetValue(fieldName, out k);
            return k;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsDragged = true;
            m_MouseDisplacement = Input.mousePosition - transform.position;
        }

        public void AddField(Transform fieldGo)
        {
            fieldGo.SetParent(m_Fields);
        }

        public void Delete()
        {
            GetComponentInParent<NodeEditor>().RemoveNode(this);
        }

        public delegate void PositionChanged();

        public event PositionChanged PositionChangedEvent;

        private void Update()
        {
            if (!IsDragged) return;
            if (!Input.GetMouseButton(0))
            {
                IsDragged = false;
                return;
            }

            transform.position = Input.mousePosition - m_MouseDisplacement;
            NodeInstance.position = transform.localPosition;
            if (PositionChangedEvent != null) PositionChangedEvent.Invoke();
        }
    }
}