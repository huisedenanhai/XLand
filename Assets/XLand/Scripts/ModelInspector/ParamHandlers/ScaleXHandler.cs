using UnityEngine;

namespace XLand
{
    public class ScaleXHandler : IParamHandler
    {
        public string Name
        {
            get { return "Scale X"; }
        }

        private readonly Transform m_Transform;

        public ScaleXHandler(Transform transform)
        {
            m_Transform = transform;
        }

        public float Value
        {
            get { return m_Transform.localScale.x; }
            set
            {
                var scale = m_Transform.localScale;
                scale.x = value;
                m_Transform.localScale = scale;
            }
        }

        public float DefaultValue
        {
            get { return 1; }
        }

        public float MinimumValue
        {
            get { return -10; }
        }

        public float MaximumValue
        {
            get { return 10; }
        }
    }
}