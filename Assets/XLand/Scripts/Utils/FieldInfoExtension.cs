using System.IO;
using System.Reflection;

namespace XLand
{
    public static class FieldInfoExtension
    {
        public static string GetDisplayName(this FieldInfo field)
        {
            var attrs = field.GetCustomAttributes(typeof(MarkAsNodeField), true);
            if (attrs.Length > 0)
            {
                var attr = attrs[0] as MarkAsNodeField;
                if (attr != null && attr.DisplayName != null)
                {
                    return Path.GetFileName(attr.DisplayName);
                }
            }

            return field.Name;
        }
    }
}