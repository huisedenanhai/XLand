using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XLand
{
    public static class PathExtension
    {
        public static string StripAllExtension(this string fileNameWithExtension)
        {
            var f = fileNameWithExtension;
            while (Path.HasExtension(f))
            {
                f = Path.GetFileNameWithoutExtension(f);
            }

            return f;
        }

        public static IEnumerable<string> GetFolders(this string path)
        {
            return path.Split('/').Where(s => !string.IsNullOrEmpty(s));
        }
    }
}