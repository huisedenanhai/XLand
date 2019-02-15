using System;
using System.Linq;
using System.Reflection;
using UnityEngine.Assertions;

namespace XLand
{
    public class NodeInfo
    {
        public readonly Type type;
        public readonly FieldInfo[] inputFields;
        public readonly FieldInfo[] attributeFields;
        public readonly FieldInfo[] outputFields;

        private static bool FieldWithType(FieldInfo field, NodeFieldType type)
        {
            var attrs = field.GetCustomAttributes(typeof(MarkAsNodeField), true);
            Assert.IsTrue(attrs.Length > 0);
            var attr = attrs[0] as MarkAsNodeField;
            Assert.IsNotNull(attr);
            return attr.Type == type;
        }

        public NodeInfo(Type type)
        {
            this.type = type;
            var fields = Array.FindAll(type.GetFields(), f => f.IsDefined(typeof(MarkAsNodeField), true));
            inputFields = Array.FindAll(fields, f => FieldWithType(f, NodeFieldType.Input));
            attributeFields = Array.FindAll(fields, f => FieldWithType(f, NodeFieldType.Attribute));
            outputFields = Array.FindAll(fields, f => FieldWithType(f, NodeFieldType.Output));
        }
    }
}