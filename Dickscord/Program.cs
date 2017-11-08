using System;
using System.Collections.Generic;
using System.Text;

namespace Dickscord
{
    internal class Program
    {
        private static void Main(string[] args) 
        { 
            var originalString = "This string contains the unicode character Pi(⛹🏼)"; 
            Console.WriteLine($"Final string: {Cv(originalString)}"); 
        }

        public static string Cv(string originalString)
        {            
            var bytes = Encoding.UTF32.GetBytes(originalString);
            var asAscii = new StringBuilder();
            for (var idx = 0; idx < bytes.Length; idx += 4)
            { 
                var codepoint = BitConverter.ToUInt32(bytes, idx);
                if (codepoint <= sbyte.MaxValue) 
                    asAscii.Append(Convert.ToChar(codepoint)); 
                else if (codepoint <= ushort.MaxValue)
                    asAscii.AppendFormat(@"\u{0:x4}", codepoint);
                else 
                    asAscii.AppendFormat(@"\U{0:x8}", codepoint); 
            }
            return asAscii.ToString();
        }
    }
}