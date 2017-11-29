using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SuperOEISGenerator.IO;

namespace OEISReader.DatabaseX
{
    public static class A999990
    {
        public static readonly long[] Value = { 1L, -1L, 2L, -2L, 8L, 8L, 112L, 656L, 5504L, 49024L, 491264L, 5401856L, 64826368L, 842734592L, 11798300672L, 176974477312L, 2831591702528L, 48137058811904L, 866467058876416L, 16462874118127616L, 329257482363600896L, 6914407129633521664L };
        public static readonly IEnumerable<byte[]> Bytes = Value.Select(BitConverter.GetBytes);

        public static Stream Stream
        {
            get
            {
                var ms = new MemoryStream();
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < Value.Length; i++)
                {
                    var b = BitConverter.GetBytes(Value[i]);
                    ms.Write(b, 0, b.Length);
                }
                return ms;
            }
        }
        
        public static Stream StreamLazy => new EnumerableStream(Bytes);
    }

    public class A999990Enumerable : IEnumerable<byte[]>
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

    public class A999990Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A999990.Value;

        public static Stream Stream
        {
            get
            {
                var ms = new MemoryStream();
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < Value.Length; i++)
                {
                    var b = BitConverter.GetBytes(Value[i]);
                    ms.Write(b, 0, b.Length);
                }
                return ms;
            }
        }
        
        public static Stream StreamLazy => new EnumerableStream(A999990.Bytes);

        public long this[int i] => Value[i];
        
        public static A999990Inst Instance = new A999990Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }
}