using System.IO;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static bool IsNullOrEmpty(this FileInfo fileInfo)
        {
            return fileInfo == null || fileInfo.Length == 0;
        }
    }
}