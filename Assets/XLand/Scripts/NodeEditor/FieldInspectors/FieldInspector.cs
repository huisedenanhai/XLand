using UnityEngine;

namespace XLand
{
    public class FieldInspector : MonoBehaviour
    {
        private UIControlledNodeField m_Field;

        protected UIControlledNodeField Field
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

        protected object FieldValue
        {
            get { return Field.FieldInfo.GetValue(Field.NodeInstance); }
            set { Field.FieldInfo.SetValue(Field.NodeInstance, value); }
        }
    }
}