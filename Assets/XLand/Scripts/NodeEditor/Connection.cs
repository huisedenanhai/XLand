using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI.Extensions;

namespace XLand
{
    [RequireComponent(typeof(UIBezierCurve))]
    public class Connection : MonoBehaviour
    {
        private Knob m_InputKnob;
        private Knob m_OutputKnob;
        private UIBezierCurve m_UIBezierCurve;

        private void Awake()
        {
            m_UIBezierCurve = GetComponent<UIBezierCurve>();
        }

        private void Start()
        {
            if (InputKnob == null)
            {
                InputPosition = MousePosition;
            }
            else if (OutputKnob == null)
            {
                OutputPosition = MousePosition;
            }
            else
            {
                ReEvaluatePosition();
            }

            m_UIBezierCurve.ForceUpdate();
        }

        private Vector2 MousePosition
        {
            get { return transform.InverseTransformPoint(Input.mousePosition); }
        }

        private Vector3 InputPosition
        {
            get { return m_UIBezierCurve.ControlPoints[0]; }
            set
            {
                m_UIBezierCurve.ControlPoints[0] = value;
                m_UIBezierCurve.ControlPoints[1] = value + Vector3.right * 100;
                m_UIBezierCurve.SetDirty();
            }
        }

        private Vector3 OutputPosition
        {
            get { return m_UIBezierCurve.ControlPoints[3]; }
            set
            {
                m_UIBezierCurve.ControlPoints[3] = value;
                m_UIBezierCurve.ControlPoints[2] = value - Vector3.right * 100;
                m_UIBezierCurve.SetDirty();
            }
        }

        public Knob InputKnob
        {
            get { return m_InputKnob; }
            set
            {
                m_InputKnob = value;
                if (value == null) return;
                Assert.IsFalse(value.isInput);
                var position = transform.InverseTransformPoint(value.transform.position);
                InputPosition = position;
            }
        }

        public void ReEvaluatePosition()
        {
            InputPosition = transform.InverseTransformPoint(InputKnob.transform.position);
            OutputPosition = transform.InverseTransformPoint(OutputKnob.transform.position);
        }

        public Knob OutputKnob
        {
            get { return m_OutputKnob; }
            set
            {
                m_OutputKnob = value;
                if (value == null) return;
                Assert.IsTrue(value.isInput);
                var position = transform.InverseTransformPoint(value.transform.position);
                OutputPosition = position;
            }
        }

        private bool IsValidEndKnob(Knob knob)
        {
            if (InputKnob != null) return InputKnob.CanConnectTo(knob);
            if (OutputKnob != null) return OutputKnob.CanConnectTo(knob);
            return false;
        }

        private ScrollController m_ScrollController;

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

        private bool m_IsDragged;

        public bool IsDragged
        {
            get { return m_IsDragged; }
            set
            {
                m_IsDragged = value;
                ScrollController.CanScroll = !value;
            }
        }

        private void OnDestroy()
        {
            if (InputKnob != null) InputKnob.RemoveConnection(this);
            if (OutputKnob != null) OutputKnob.RemoveConnection(this);
        }

        private NodeEditor m_CurrentNodeEditor;

        private NodeEditor CurrentNodeEditor
        {
            get
            {
                if (m_CurrentNodeEditor == null)
                {
                    m_CurrentNodeEditor = GetComponentInParent<NodeEditor>();
                }

                return m_CurrentNodeEditor;
            }
        }

        /// <summary>
        /// Remove Connection
        /// Ask NodeEditor to remove an edge in the underlying graph
        /// </summary>
        public void Detach()
        {
            Assert.IsNotNull(CurrentNodeEditor);
            if (InputKnob == null || OutputKnob == null) return;
            CurrentNodeEditor.RemoveEdge(InputKnob.Field, OutputKnob.Field);
        }

        /// <summary>
        /// Establish Connection
        /// Ask NodeEditor to add an edge to the underlying graph
        /// </summary>
        public void Establish()
        {
            Assert.IsNotNull(CurrentNodeEditor);
            if (InputKnob == null || OutputKnob == null) return;
            CurrentNodeEditor.AddEdge(InputKnob.Field, OutputKnob.Field);
        }

        private Knob m_LastFrameHoveredKnob;


        private void Update()
        {
            if (!IsDragged) return;
            if (Input.GetMouseButton(0))
            {
                Assert.IsTrue(InputKnob != null || OutputKnob != null);
                if (InputKnob == null)
                {
                    InputPosition = MousePosition;
                }
                else if (OutputKnob == null)
                {
                    OutputPosition = MousePosition;
                }
            }
            else
            {
                IsDragged = false;
                // to deal with touch screens
                var currentHoveredKnob = m_LastFrameHoveredKnob;
                if (currentHoveredKnob != null && IsValidEndKnob(currentHoveredKnob))
                {
                    Assert.IsTrue(InputKnob != null || OutputKnob != null);
                    // establish connection
                    if (InputKnob == null)
                    {
                        InputKnob = currentHoveredKnob;
                        currentHoveredKnob.AddConnection(this);
                    }

                    if (OutputKnob == null)
                    {
                        OutputKnob = currentHoveredKnob;
                        currentHoveredKnob.AddConnection(this);
                    }

                    Assert.IsTrue(InputKnob != null && OutputKnob != null);
                    Establish();
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            m_LastFrameHoveredKnob = Knob.CurrentHoveredKnob;
        }
    }
}