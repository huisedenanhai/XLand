using System.Collections.Generic;

namespace XLand
{
    public class AddNodeMenuData
    {
        private readonly Dictionary<string, AddNodeMenuData> m_Data = new Dictionary<string, AddNodeMenuData>();

        public bool IsNodeEntry
        {
            get { return NodeInfo != null; }
        }

        public NodeInfo NodeInfo { get; set; }

        public AddNodeMenuData Add(string name, AddNodeMenuData data)
        {
            if (!IsNodeEntry && !m_Data.ContainsKey(name))
            {
                m_Data.Add(name, data);
                return data;
            }

            return null;
        }

        public Dictionary<string, AddNodeMenuData> Data
        {
            get { return m_Data; }
        }

        public AddNodeMenuData this[string name]
        {
            get
            {
                AddNodeMenuData data;
                return m_Data.TryGetValue(name, out data) ? data : null;
            }
        }
    }
}