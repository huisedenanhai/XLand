using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace XLand
{
    [RequireComponent(typeof(UILineRenderer))]
    public class UIBezierCurve : MonoBehaviour
    {
        private UILineRenderer m_LineRender;

        private void Awake()
        {
            m_LineRender = GetComponent<UILineRenderer>();
        }

        [SerializeField] private Vector2[] m_ControlPoints;

        public Vector2[] ControlPoints
        {
            get { return m_ControlPoints; }
            set
            {
                if (m_ControlPoints == value) return;
                m_ControlPoints = value ?? new Vector2[1];
                SetDirty();
            }
        }

        private bool m_IsDirty = true;

        public void SetDirty()
        {
            m_IsDirty = true;
        }

        [SerializeField] private int m_Resolution = 10;

        public int Resolution
        {
            get { return m_Resolution; }
            set
            {
                if (value < 0) value = 0;
                if (m_Resolution == value) return;
                m_Resolution = value;
                SetDirty();
            }
        }

        public Vector2 Evaluate(float t)
        {
            var pointNum = ControlPoints.Length;
            if (pointNum == 0) return Vector2.zero;
            if (pointNum == 1) return ControlPoints[0];
            // More than 2 control points
            var ps = ControlPoints;
            var nps = new Vector2[pointNum];
            for (int i = pointNum - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    nps[j].x = Mathf.Lerp(ps[j].x, ps[j + 1].x, t);
                    nps[j].y = Mathf.Lerp(ps[j].y, ps[j + 1].y, t);
                }

                ps = nps;
            }

            return nps[0];
        }


        private void LateUpdate()
        {
            if (m_IsDirty)
            {
                ForceUpdate();
                m_IsDirty = false;
            }
        }

        public void ForceUpdate()
        {
            var pc = Resolution + 1;
            var dt = 1.0f / Resolution;
            var t = 0f;
            var ps = new Vector2[pc];
            for (int i = 0; i < pc; i++)
            {
                ps[i] = Evaluate(t);
                t += dt;
            }

            m_LineRender.Points = ps;
        }
    }
}