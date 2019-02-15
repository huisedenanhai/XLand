using System;

namespace XLand
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class MarkAsNodeField : Attribute
    {
        private NodeFieldType m_Type = NodeFieldType.Attribute;

        public NodeFieldType Type
        {
            set { m_Type = value; }
            get { return m_Type; }
        }

        private string m_DisplayName;

        public string DisplayName
        {
            set { m_DisplayName = value; }
            get { return m_DisplayName; }
        }
    }
}