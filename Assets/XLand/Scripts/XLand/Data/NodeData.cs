using System.Collections.Generic;

namespace XLand
{
    public class Vector2Data
    {
        public float x;
        public float y;
    }

    public class NodeData
    {
        public string type;
        public Vector2Data position;
        public Dictionary<string, string> fields;
    }
}