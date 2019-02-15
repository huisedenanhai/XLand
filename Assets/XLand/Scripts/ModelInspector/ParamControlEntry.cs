using System.Globalization;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XLand
{
    public class ParamControlEntry : MonoBehaviour
    {
        private IParamHandler m_Handler;
        [SerializeField] private Text m_ParamNameLabel;
        [SerializeField] private ParamControlEntrySlider m_ParamValueSlider;
        [SerializeField] private ParamControlEntryInputField m_ParamValueInputField;

        public delegate void NodeButtonClicked();

        public NodeButtonClicked OnNodeButtonClicked;

        public IParamHandler Handler
        {
            set
            {
                m_Handler = value;
                m_ParamNameLabel.text = value.Name;
                // Configure slider
                m_ParamValueSlider.Handler = value;
                // Configure input field
                m_ParamValueInputField.Handler = value;
            }
            get { return m_Handler; }
        }

        private bool m_NeedResetValue;


        public void OpenNodeEditor()
        {
            if (OnNodeButtonClicked != null)
            {
                OnNodeButtonClicked.Invoke();
            }
        }

        public void ResetValue()
        {
            m_NeedResetValue = true;
        }

        private void LateUpdate()
        {
            if (m_NeedResetValue)
            {
                if (Handler == null) return;
                Handler.Value = Handler.DefaultValue;
                m_NeedResetValue = false;
            }
        }
    }
}