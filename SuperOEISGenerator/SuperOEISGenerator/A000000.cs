using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using SuperOEISGenerator.IO;

namespace OEISReader.DatabaseX
{
    public static class A000000
    {
        public static readonly BigInteger[] Value = { 1L, -1L, 2L, -2L, 8L, 8L, 112L, 656L, 5504L, 49024L, 491264L, 5401856L, 64826368L, 842734592L, 11798300672L, 176974477312L, 2831591702528L, 48137058811904L, 866467058876416L, 16462874118127616L, 329257482363600896L, 6914407129633521664L, BigInteger.Parse("152116956851941670912") };
        public static readonly IEnumerable<byte[]> Bytes = Value.Select(e => e.ToByteArray());

        public static Stream Stream
        {
            get
            {
                var ms = new MemoryStream();
                for (var i = 0; i < Value.Length; i++)
                {
                    var b = Value[i].ToByteArray();
                    ms.Write(b, 0, b.Length);
                }
                return ms;
            }
        }
        
        public static Stream StreamLazy => new EnumerableStream(Bytes);

        //public static Stream Stream = new MemoryStream(BitConverter.GetBytes(Value[0]));
    }

    public class A000000Enumerable : IEnumerable<byte[]>
    {
        public IEnumerator<byte[]> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class A000000Inst : IEnumerable<BigInteger>
    {
        public static readonly BigInteger[] Value = A000000.Value;

        public static Stream Stream
        {
            get
            {
                var ms = new MemoryStream();
                for (var i = 0; i < Value.Length; i++)
                {
                    var b = Value[i].ToByteArray();
                    ms.Write(b, 0, b.Length);
                }
                return ms;
            }
        }
        
        public static Stream StreamLazy => new EnumerableStream(A000000.Bytes);

        public BigInteger this[int i] => Value[i];
        
        public static A000000Inst Instance = new A000000Inst();
        
        public IEnumerator<BigInteger> GetEnumerator()
        {
            return (Value as IEnumerable<BigInteger>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }
}