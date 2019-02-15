using System;
using System.Text;

namespace XLand
{
    public static class StringExtension
    {
        public static string InsertSpaceAtUpperCase(this string str)
        {
            var sb = new StringBuilder();
            foreach (var c in str)
            {
                if (char.IsUpper(c))
                {
                    sb.Append(" ");
                }

                sb.Append(c);
            }

            return sb.ToString().Trim();
        }

        public static string WordFirstCharToUpper(this string str)
        {
            var needUpper = true;
            var sb = new StringBuilder();
            foreach (var c in str)
            {
                if (char.IsWhiteSpace(c))
                {
                    needUpper = true;
                    sb.Append(c);
                }
                else if (needUpper)
                {
                    sb.Append(char.ToUpper(c));
                    needUpper = false;
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}