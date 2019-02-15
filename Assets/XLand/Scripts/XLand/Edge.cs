using System.Reflection;
using UnityEngine.Assertions;

namespace XLand
{
    public class Edge
    {
        public Node FromNode { get; private set; }
        public FieldInfo FromField { get; private set; }
        public Node ToNode { get; private set; }
        public FieldInfo ToField { get; private set; }

        public Edge(Node fromNode, FieldInfo fromField, Node toNode, FieldInfo toField)
        {
            Assert.IsTrue(fromField.FieldType == toField.FieldType);
            FromNode = fromNode;
            FromField = fromField;
            ToNode = toNode;
            ToField = toField;
        }

        public void PassValue()
        {
            var v = FromField.GetValue(FromNode);
            ToField.SetValue(ToNode, v);
        }

        public override bool Equals(object obj)
        {
            var rhs = obj as Edge;
            if (rhs == null) return false;
            return FromNode == rhs.FromNode && ToNode == rhs.ToNode && FromField == rhs.FromField &&
                   ToField == rhs.ToField;
        }
    }
}