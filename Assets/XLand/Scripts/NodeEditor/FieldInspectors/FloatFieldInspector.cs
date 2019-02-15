using System.Globalization;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XLand
{
    [RequireComponent(typeof(InputField))]
    public class FloatFieldInspector : FieldInspector
    {
        private InputField m_ValueInputField;

        private InputField ValueInputField
        {
            get
            {
                if (m_ValueInputField == null)
                {
                    m_ValueInputField = GetComponent<InputField>();
                }

                return m_ValueInputField;
            }
        }

        private void Start()
        {
            Assert.IsTrue(Field.FieldInfo.FieldType == typeof(float));
            ValueInputField.onValueChanged.AddListener(OnValueChanged);
        }

        private bool m_NeedUpdateValue;
        private float m_DesiredValue;

        private void OnValueChanged(string value)
        {
            m_NeedUpdateValue = true;
            if (!float.TryParse(value, out m_DesiredValue))
            {
                m_DesiredValue = (float) FieldValue;
            }
        }

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == this.gameObject) return;
            var value = (float) FieldValue;
            ValueInputField.text = value.ToString(CultureInfo.InvariantCulture);
        }

        private void LateUpdate()
        {
            if (m_NeedUpdateValue)
            {
                FieldValue = m_DesiredValue;
                m_NeedUpdateValue = false;
            }
        }
    }
}