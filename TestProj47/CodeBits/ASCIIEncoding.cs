using System;
using System.Text;

namespace HSNXT.Portable
{
    // ReSharper disable once InconsistentNaming
    public sealed class ASCIIEncoding
    {
        public static string GetString(byte[] bytes, int index, int count)
        {
            //return Encoding.ASCII.GetString(bytes, index, count);
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));
            if (bytes.Length == 0)
                return string.Empty;

            if (index > bytes.Length - 1)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "index value is greater than the size of the byte array");

            if (index < 0)
                index = 0;
            if (index + count > bytes.Length)
                count = bytes.Length - index;

            var sb = new StringBuilder(count);
            for (var i = index; i < index + count; i++)
                sb.Append(bytes[i] <= 0x7f ? (char) bytes[i] : '?');
            return sb.ToString();
        }

        public static byte[] GetBytes(string str)
        {
            //return Encoding.ASCII.GetBytes(str);
            if (str == null)
                return new byte[0];
            var retval = new byte[str.Length];
            for (var i = 0; i < str.Length; ++i)
            {
                var ch = str[i];
                if (ch <= 0x7f)
                    retval[i] = (byte) ch;
                else
                    retval[i] = (byte) '?';
            }
            return retval;
        }
    }
}