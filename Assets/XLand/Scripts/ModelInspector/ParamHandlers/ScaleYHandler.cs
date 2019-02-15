using UnityEngine;

namespace XLand
{
    public class ScaleYHandler : IParamHandler
    {
        public string Name
        {
            get { return "Scale Y"; }
        }

        private readonly Transform m_Transform;

        public ScaleYHandler(Transform transform)
        {
            m_Transform = transform;
        }

        public float Value
        {
            get { return m_Transform.localScale.y; }
            set
            {
                var scale = m_Transform.localScale;
                scale.y = value;
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