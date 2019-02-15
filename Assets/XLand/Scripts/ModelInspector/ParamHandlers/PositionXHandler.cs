using UnityEngine;

namespace XLand
{
    public class PositionXHandler : IParamHandler
    {
        public string Name
        {
            get { return "Position X"; }
        }

        private readonly Transform m_Transform;

        public PositionXHandler(Transform transform)
        {
            m_Transform = transform;
        }

        public float Value
        {
            get { return m_Transform.position.x; }
            set
            {
                var pos = m_Transform.position;
                pos.x = value;
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