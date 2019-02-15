using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XLand
{
    [RequireComponent(typeof(InputField))]
    public class ParamControlEntryInputField : MonoBehaviour
    {
        private InputField m_InputField;

        private void Awake()
        {
            m_InputField = GetComponent<InputField>();
            m_InputField.onValueChanged.AddListener(OnValueChanged);
        }

        private IParamHandler m_Handler;

        public IParamHandler Handler
        {
            set { m_Handler = value; }
            get { return m_Handler; }
        }

        private bool m_NeedUpdateValue;
        private float m_DesiredValue;

        private void OnValueChanged(string value)
        {
            m_NeedUpdateValue = true;
            if (!float.TryParse(value, out m_DesiredValue))
            {
                m_DesiredValue = Handler.Value;
            }
        }

        private void Update()
        {
            if (Handler == null || EventSystem.current.currentSelectedGameObject == this.gameObject) return;
            m_InputField.text = Handler.Value.ToString(CultureInfo.InvariantCulture);
        }

        private void LateUpdate()
        {
            if (m_NeedUpdateValue)
            {
                Handler.Value = Mathf.Clamp(m_DesiredValue, Handler.MinimumValue, Handler.MaximumValue);
                m_NeedUpdateValue = false;
            }
        }
    }
}