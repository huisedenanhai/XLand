using System;
using System.IO;

namespace XLand
{
    public static class TypeExtension
    {
        public static string GetDisplayName(this Type type)
        {
            var attrs = type.GetCustomAttributes(typeof(MarkAsNode), false);
            if (attrs.Length > 0)
            {
                var attr = attrs[0] as MarkAsNode;
                if (attr != null && attr.DisplayName != null)
                {
                    return Path.GetFileName(attr.DisplayName);
                }
            }

            return type.Name.InsertSpaceAtUpperCase().WordFirstCharToUpper();
        }

        public static bool CanBeDeleted(this Type type)
        {
            var attrs = type.GetCustomAttributes(typeof(MarkAsNode), false);
            if (attrs.Length > 0)
            {
                var attr = attrs[0] as MarkAsNode;
                return attr == null || attr.CanBeDeleted;
            }

            return true;
        }
    }
}