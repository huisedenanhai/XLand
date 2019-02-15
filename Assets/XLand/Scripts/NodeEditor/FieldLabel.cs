using UnityEngine;
using UnityEngine.UI;

namespace XLand
{
    [RequireComponent(typeof(Text))]
    public class FieldLabel : MonoBehaviour
    {
        private Text m_Label;

        private Text Label
        {
            get
            {
                if (m_Label == null)
                {
                    m_Label = GetComponent<Text>();
                }

                return m_Label;
            }
        }

        private UIControlledNodeField m_Field;

        private UIControlledNodeField Field
        {
            get
            {
                if (m_Field == null)
                {
                    m_Field = GetComponentInParent<UIControlledNodeField>();
                }

                return m_Field;
            }
        }

        private void Start()
        {
            Label.text = Field.FieldInfo.GetDisplayName();
        }
    }
}