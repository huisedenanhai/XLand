using System;

namespace XLand
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MarkAsNode : Attribute
    {
        public string DisplayName { get; set; }

        public bool HideInInspector { get; set; }

        private bool m_CanBeDeleted = true;

        public bool CanBeDeleted
        {
            get { return m_CanBeDeleted; }
            set { m_CanBeDeleted = value; }
        }
    }
}