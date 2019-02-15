using UnityEngine;
using UnityEngine.UI;

namespace XLand
{
    [RequireComponent(typeof(Slider))]
    public class ParamControlEntrySlider : MonoBehaviour
    {
        private IParamHandler m_Handler;

        private Slider m_Slider;

        private void Awake()
        {
            m_Slider = GetComponent<Slider>();
            m_Slider.onValueChanged.AddListener(OnValueChanged);
        }

        private bool m_ValueChanged;
        private float m_DesiredValue;

        private void OnValueChanged(float value)
        {
            m_ValueChanged = true;
            m_DesiredValue = value;
        }

        public IParamHandler Handler
        {
            set
            {
                m_Handler = value;
                m_Slider.minValue = m_Handler.MinimumValue;
                m_Slider.maxValue = m_Handler.MaximumValue;
            }
            get { return m_Handler; }
        }

        private void Update()
        {
            if (Handler == null) return;
            m_Slider.Set(Handler.Value, false);
        }

        private void LateUpdate()
        {
            if (m_ValueChanged)
            {
                Handler.Value = Mathf.Clamp(m_DesiredValue, Handler.MinimumValue, Handler.MaximumValue);
                m_ValueChanged = false;
            }
        }
    }
}