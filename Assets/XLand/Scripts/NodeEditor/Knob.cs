using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XLand
{
    public class Knob : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Connection connectionPrefab;
        private Transform m_EdgesParent;
        public bool isInput;

        private UIControlledNodeField m_Field;

        public UIControlledNodeField Field
        {
            get
            {
                if (m_Field == null)
                {
                    m_Field = GetComponentInParent<UIControlledNodeField>();
                }

                return m_Field;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (isInput && m_RelatedConnections.Count == 1)
            {
                // only one connection can be associated with an input knob
                var connect = m_RelatedConnections.First();
                Assert.IsTrue(connect.InputKnob != null && connect.OutputKnob != null);
                connect.Detach();
                RemoveConnection(connect);
                connect.OutputKnob = null;
                connect.IsDragged = true;
                return;
            }

            // create a new connection
            var connection = Instantiate(connectionPrefab, m_EdgesParent, false);
            Assert.IsNotNull(connection);
            connection.transform.localPosition = Vector3.zero;
            connection.transform.localScale = Vector3.one;
            if (isInput)
            {
                connection.OutputKnob = this;
            }
            else
            {
                connection.InputKnob = this;
            }

            connection.IsDragged = true;
            AddConnection(connection);
        }

        private readonly HashSet<Connection> m_RelatedConnections = new HashSet<Connection>();

        private Text connectCountLabel;

        private void Awake()
        {
            connectCountLabel = GetComponentInChildren<Text>();
        }

        private void Start()
        {
            m_EdgesParent = GetComponentInParent<NodeEditor>().EdgesParent;
            GetComponentInParent<NodeEntry>().PositionChangedEvent += UpdatePosition;
        }

        private void UpdatePosition()
        {
            foreach (var connection in m_RelatedConnections)
            {
                connection.ReEvaluatePosition();
            }
        }

        private void UpdateCountLabel()
        {
            var cnt = m_RelatedConnections.Count;
            connectCountLabel.text = cnt != 0 ? cnt.ToString() : ">";
        }

        public void AddConnection(Connection connection)
        {
            m_RelatedConnections.Add(connection);
            UpdateCountLabel();
        }

        public void RemoveConnection(Connection connection)
        {
            m_RelatedConnections.Remove(connection);
            UpdateCountLabel();
        }

        private bool CanAddConnection()
        {
            return !isInput || m_RelatedConnections.Count != 1;
        }

        private void OnDestroy()
        {
            foreach (var connection in m_RelatedConnections)
            {
                Destroy(connection.gameObject);
            }
        }

        /// <summary>
        /// When calling this method, current knob has already been associated with an incomplete connection. This method
        /// will answer whether the new knob can be added to this incomplete connection.
        /// </summary>
        /// <param name="knob">the knob to be connected</param>
        /// <returns>true when two knobs can form a legal connection</returns>
        public bool CanConnectTo(Knob knob)
        {
            return knob.isInput != isInput && knob.CanAddConnection() &&
                   knob.Field.FieldInfo.FieldType == Field.FieldInfo.FieldType;
        }

        public static Knob CurrentHoveredKnob { get; private set; }

        public string DisplayName
        {
            get { return Field.NodeInfo.type.GetDisplayName() + "/" + Field.FieldInfo.GetDisplayName(); }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CurrentHoveredKnob = this;
            CurrentHoveredKnobLabel.text = DisplayName;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CurrentHoveredKnob = null;
            CurrentHoveredKnobLabel.text = "";
        }

        private static Text m_CurrentHoveredKnobLabel;

        public Text CurrentHoveredKnobLabel
        {
            get
            {
                if (m_CurrentHoveredKnobLabel == null)
                {
                    m_CurrentHoveredKnobLabel = GameObject.Find("_Hovered Knob Label").GetComponent<Text>();
                }

                return m_CurrentHoveredKnobLabel;
            }
        }
    }
}