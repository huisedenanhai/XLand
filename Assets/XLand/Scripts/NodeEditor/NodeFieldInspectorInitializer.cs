using UnityEngine;

namespace XLand
{
    [RequireComponent(typeof(UIControlledNodeField))]
    public class NodeFieldInspectorInitializer : MonoBehaviour
    {
        public Transform floatInspectorPrefab;
        public Transform enumInspectorPrefab;
        public Transform stringFieldPrefab;

        private UIControlledNodeField m_Field;

        private UIControlledNodeField Field
        {
            get
            {
                if (m_Field == null)
                {
                    m_Field = GetComponent<UIControlledNodeField>();
                }

                return m_Field;
            }
        }

        private void Start()
        {
            var fieldType = Field.FieldInfo.FieldType;
            if (fieldType == typeof(float))
            {
                Instantiate(floatInspectorPrefab, transform, false);
            }
            else if (fieldType == typeof(string))
            {
                Instantiate(stringFieldPrefab, transform, false);
            }
            else if (fieldType.IsEnum)
            {
                Instantiate(enumInspectorPrefab, transform, false);
            }
        }
    }
}