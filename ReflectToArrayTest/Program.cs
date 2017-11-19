using System;
using System.Collections.Generic;
using HSNXT;

namespace ReflectToArrayTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] outs;
            Console.WriteLine(outs = new List<string>
            {
                "fiddly",
                "hoo"
            }.ReflectToArray()); // System.String[]
            
            Console.WriteLine(outs.Length); // 4 (because of how List expands)
            Console.WriteLine(outs[0]); // "fiddly"
            Console.WriteLine(outs[1]); // "hoo"
        }
    }
}