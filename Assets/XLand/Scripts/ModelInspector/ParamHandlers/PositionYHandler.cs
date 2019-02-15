using UnityEngine;

namespace XLand
{
    public class PositionYHandler : IParamHandler
    {
        public string Name
        {
            get { return "Position Y"; }
        }

        private readonly Transform m_Transform;

        public PositionYHandler(Transform transform)
        {
            m_Transform = transform;
        }

        public float Value
        {
            get { return m_Transform.position.y; }
            set
            {
                var pos = m_Transform.position;
                pos.y = value;
                m_Transform.position = pos;
            }
        }

        public float DefaultValue
        {
            get { return 0; }
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