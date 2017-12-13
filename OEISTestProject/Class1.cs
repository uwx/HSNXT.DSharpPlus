using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using SuperOEISGenerator.IO;

namespace OEISTestProject
{
    public static class A291421
    {
        public static readonly BigInteger[] Value = { 1L, 1L, 6L, 45L, 414L, 4310L, 49068L, 598253L, 7707738L, 103981222L, 1459259444L, 21201220726L, 317718863636L, 4897066444332L, 77455837982360L, 1254882911977597L, 20793816009974054L, 351973815700006842L, 6079707258590589100L, BigInteger.Parse("107070921557974264470"), BigInteger.Parse("1921112466081500096044"), BigInteger.Parse("35095122874748021511252"), BigInteger.Parse("652393778217784214993656"), BigInteger.Parse("12334667847853804120010726") };
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
    }

    public class A291421Enumerable : IEnumerable<byte[]>
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

    public class A291421Inst : IEnumerable<BigInteger>
    {
        public static readonly BigInteger[] Value = A291421.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291421.Bytes);

        public BigInteger this[int i] => Value[i];
        
        public static A291421Inst Instance = new A291421Inst();
        
        public IEnumerator<BigInteger> GetEnumerator()
        {
            return (Value as IEnumerable<BigInteger>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291420
    {
        public static readonly long[] Value = { 341880L, 8168160L, 14636160L, 17957940L, 52492440L, 116396280L, 1071572040L, 1187525640L, 1728483120L, 5988702720L, 6609482880L, 22539095040L, 29239970760L, 136496680320L, 258670630680L, 398648544840L, 494892478080L, 592003418160L, 1329673884000L, 1343798407560L, 2190884461920L };
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

    public class A291420Enumerable : IEnumerable<byte[]>
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

    public class A291420Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291420.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291420.Bytes);

        public long this[int i] => Value[i];
        
        public static A291420Inst Instance = new A291420Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291419
    {
        public static readonly long[] Value = { 1L, 1L, 2L, 4L, 10L, 24L, 60L, 148L, 376L, 944L, 2392L, 6032L, 15280L, 38608L, 97728L, 247104L, 625312L, 1581568L, 4001680L, 10122624L, 25610368L, 64787520L, 163907904L, 414654848L, 1049031104L, 2653873152L, 6713958912L, 16985280000L, 42970438432L, 108708830336L, 275018076928L, 695755635328L, 1760162851328L };
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

    public class A291419Enumerable : IEnumerable<byte[]>
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

    public class A291419Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291419.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291419.Bytes);

        public long this[int i] => Value[i];
        
        public static A291419Inst Instance = new A291419Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291418
    {
        public static readonly long[] Value = { 1L, 0L, -1L, 0L, 3L, -1L, -12L, 9L, 55L, -67L, -267L, 468L, 1323L, -3180L, -6513L, 21267L, 30969L, -140581L, -135995L, 919698L, 494361L, -5954217L, -829116L, 38113425L, -9433359L, -240844482L, 154219912L, 1499076989L, -1585801575L, -9161079266L, 13958031252L, 54710928759L, -113373461193L, -317030478360L, 875491422246L };
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

    public class A291418Enumerable : IEnumerable<byte[]>
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

    public class A291418Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291418.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291418.Bytes);

        public long this[int i] => Value[i];
        
        public static A291418Inst Instance = new A291418Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291378
    {
        public static readonly long[] Value = { 1L, -2L, 4L, -9L, 24L, -74L, 251L, -902L, 3359L, -12802L, 49588L, -194445L, 770099L, -3076129L, 12380317L, -50162386L, 204475572L, -838014584L, 3451174777L, -14274905490L, 59276495017L, -247019567936L, 1032709501505L, -4330122550717L, 18204993223606L, -76728300335664L, 324125242867935L, -1372110743864550L };
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

    public class A291378Enumerable : IEnumerable<byte[]>
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

    public class A291378Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291378.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291378.Bytes);

        public long this[int i] => Value[i];
        
        public static A291378Inst Instance = new A291378Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291377
    {
        public static readonly long[] Value = { 1L, 0L, 1L, 0L, 2L, -1L, 5L, -7L, 15L, -35L, 57L, -155L, 262L, -664L, 1297L, -2910L, 6437L, -13428L, 31461L, -65137L, 152576L, -325838L, 744223L, -1649943L, 3685869L, -8376976L, 18574146L, -42579093L, 94912298L, -217177891L, 489321856L, -1114542791L, 2535640016L, -5761630456L, 13184657747L, -29989008137L };
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

    public class A291377Enumerable : IEnumerable<byte[]>
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

    public class A291377Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291377.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291377.Bytes);

        public long this[int i] => Value[i];
        
        public static A291377Inst Instance = new A291377Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291376
    {
        public static readonly BigInteger[] Value = { 1L, -2L, -2L, -14L, -134L, -1610L, -22970L, -376070L, -6912590L, -140545682L, -3127227122L, -75537934526L, -1968218386646L, -55032827628122L, -1643983822922282L, -52268580072454070L, -1762720241380630430L, BigInteger.Parse("-62864993479711480610"), BigInteger.Parse("-2364417640569364405730"), BigInteger.Parse("-93549390640311405418094") };
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
    }

    public class A291376Enumerable : IEnumerable<byte[]>
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

    public class A291376Inst : IEnumerable<BigInteger>
    {
        public static readonly BigInteger[] Value = A291376.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291376.Bytes);

        public BigInteger this[int i] => Value[i];
        
        public static A291376Inst Instance = new A291376Inst();
        
        public IEnumerator<BigInteger> GetEnumerator()
        {
            return (Value as IEnumerable<BigInteger>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291375
    {
        public static readonly long[] Value = { 0L, 1L, 0L, 2L, 0L, 1L, 1L, 0L, 0L, 4L, 0L, 0L, 5L, 1L, 0L, 0L, 2L, 6L, 0L, 0L, 0L, 12L, 1L, 0L, 0L, 0L, 8L, 9L, 0L, 0L, 0L, 1L, 25L, 1L, 0L, 0L, 0L, 0L, 28L, 12L, 0L, 0L, 0L, 0L, 12L, 44L, 1L, 0L, 0L, 0L, 0L, 2L, 68L, 16L, 0L, 0L, 0L, 0L, 0L, 48L, 73L, 1L, 0L, 0L, 0L, 0L, 0L, 14L, 150L, 20L, 0L, 0L, 0L, 0L, 0L, 1L, 155L, 112L, 1L };
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

    public class A291375Enumerable : IEnumerable<byte[]>
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

    public class A291375Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291375.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291375.Bytes);

        public long this[int i] => Value[i];
        
        public static A291375Inst Instance = new A291375Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291374
    {
        public static readonly long[] Value = { 11L, 17L, 41L, 43L, 47L, 137L, 313L, 359L, 389L, 401L, 491L, 557L, 577L, 709L, 757L, 829L, 863L, 929L, 937L, 953L, 1129L, 1163L, 1249L, 1301L, 1307L, 1439L, 1597L, 1627L, 1693L, 1847L, 2087L, 2311L, 2351L, 2437L, 2663L, 2731L, 2741L, 3109L, 3119L, 3217L, 3253L, 4027L, 4219L, 4271L, 4547L, 4637L, 5189L, 5237L };
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

    public class A291374Enumerable : IEnumerable<byte[]>
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

    public class A291374Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291374.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291374.Bytes);

        public long this[int i] => Value[i];
        
        public static A291374Inst Instance = new A291374Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291373
    {
        public static readonly long[] Value = { 2L, 0L, 6L, 841L, 0L, 1722L, 30018L, 0L };
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

    public class A291373Enumerable : IEnumerable<byte[]>
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

    public class A291373Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291373.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291373.Bytes);

        public long this[int i] => Value[i];
        
        public static A291373Inst Instance = new A291373Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291370
    {
        public static readonly long[] Value = { 17L, 17L, 3L, 4L, 5L, 3L, 7L, 4L, 3L, 5L, 11L, 3L, 13L, 7L, 3L, 4L, 17L, 3L, 17L, 4L, 3L, 11L, 17L, 3L, 5L, 13L, 3L, 4L, 17L, 3L, 17L, 4L, 3L, 17L, 5L, 3L, 17L, 17L, 3L, 4L, 17L, 3L, 17L, 4L, 3L, 17L, 17L, 3L, 7L, 5L, 3L, 4L, 17L, 3L, 5L, 4L, 3L, 17L, 17L, 3L, 17L, 17L, 3L, 4L, 5L, 3L, 17L, 4L, 3L, 5L };
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

    public class A291370Enumerable : IEnumerable<byte[]>
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

    public class A291370Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291370.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291370.Bytes);

        public long this[int i] => Value[i];
        
        public static A291370Inst Instance = new A291370Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291369
    {
        public static readonly long[] Value = { 15L, 15L, 3L, 4L, 5L, 3L, 7L, 4L, 3L, 5L, 11L, 3L, 13L, 7L, 3L, 4L, 15L, 3L, 15L, 4L, 3L, 11L, 15L, 3L, 5L, 13L, 3L, 4L, 15L, 3L, 15L, 4L, 3L, 15L, 5L, 3L, 15L, 15L, 3L, 4L, 15L, 3L, 15L, 4L, 3L, 15L, 15L, 3L, 7L, 5L, 3L, 4L, 15L, 3L, 5L, 4L, 3L, 15L, 15L, 3L, 15L, 15L, 3L, 4L, 5L, 3L, 15L, 4L, 3L, 5L };
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

    public class A291369Enumerable : IEnumerable<byte[]>
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

    public class A291369Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291369.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291369.Bytes);

        public long this[int i] => Value[i];
        
        public static A291369Inst Instance = new A291369Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291368
    {
        public static readonly long[] Value = { 13L, 13L, 3L, 4L, 5L, 3L, 7L, 4L, 3L, 5L, 11L, 3L, 13L, 7L, 3L, 4L, 13L, 3L, 13L, 4L, 3L, 11L, 13L, 3L, 5L, 13L, 3L, 4L, 13L, 3L, 13L, 4L, 3L, 13L, 5L, 3L, 13L, 13L, 3L, 4L, 13L, 3L, 13L, 4L, 3L, 13L, 13L, 3L, 7L, 5L, 3L, 4L, 13L, 3L, 5L, 4L, 3L, 13L, 13L, 3L, 13L, 13L, 3L, 4L, 5L, 3L, 13L, 4L, 3L, 5L };
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

    public class A291368Enumerable : IEnumerable<byte[]>
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

    public class A291368Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291368.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291368.Bytes);

        public long this[int i] => Value[i];
        
        public static A291368Inst Instance = new A291368Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291367
    {
        public static readonly long[] Value = { 11L, 11L, 3L, 4L, 5L, 3L, 7L, 4L, 3L, 5L, 11L, 3L, 11L, 7L, 3L, 4L, 11L, 3L, 11L, 4L, 3L, 11L, 11L, 3L, 5L, 11L, 3L, 4L, 11L, 3L, 11L, 4L, 3L, 11L, 5L, 3L, 11L, 11L, 3L, 4L, 11L, 3L, 11L, 4L, 3L, 11L, 11L, 3L, 7L, 5L, 3L, 4L, 11L, 3L, 5L, 4L, 3L, 11L, 11L, 3L, 11L, 11L, 3L, 4L, 5L, 3L, 11L, 4L, 3L, 5L };
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

    public class A291367Enumerable : IEnumerable<byte[]>
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

    public class A291367Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291367.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291367.Bytes);

        public long this[int i] => Value[i];
        
        public static A291367Inst Instance = new A291367Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291366
    {
        public static readonly long[] Value = { 9L, 9L, 3L, 4L, 5L, 3L, 7L, 4L, 3L, 5L, 9L, 3L, 9L, 7L, 3L, 4L, 9L, 3L, 9L, 4L, 3L, 9L, 9L, 3L, 5L, 9L, 3L, 4L, 9L, 3L, 9L, 4L, 3L, 9L, 5L, 3L, 9L, 9L, 3L, 4L, 9L, 3L, 9L, 4L, 3L, 9L, 9L, 3L, 7L, 5L, 3L, 4L, 9L, 3L, 5L, 4L, 3L, 9L, 9L, 3L, 9L, 9L, 3L, 4L, 5L, 3L, 9L, 4L, 3L, 5L };
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

    public class A291366Enumerable : IEnumerable<byte[]>
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

    public class A291366Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291366.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291366.Bytes);

        public long this[int i] => Value[i];
        
        public static A291366Inst Instance = new A291366Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291364
    {
        public static readonly long[] Value = { 7L, 7L, 3L, 4L, 5L, 3L, 7L, 4L, 3L, 5L, 7L, 3L, 7L, 7L, 3L, 4L, 7L, 3L, 7L, 4L, 3L, 7L, 7L, 3L, 5L, 7L, 3L, 4L, 7L, 3L, 7L, 4L, 3L, 7L, 5L, 3L, 7L, 7L, 3L, 4L, 7L, 3L, 7L, 4L, 3L, 7L, 7L, 3L, 7L, 5L, 3L, 4L, 7L, 3L, 5L, 4L, 3L, 7L, 7L, 3L, 7L, 7L, 3L, 4L, 5L, 3L, 7L, 4L, 3L, 5L };
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

    public class A291364Enumerable : IEnumerable<byte[]>
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

    public class A291364Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291364.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291364.Bytes);

        public long this[int i] => Value[i];
        
        public static A291364Inst Instance = new A291364Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291264
    {
        public static readonly long[] Value = { 4L, 12L, 36L, 104L, 292L, 804L, 2180L, 5840L, 15492L, 40764L, 106532L, 276792L, 715556L, 1841748L, 4722180L, 12066208L, 30737924L, 78088812L, 197892388L, 500374024L, 1262618148L, 3180066180L, 7995639940L, 20071580784L, 50312160388L, 125942854044L, 314865132324L, 786254598872L };
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

    public class A291264Enumerable : IEnumerable<byte[]>
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

    public class A291264Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291264.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291264.Bytes);

        public long this[int i] => Value[i];
        
        public static A291264Inst Instance = new A291264Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291263
    {
        public static readonly long[] Value = { 1L, 2L, 11L, 9468L };
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

    public class A291263Enumerable : IEnumerable<byte[]>
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

    public class A291263Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291263.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291263.Bytes);

        public long this[int i] => Value[i];
        
        public static A291263Inst Instance = new A291263Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291262
    {
        public static readonly long[] Value = { 2L, 3L, 24L, 76811L };
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

    public class A291262Enumerable : IEnumerable<byte[]>
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

    public class A291262Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291262.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291262.Bytes);

        public long this[int i] => Value[i];
        
        public static A291262Inst Instance = new A291262Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291261
    {
        public static readonly long[] Value = { 1L, 1L, 1L, 1L, 1L, 2L, 1L, 1L, 4L, 5L, 1L, 1L, 10L, 31L, 14L, 1L, 1L, 28L, 325L, 364L, 42L, 1L, 1L, 82L, 4159L, 22150L, 5746L, 132L, 1L, 1L, 244L, 57349L, 1790452L, 2586250L, 113944L, 429L, 1L, 1L, 730L, 818911L, 162045118L, 1691509906L, 461242900L, 2719291L, 1430L, 1L, 1L, 2188L, 11923525L, 15520964284L, 1289803048426L, 2978600051368L, 116651486125L, 75843724L, 4862L };
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

    public class A291261Enumerable : IEnumerable<byte[]>
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

    public class A291261Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291261.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291261.Bytes);

        public long this[int i] => Value[i];
        
        public static A291261Inst Instance = new A291261Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291260
    {
        public static readonly long[] Value = { 1L, 1L, 1L, 1L, 2L, 2L, 1L, 4L, 12L, 5L, 1L, 8L, 80L, 120L, 14L, 1L, 16L, 576L, 3904L, 1680L, 42L, 1L, 32L, 4352L, 152064L, 354560L, 30240L, 132L, 1L, 64L, 33792L, 6492160L, 99422208L, 51733504L, 665280L, 429L, 1L, 128L, 266240L, 290488320L, 31832735744L, 130292416512L, 11070525440L, 17297280L, 1430L };
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

    public class A291260Enumerable : IEnumerable<byte[]>
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

    public class A291260Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291260.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291260.Bytes);

        public long this[int i] => Value[i];
        
        public static A291260Inst Instance = new A291260Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291259
    {
        public static readonly long[] Value = { 0L, 1L, 9L, 25L, 45L, 69L, 108L, 145L, 193L, 248L, 305L, 373L, 437L, 517L, 608L, 697L, 793L, 889L, 1005L, 1124L, 1245L, 1369L, 1510L, 1649L, 1789L, 1941L, 2109L, 2278L, 2449L, 2617L, 2809L };
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

    public class A291259Enumerable : IEnumerable<byte[]>
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

    public class A291259Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291259.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291259.Bytes);

        public long this[int i] => Value[i];
        
        public static A291259Inst Instance = new A291259Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291258
    {
        public static readonly long[] Value = { 1L, 2L, 12L, 75960L, 17156160L };
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

    public class A291258Enumerable : IEnumerable<byte[]>
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

    public class A291258Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291258.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291258.Bytes);

        public long this[int i] => Value[i];
        
        public static A291258Inst Instance = new A291258Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291257
    {
        public static readonly long[] Value = { 1L, 3L, 9L, 28L, 85L, 261L, 797L, 2440L, 7461L, 22827L, 69821L, 213588L, 653345L, 1998573L, 6113529L, 18701072L, 57205769L, 174990195L, 535287793L, 1637423756L, 5008812525L, 15321754293L, 46868623381L, 143369215128L, 438560602669L, 1341539064795L, 4103713486629L, 12553092811972L };
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

    public class A291257Enumerable : IEnumerable<byte[]>
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

    public class A291257Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291257.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291257.Bytes);

        public long this[int i] => Value[i];
        
        public static A291257Inst Instance = new A291257Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291256
    {
        public static readonly long[] Value = { 1L, 20L, 300L, 2010L, 4000L, 50000L, 100110L, 102100L, 200200L, 300010L, 302000L, 400100L, 600000L, 7000000L, 20001010L, 20003000L, 20101100L, 20301000L, 40000010L, 40002000L, 40100100L, 40300000L, 60001000L, 80000000L, 300000200L, 300100010L, 300102000L, 300200100L, 300400000L, 320101000L, 340100000L };
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

    public class A291256Enumerable : IEnumerable<byte[]>
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

    public class A291256Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291256.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291256.Bytes);

        public long this[int i] => Value[i];
        
        public static A291256Inst Instance = new A291256Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291255
    {
        public static readonly long[] Value = { 2L, 7L, 18L, 55L, 144L, 404L, 1060L, 2853L, 7442L, 19573L, 50670L, 131368L, 337622L, 866819L, 2213650L, 5642899L, 14332988L, 36335548L, 91872760L, 231875713L, 584030738L, 1468631153L, 3686943130L, 9242753104L, 23138167146L, 57851432575L, 144470316562L, 360384852207L, 898051760168L };
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

    public class A291255Enumerable : IEnumerable<byte[]>
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

    public class A291255Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291255.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291255.Bytes);

        public long this[int i] => Value[i];
        
        public static A291255Inst Instance = new A291255Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291254
    {
        public static readonly long[] Value = { 4L, 14L, 48L, 159L, 512L, 1618L, 5036L, 15491L, 47192L, 142624L, 428144L, 1277884L, 3795208L, 11222716L, 33060072L, 97060033L, 284095940L, 829298422L, 2414859016L, 7016265637L, 20344112608L, 58879534286L, 170117201548L, 490736173432L, 1413562889020L, 4066259673834L, 11682314946048L };
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

    public class A291254Enumerable : IEnumerable<byte[]>
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

    public class A291254Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291254.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291254.Bytes);

        public long this[int i] => Value[i];
        
        public static A291254Inst Instance = new A291254Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291253
    {
        public static readonly long[] Value = { 2L, 5L, 12L, 30L, 70L, 166L, 382L, 881L, 2002L, 4540L, 10210L, 22891L, 51050L, 113506L, 251430L, 555466L, 1223680L, 2689591L, 5898290L, 12909880L, 28204178L, 61515521L, 133961048L, 291308806L, 632628710L, 1372170030L, 2972790738L, 6433570445L, 13909116418L, 30042364980L, 64830556978L };
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

    public class A291253Enumerable : IEnumerable<byte[]>
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

    public class A291253Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291253.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291253.Bytes);

        public long this[int i] => Value[i];
        
        public static A291253Inst Instance = new A291253Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291252
    {
        public static readonly long[] Value = { 0L, 0L, 3L, 0L, 9L, 6L, 18L, 36L, 40L, 126L, 135L, 351L, 513L, 936L, 1755L, 2682L, 5373L, 8260L, 15525L, 25731L, 44511L, 78030L, 129564L, 229617L, 381438L, 664038L, 1121144L, 1910790L, 3263796L, 5500110L, 9404820L, 15824790L, 26910426L, 45388638L, 76700664L, 129564945L, 218084256L, 368095230L };
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

    public class A291252Enumerable : IEnumerable<byte[]>
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

    public class A291252Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291252.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291252.Bytes);

        public long this[int i] => Value[i];
        
        public static A291252Inst Instance = new A291252Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291251
    {
        public static readonly long[] Value = { 0L, 3L, -2L, 15L, -18L, 76L, -126L, 405L, -802L, 2241L, -4884L, 12696L, -29100L, 72903L, -171490L, 421683L, -1005030L, 2448356L, -5873706L, 14243001L, -34280258L, 82936965L, -199930344L, 483172656L, -1165648152L, 2815517835L, -6794932418L, 16408304343L, -39606671610L, 95629756540L };
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

    public class A291251Enumerable : IEnumerable<byte[]>
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

    public class A291251Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291251.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291251.Bytes);

        public long this[int i] => Value[i];
        
        public static A291251Inst Instance = new A291251Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291250
    {
        public static readonly long[] Value = { 1L, 3L, 4L, 13L, 17L, 52L, 69L, 203L, 272L, 781L, 1053L, 2976L, 4029L, 11267L, 15296L, 42469L, 57765L, 159596L, 217361L, 598499L, 815860L, 2241165L, 3057025L, 8383872L, 11440897L, 31340691L, 42781588L, 117100285L, 159881873L, 437378260L, 597260133L, 1633244795L, 2230504928L, 6097779229L };
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

    public class A291250Enumerable : IEnumerable<byte[]>
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

    public class A291250Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291250.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291250.Bytes);

        public long this[int i] => Value[i];
        
        public static A291250Inst Instance = new A291250Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291249
    {
        public static readonly long[] Value = { 1L, 2L, 5L, 10L, 23L, 47L, 102L, 214L, 452L, 955L, 2003L, 4223L, 8854L, 18610L, 39032L, 81896L, 171752L, 360103L, 754985L, 1582497L, 3316978L, 6951684L, 14568692L, 30530311L, 63977107L, 134063288L, 280920507L, 588643384L, 1233430247L, 2584481968L, 5415381139L, 11347029572L, 23775710791L };
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

    public class A291249Enumerable : IEnumerable<byte[]>
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

    public class A291249Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291249.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291249.Bytes);

        public long this[int i] => Value[i];
        
        public static A291249Inst Instance = new A291249Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291200
    {
        public static readonly long[] Value = { 1L, -1L, -1L, 1L, 1L, -2L, -1L, 4L, 0L, -6L, 3L, 7L, -8L, -6L, 15L, 2L, -24L, 9L, 33L, -32L, -35L, 68L, 20L, -114L, 25L, 164L, -120L, -196L, 285L, 160L, -521L, 16L, 796L, -423L, -1021L, 1166L, 999L, -2310L, -387L, 3774L, -1296L, -5194L, 4608L, 5735L, -10007L, -3870L, 17441L, -2750L, -25635L, 17116L, 31111L };
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

    public class A291200Enumerable : IEnumerable<byte[]>
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

    public class A291200Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291200.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291200.Bytes);

        public long this[int i] => Value[i];
        
        public static A291200Inst Instance = new A291200Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291199
    {
        public static readonly long[] Value = { 2477L, 44287823L, 58192759L, 110369351L, 664009019L, 2574106333L, 6870260119L, 7423240007L, 60370077539L, 188271042191L, 235399729007L, 236767359977L, 305214702643L, 717724689959L };
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

    public class A291199Enumerable : IEnumerable<byte[]>
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

    public class A291199Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291199.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291199.Bytes);

        public long this[int i] => Value[i];
        
        public static A291199Inst Instance = new A291199Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291198
    {
        public static readonly BigInteger[] Value = { 1L, 1L, -4L, 44L, -704L, 14208L, -338304L, 9165696L, -276371712L, 9139825152L, -328127311872L, 12691848990720L, -525849764782080L, 23229272756912128L, -1089877362984222720L, BigInteger.Parse("54133294598753206272"), BigInteger.Parse("-2838256094009499844608"), BigInteger.Parse("156685554517473518682112"), BigInteger.Parse("-9086394132461874613059584"), BigInteger.Parse("552356609566876038974144512"), BigInteger.Parse("-35128905989147663752065187840"), BigInteger.Parse("2333138175889736609287142113280") };
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
    }

    public class A291198Enumerable : IEnumerable<byte[]>
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

    public class A291198Inst : IEnumerable<BigInteger>
    {
        public static readonly BigInteger[] Value = A291198.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291198.Bytes);

        public BigInteger this[int i] => Value[i];
        
        public static A291198Inst Instance = new A291198Inst();
        
        public IEnumerator<BigInteger> GetEnumerator()
        {
            return (Value as IEnumerable<BigInteger>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291197
    {
        public static readonly long[] Value = { 0L, -1L, 1L, 0L, 0L, -1L, 1L, 0L, 0L, -1L, 1L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, -1L, 1L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, -1L, 1L, 0L, 0L, 0L, 0L, -1L };
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

    public class A291197Enumerable : IEnumerable<byte[]>
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

    public class A291197Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291197.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291197.Bytes);

        public long this[int i] => Value[i];
        
        public static A291197Inst Instance = new A291197Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291196
    {
        public static readonly long[] Value = { 1L, 0L, -1L, 2L, -3L, 3L, -2L, 1L, 0L, 0L, -1L, 1L, -1L, 2L, -2L, 0L, 2L, -2L, 1L, 0L, -1L, 1L, 0L, -1L, 1L, 0L, 0L, -1L, 2L, -3L, 3L, -2L, 1L, 0L, -1L, 1L, 0L, -1L, 2L, -2L, 0L, 2L, -2L, 1L, -1L, 2L, -2L, 0L, 2L, -2L, 1L, -1L, 1L, 0L, 0L, 0L, -1L, 1L, -1L, 2L, -2L, 0L, 2L, -2L, 1L, -1L, 2L, -2L, 0L, 2L, -2L, 1L, 0L, -1L, 1L, 0L, -1L, 2L, -3L, 3L, -2L, 1L, 0L, 0L, -1L, 1L, 0L, -1L };
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

    public class A291196Enumerable : IEnumerable<byte[]>
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

    public class A291196Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291196.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291196.Bytes);

        public long this[int i] => Value[i];
        
        public static A291196Inst Instance = new A291196Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291195
    {
        public static readonly long[] Value = { 1L, 1L, 0L, 1L, 0L, 1L, 0L, 1L, 1L, 1L, 0L, 1L, 1L, 0L, 1L, 1L, 1L, 0L, 1L, 1L, 0L, 1L, 0L, 1L, 0L, 1L, 1L, 1L, 0L, 1L, 1L, 0L, 1L, 1L, 1L, 0L, 1L, 1L, 0L, 1L, 0L, 1L, 0L, 1L, 1L, 1L, 0L, 1L, 1L, 0L, 1L, 1L, 1L, 0L, 1L, 0L, 0L, 0L, 0L, 1L, 0L, 1L, 1L, 1L, 0L, 1L, 1L, 0L, 1L, 1L, 1L, 0L, 1L, 0L, 0L, 0L, 0L, 1L, 0L, 1L, 1L, 1L, 1L, 1L, 1L, 0L, 1L, 1L, 1L, 0L, 1L, 0L, 0L, 0L, 0L, 1L, 0L, 1L, 1L, 1L, 0L, 2L, 1L, 0L, 1L };
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

    public class A291195Enumerable : IEnumerable<byte[]>
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

    public class A291195Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291195.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291195.Bytes);

        public long this[int i] => Value[i];
        
        public static A291195Inst Instance = new A291195Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291194
    {
        public static readonly long[] Value = { 1093L, 3511L, 398945L, 796797L, 1194649L, 1592501L, 1990353L, 2388205L, 2786057L, 3183909L, 3581761L, 3979613L, 4377465L, 4775317L, 5173169L, 5571021L, 5968873L, 6165316L, 6366725L, 6764577L, 7162429L, 7560281L, 7958133L, 8355985L, 8753837L, 9151689L, 9549541L, 9947393L, 10345245L };
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

    public class A291194Enumerable : IEnumerable<byte[]>
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

    public class A291194Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291194.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291194.Bytes);

        public long this[int i] => Value[i];
        
        public static A291194Inst Instance = new A291194Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291193
    {
        public static readonly long[] Value = { 1L, 1L, -1L, -1L, 1L, 2L, -1L, -4L, 0L, 6L, 3L, -7L, -8L, 6L, 15L, -2L, -24L, -9L, 33L, 32L, -35L, -68L, 20L, 114L, 25L, -164L, -120L, 196L, 285L, -160L, -521L, -16L, 796L, 423L, -1021L, -1166L, 999L, 2310L, -387L, -3774L, -1296L, 5194L, 4608L, -5735L, -10007L, 3870L, 17441L, 2750L, -25635L, -17116L, 31111L };
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

    public class A291193Enumerable : IEnumerable<byte[]>
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

    public class A291193Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291193.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291193.Bytes);

        public long this[int i] => Value[i];
        
        public static A291193Inst Instance = new A291193Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291192
    {
        public static readonly long[] Value = { 6L, 42L, 1430L, 3686L, 5685L, 23815L, 60235L, 129778L, 370991L, 652289L, 654545L, 660265L, 795405L, 801645L, 1532170L, 3413267L, 3457597L, 4235270L, 4282330L, 8107937L, 9679187L, 10835013L, 15464685L, 15963578L, 16636503L, 24976497L, 28122458L, 29595310L, 34759879L, 35642479L, 58525286L };
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

    public class A291192Enumerable : IEnumerable<byte[]>
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

    public class A291192Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291192.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291192.Bytes);

        public long this[int i] => Value[i];
        
        public static A291192Inst Instance = new A291192Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291191
    {
        public static readonly long[] Value = { 1L, 1L, 1L, 2L, 2L, 1L, 1L, 2L, 2L, 2L, 1L, 1L, 3L, 2L, 2L, 1L, 2L, 2L, 1L, 1L, 3L, 3L, 1L, 2L, 2L, 1L, 3L, 2L, 2L, 4L, 4L, 3L, 4L, 2L, 2L, 5L, 5L, 5L, 4L, 5L, 3L, 6L, 7L, 4L, 7L, 5L, 3L, 6L, 6L, 5L, 4L, 10L, 5L, 3L, 3L, 3L, 4L, 6L, 4L, 4L, 4L, 4L, 1L, 5L, 5L, 3L, 3L, 4L, 3L, 3L, 3L, 3L, 4L, 2L, 1L, 2L, 3L, 4L, 2L, 7L, 4L, 4L, 2L, 4L, 6L, 5L, 2L, 3L, 6L, 3L, 3L, 3L, 2L, 5L, 3L, 2L, 3L, 1L, 6L, 5L };
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

    public class A291191Enumerable : IEnumerable<byte[]>
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

    public class A291191Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291191.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291191.Bytes);

        public long this[int i] => Value[i];
        
        public static A291191Inst Instance = new A291191Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291190
    {
        public static readonly long[] Value = { 1L, 1L, 2L, 4L, 12L, 36L, 112L, 361L, 1186L, 3974L, 13524L, 46612L, 162384L, 570880L, 2022800L, 7216480L, 25900036L, 93449752L, 338772408L, 1233326352L, 4507204720L, 16528765376L, 60805491392L, 224335046602L, 829851744732L, 3077246265612L, 11436732740472L, 42593968518536L, 158941264247584L, 594169284671232L, 2224933015422432L, 8344687554060528L, 31343475208937024L, 117893400330845424L, 444019302263216224L };
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

    public class A291190Enumerable : IEnumerable<byte[]>
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

    public class A291190Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291190.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291190.Bytes);

        public long this[int i] => Value[i];
        
        public static A291190Inst Instance = new A291190Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291189
    {
        public static readonly long[] Value = { 1L, 1L, 2L, 6L, 16L, 48L, 152L, 501L, 1690L, 5822L, 20388L, 72360L, 259688L, 940792L, 3435904L, 12636554L, 46760376L, 173971252L, 650380288L, 2441905192L, 9203979808L, 34813551616L, 132101846848L, 502732914346L, 1918353118348L, 7338208929260L, 28134551443480L, 108094972590872L, 416122805092224L, 1604832481200352L, 6199797669769760L, 23989294121910790L, 92962226232374892L, 360749306397285812L };
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

    public class A291189Enumerable : IEnumerable<byte[]>
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

    public class A291189Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291189.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291189.Bytes);

        public long this[int i] => Value[i];
        
        public static A291189Inst Instance = new A291189Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291188
    {
        public static readonly long[] Value = { 2L, 49L, 1673187L, 4743933602050718L };
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

    public class A291188Enumerable : IEnumerable<byte[]>
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

    public class A291188Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291188.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291188.Bytes);

        public long this[int i] => Value[i];
        
        public static A291188Inst Instance = new A291188Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291187
    {
        public static readonly BigInteger[] Value = { 288L, 28200960L, 29136487207403520L, BigInteger.Parse("1903816047972624930994913280000") };
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
    }

    public class A291187Enumerable : IEnumerable<byte[]>
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

    public class A291187Inst : IEnumerable<BigInteger>
    {
        public static readonly BigInteger[] Value = A291187.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291187.Bytes);

        public BigInteger this[int i] => Value[i];
        
        public static A291187Inst Instance = new A291187Inst();
        
        public IEnumerator<BigInteger> GetEnumerator()
        {
            return (Value as IEnumerable<BigInteger>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291185
    {
        public static readonly long[] Value = { 1L, 2L, 2L, 6L, 6L, 6L, 12L, 30L, 30L, 60L, 120L, 210L, 420L, 420L, 840L, 2520L, 9240L, 9240L, 27720L, 55440L, 120120L, 360360L, 720720L, 2162160L, 6126120L, 12252240L, 36756720L, 116396280L, 232792560L, 698377680L, 2677114440L, 5354228880L, 26771144400L, 155272637520L, 465817912560L };
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

    public class A291185Enumerable : IEnumerable<byte[]>
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

    public class A291185Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291185.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291185.Bytes);

        public long this[int i] => Value[i];
        
        public static A291185Inst Instance = new A291185Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291184
    {
        public static readonly long[] Value = { 4L, 21L, 104L, 507L, 2452L, 11808L, 56732L, 272229L, 1305400L, 6257355L, 29988140L, 143701056L, 688563508L, 3299237877L, 15807943688L, 75741312603L, 362900797636L, 1738768378464L, 8330956025036L, 39916050834885L, 191249400483544L, 916331219497131L, 4390407398410844L };
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

    public class A291184Enumerable : IEnumerable<byte[]>
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

    public class A291184Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291184.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291184.Bytes);

        public long this[int i] => Value[i];
        
        public static A291184Inst Instance = new A291184Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291098
    {
        public static readonly long[] Value = { 1L, 4L, 12L, 266L };
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

    public class A291098Enumerable : IEnumerable<byte[]>
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

    public class A291098Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291098.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291098.Bytes);

        public long this[int i] => Value[i];
        
        public static A291098Inst Instance = new A291098Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291097
    {
        public static readonly long[] Value = { 3L, 8L, 20L, 47L, 106L, 233L, 504L, 1079L, 2294L, 4853L, 10228L, 21491L, 45042L, 94193L, 196592L, 409583L, 851950L, 1769453L, 3669996L, 7602155L, 15728618L, 32505833L, 67108840L, 138412007L, 285212646L, 587202533L, 1207959524L, 2483027939L, 5100273634L, 10468982753L, 21474836448L, 44023414751L };
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

    public class A291097Enumerable : IEnumerable<byte[]>
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

    public class A291097Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291097.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291097.Bytes);

        public long this[int i] => Value[i];
        
        public static A291097Inst Instance = new A291097Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291096
    {
        public static readonly BigInteger[] Value = { 1L, 3L, 36L, 594L, 11340L, 235467L, 5164236L, 117704340L, 2760422652L, 66179363580L, 1614629242512L, 39958835859306L, 1000667989897524L, 25310418084553770L, 645671000841035400L, 16592979103827051240UL, BigInteger.Parse("429173117580596633820"), BigInteger.Parse("11163550152596460675012"), BigInteger.Parse("291848008677713303547312") };
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
    }

    public class A291096Enumerable : IEnumerable<byte[]>
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

    public class A291096Inst : IEnumerable<BigInteger>
    {
        public static readonly BigInteger[] Value = A291096.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291096.Bytes);

        public BigInteger this[int i] => Value[i];
        
        public static A291096Inst Instance = new A291096Inst();
        
        public IEnumerator<BigInteger> GetEnumerator()
        {
            return (Value as IEnumerable<BigInteger>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291095
    {
        public static readonly BigInteger[] Value = { 3L, 3L, 878L, 11404L, 11404L, 595413L, 1797640L, 98274734L, 198347106L, 8128636028L, 75041122922L, 922797637351L, 6759747953135L, 28036830572808L, 1213341301344107L, 19027704941439533L, 71928417857731452L, 240751079727999028L, 5127701092145711019L, BigInteger.Parse("81320964235147379208"), BigInteger.Parse("1224942164619356399124") };
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
    }

    public class A291095Enumerable : IEnumerable<byte[]>
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

    public class A291095Inst : IEnumerable<BigInteger>
    {
        public static readonly BigInteger[] Value = A291095.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291095.Bytes);

        public BigInteger this[int i] => Value[i];
        
        public static A291095Inst Instance = new A291095Inst();
        
        public IEnumerator<BigInteger> GetEnumerator()
        {
            return (Value as IEnumerable<BigInteger>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291094
    {
        public static readonly long[] Value = { 64L, 65L, 95L, 98L, 110L, 120L, 121L, 130L, 132L, 136L, 140L, 143L, 150L, 154L, 160L, 160L, 165L, 170L, 176L, 180L, 187L, 190L, 190L, 192L, 194L, 195L, 196L, 196L, 198L, 202L, 204L, 206L, 208L, 210L, 220L, 220L, 230L, 231L, 231L, 238L, 238L, 240L, 242L, 242L, 250L };
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

    public class A291094Enumerable : IEnumerable<byte[]>
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

    public class A291094Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291094.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291094.Bytes);

        public long this[int i] => Value[i];
        
        public static A291094Inst Instance = new A291094Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291093
    {
        public static readonly long[] Value = { 16L, 26L, 19L, 49L, 11L, 12L, 22L, 13L, 33L, 34L, 14L, 44L, 15L, 55L, 16L, 64L, 66L, 17L, 77L, 18L, 88L, 19L, 95L, 96L, 97L, 39L, 49L, 98L, 99L, 101L, 102L, 103L, 104L, 21L, 22L, 121L, 23L, 33L, 132L, 34L, 136L, 24L, 44L, 143L, 25L, 55L, 154L, 26L, 65L, 66L, 165L, 106L, 67L, 27L, 77L, 176L, 28L, 88L, 187L, 29L };
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

    public class A291093Enumerable : IEnumerable<byte[]>
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

    public class A291093Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291093.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291093.Bytes);

        public long this[int i] => Value[i];
        
        public static A291093Inst Instance = new A291093Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291092
    {
        public static readonly long[] Value = { 1L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L, 9L };
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

    public class A291092Enumerable : IEnumerable<byte[]>
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

    public class A291092Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291092.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291092.Bytes);

        public long this[int i] => Value[i];
        
        public static A291092Inst Instance = new A291092Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291091
    {
        public static readonly long[] Value = { 1L, 2L, 7L, 28L, 122L, 562L, 2693L, 13288L, 67064L, 344588L, 1796518L, 9479780L, 50532640L, 271710662L, 1471935235L, 8026070768L, 44015873308L, 242619318848L, 1343427572648L, 7469219870968L, 41680871386016L, 233373274580372L, 1310659959443722L, 7381448319246248L, 41678055955034962L };
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

    public class A291091Enumerable : IEnumerable<byte[]>
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

    public class A291091Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291091.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291091.Bytes);

        public long this[int i] => Value[i];
        
        public static A291091Inst Instance = new A291091Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291090
    {
        public static readonly long[] Value = { 1L, 1L, 3L, 11L, 46L, 207L, 977L, 4769L, 23872L, 121862L, 631958L, 3319923L, 17630692L, 94493713L, 510468519L, 2776629563L, 15194389388L, 83591476528L, 462062822648L, 2564995473974L, 14293435176216L, 79927207249606L, 448358398361618L, 2522381161938591L, 14228119729773226L };
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

    public class A291090Enumerable : IEnumerable<byte[]>
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

    public class A291090Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291090.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291090.Bytes);

        public long this[int i] => Value[i];
        
        public static A291090Inst Instance = new A291090Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291089
    {
        public static readonly long[] Value = { 1L, 4L, 20L, 104L, 556L, 3032L, 16778L, 93872L, 529684L, 3008864L, 17184188L, 98577712L, 567591142L, 3278348608L, 18986482250L, 110217131168L, 641125473092L, 3736134109936L, 21807240851480L, 127469052615104L, 746057665449076L, 4371699398312704L, 25644387465768860L };
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

    public class A291089Enumerable : IEnumerable<byte[]>
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

    public class A291089Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291089.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291089.Bytes);

        public long this[int i] => Value[i];
        
        public static A291089Inst Instance = new A291089Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291088
    {
        public static readonly long[] Value = { 1L, 2L, 8L, 38L, 196L, 1052L, 5774L, 32146L, 180772L, 1024256L, 5837908L, 33433996L, 192239854L, 1109049320L, 6416509142L, 37215072638L, 216309089956L, 1259663964184L, 7347943049432L, 42926944354480L, 251119894730596L, 1470830479288432L, 8624336421678788L, 50620679934081988L };
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

    public class A291088Enumerable : IEnumerable<byte[]>
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

    public class A291088Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291088.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291088.Bytes);

        public long this[int i] => Value[i];
        
        public static A291088Inst Instance = new A291088Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291087
    {
        public static readonly long[] Value = { 1L, 1L, 2L, 3L, 2L, 1L, 7L, 11L, 10L, 7L, 3L, 1L, 28L, 46L, 48L, 39L, 24L, 12L, 4L, 1L, 122L, 207L, 233L, 208L, 151L, 92L, 45L, 18L, 5L, 1L, 562L, 977L, 1154L, 1099L, 880L, 606L, 356L, 179L, 74L, 25L, 6L, 1L, 2693L, 4769L, 5826L, 5815L, 4975L, 3726L, 2451L, 1419L, 714L, 310L, 112L, 33L, 7L, 1L, 13288L, 23872L, 29904L, 30926L, 27768L, 22112L, 15736L, 10039L, 5720L, 2898L, 1288L, 496L, 160L, 42L, 8L, 1L };
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

    public class A291087Enumerable : IEnumerable<byte[]>
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

    public class A291087Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291087.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291087.Bytes);

        public long this[int i] => Value[i];
        
        public static A291087Inst Instance = new A291087Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291086
    {
        public static readonly long[] Value = { 1L, 1L, 1L, 1L, 3L, 4L, 4L, 2L, 1L, 11L, 17L, 18L, 13L, 8L, 3L, 1L, 46L, 76L, 85L, 72L, 51L, 28L, 13L, 4L, 1L, 207L, 355L, 415L, 384L, 300L, 196L, 110L, 50L, 19L, 5L, 1L, 977L, 1716L, 2076L, 2034L, 1705L, 1236L, 785L, 430L, 204L, 80L, 26L, 6L, 1L, 4769L, 8519L, 10584L, 10801L, 9541L, 7426L, 5145L, 3165L, 1729L, 826L, 343L, 119L, 34L, 7L, 1L };
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

    public class A291086Enumerable : IEnumerable<byte[]>
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

    public class A291086Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291086.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291086.Bytes);

        public long this[int i] => Value[i];
        
        public static A291086Inst Instance = new A291086Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291085
    {
        public static readonly long[] Value = { 1L, 1L, 4L, 4L, 2L, 1L, 20L, 19L, 13L, 8L, 3L, 1L, 104L, 98L, 76L, 52L, 28L, 13L, 4L, 1L, 556L, 526L, 434L, 319L, 201L, 111L, 50L, 19L, 5L, 1L, 3032L, 2887L, 2470L, 1910L, 1316L, 811L, 436L, 205L, 80L, 26L, 6L, 1L, 16778L, 16073L, 14085L, 11304L, 8259L, 5489L, 3284L, 1763L, 833L, 344L, 119L, 34L, 7L, 1L, 93872L, 90386L, 80584L, 66514L, 50680L, 35588L, 22912L, 13476L, 7176L, 3437L, 1456L, 539L, 168L, 43L, 8L, 1L };
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

    public class A291085Enumerable : IEnumerable<byte[]>
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

    public class A291085Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291085.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291085.Bytes);

        public long this[int i] => Value[i];
        
        public static A291085Inst Instance = new A291085Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291084
    {
        public static readonly long[] Value = { 1L, 2L, 1L, 1L, 8L, 6L, 5L, 2L, 1L, 38L, 33L, 27L, 16L, 9L, 3L, 1L, 196L, 180L, 150L, 104L, 65L, 32L, 14L, 4L, 1L, 1052L, 990L, 845L, 635L, 430L, 251L, 130L, 55L, 20L, 5L, 1L, 5774L, 5502L, 4797L, 3786L, 2721L, 1752L, 1016L, 516L, 231L, 86L, 27L, 6L, 1L, 32146L, 30863L, 27377L, 22344L, 16793L, 11543L, 7252L, 4117L, 2107L, 952L, 378L, 126L, 35L, 7L, 1L };
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

    public class A291084Enumerable : IEnumerable<byte[]>
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

    public class A291084Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291084.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291084.Bytes);

        public long this[int i] => Value[i];
        
        public static A291084Inst Instance = new A291084Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291083
    {
        public static readonly long[] Value = { 1L, 1L, 4L, 5L, 3L, 1L, 21L, 30L, 25L, 14L, 5L, 1L, 127L, 196L, 189L, 133L, 70L, 27L, 7L, 1L, 835L, 1353L, 1422L, 1140L, 726L, 369L, 147L, 44L, 9L, 1L, 5798L, 9713L, 10813L, 9438L, 6765L, 4037L, 2002L, 814L, 264L, 65L, 11L, 1L, 41835L, 71799L, 83304L, 77220L, 60060L, 39897L, 22737L, 11076L, 4563L, 1560L, 429L, 90L, 13L, 1L };
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

    public class A291083Enumerable : IEnumerable<byte[]>
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

    public class A291083Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291083.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291083.Bytes);

        public long this[int i] => Value[i];
        
        public static A291083Inst Instance = new A291083Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291023
    {
        public static readonly long[] Value = { 0L, 3L, 4L, 12L, 24L, 56L, 120L, 264L, 568L, 1224L, 2616L, 5576L, 11832L, 25032L, 52792L, 111048L, 233016L, 487880L, 1019448L, 2126280L, 4427320L, 9204168L, 19107384L, 39612872L, 82021944L, 169636296L, 350457400L, 723284424L, 1491308088L, 3072094664L, 6323146296L, 13004206536L, 26724240952L };
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

    public class A291023Enumerable : IEnumerable<byte[]>
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

    public class A291023Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291023.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291023.Bytes);

        public long this[int i] => Value[i];
        
        public static A291023Inst Instance = new A291023Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291022
    {
        public static readonly long[] Value = { 6L, 12L, 18L, 20L, 24L, 30L, 36L, 40L, 42L, 48L, 54L, 80L, 96L, 100L, 108L, 140L, 150L, 156L, 160L, 162L, 192L, 198L, 200L, 220L, 264L, 272L, 280L, 294L, 312L, 320L, 324L, 342L, 384L, 396L, 400L, 440L, 486L, 500L, 510L, 520L, 528L, 544L, 546L, 560L, 624L, 640L, 684L, 702L, 714L, 750L, 768L, 798L, 800L, 880L, 912L };
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

    public class A291022Enumerable : IEnumerable<byte[]>
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

    public class A291022Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291022.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291022.Bytes);

        public long this[int i] => Value[i];
        
        public static A291022Inst Instance = new A291022Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291021
    {
        public static readonly long[] Value = { 1L, 3L, 9L, 25L, 67L, 178L, 472L, 1249L, 3297L, 8685L, 22843L, 60014L, 157540L, 413289L, 1083693L, 2840521L, 7443331L, 19500394L, 51079696L, 133782385L, 350354841L, 917456901L, 2402365387L, 6290338310L, 16470047644L, 43122600825L, 112903347237L, 295598625697L, 773914899475L };
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

    public class A291021Enumerable : IEnumerable<byte[]>
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

    public class A291021Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291021.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291021.Bytes);

        public long this[int i] => Value[i];
        
        public static A291021Inst Instance = new A291021Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291020
    {
        public static readonly long[] Value = { 1L, 3L, 9L, 27L, 79L, 228L, 656L, 1889L, 5445L, 15701L, 45275L, 130544L, 376388L, 1085199L, 3128841L, 9021083L, 26009635L, 74991112L, 216214692L, 623391005L, 1797363157L, 5182163781L, 14941232871L, 43078615236L, 124204414928L, 358106605227L, 1032494220505L, 2976890957419L };
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

    public class A291020Enumerable : IEnumerable<byte[]>
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

    public class A291020Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291020.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291020.Bytes);

        public long this[int i] => Value[i];
        
        public static A291020Inst Instance = new A291020Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291019
    {
        public static readonly long[] Value = { 1L, 3L, 9L, 25L, 68L, 185L, 504L, 1373L, 3739L, 10180L, 27714L, 75445L, 205376L, 559064L, 1521840L, 4142609L, 11276581L, 30695881L, 83556891L, 227449066L, 619135745L, 1685339900L, 4587637263L, 12487934387L, 33993205996L, 92532358762L, 251880840375L, 685640764594L, 1866371634554L };
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

    public class A291019Enumerable : IEnumerable<byte[]>
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

    public class A291019Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291019.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291019.Bytes);

        public long this[int i] => Value[i];
        
        public static A291019Inst Instance = new A291019Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291018
    {
        public static readonly long[] Value = { 6L, 41L, 280L, 1912L, 13056L, 89152L, 608768L, 4156928L, 28385280L, 193826816L, 1323532288L, 9037643776L, 61712891904L, 421401985024L, 2877512744960L, 19648886079488L, 134170986676224L, 916176804773888L, 6256046544781312L, 42718957920059392L, 291703291002224640L };
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

    public class A291018Enumerable : IEnumerable<byte[]>
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

    public class A291018Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291018.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291018.Bytes);

        public long this[int i] => Value[i];
        
        public static A291018Inst Instance = new A291018Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291017
    {
        public static readonly long[] Value = { 5L, 29L, 168L, 973L, 5635L, 32634L, 188993L, 1094513L, 6338640L, 36708889L, 212591743L, 1231179978L, 7130117645L, 41292563669L, 239137122168L, 1384911909493L, 8020423511275L, 46448581212474L, 268997103908393L, 1557839658871433L, 9021897884741280L, 52248407581088929L };
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

    public class A291017Enumerable : IEnumerable<byte[]>
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

    public class A291017Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291017.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291017.Bytes);

        public long this[int i] => Value[i];
        
        public static A291017Inst Instance = new A291017Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291016
    {
        public static readonly long[] Value = { 4L, 19L, 90L, 426L, 2016L, 9540L, 45144L, 213624L, 1010880L, 4783536L, 22635936L, 107114400L, 506870784L, 2398538304L, 11350005120L, 53708800896L, 254152774656L, 1202663842560L, 5691066407424L, 26930415389184L, 127436093890560L, 603034071008256L, 2853587862706176L };
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

    public class A291016Enumerable : IEnumerable<byte[]>
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

    public class A291016Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291016.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291016.Bytes);

        public long this[int i] => Value[i];
        
        public static A291016Inst Instance = new A291016Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291015
    {
        public static readonly long[] Value = { 2L, 7L, 23L, 75L, 244L, 793L, 2576L, 8366L, 27167L, 88215L, 286439L, 930072L, 3019941L, 9805712L, 31838986L, 103380599L, 335674791L, 1089929347L, 3538978588L, 11490991649L, 37311016064L, 121148109014L, 393365440335L, 1277249563655L, 4147203285279L, 13465884484800L, 43723452275981L };
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

    public class A291015Enumerable : IEnumerable<byte[]>
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

    public class A291015Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291015.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291015.Bytes);

        public long this[int i] => Value[i];
        
        public static A291015Inst Instance = new A291015Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291014
    {
        public static readonly long[] Value = { 0L, 0L, 2L, 6L, 12L, 23L, 48L, 105L, 228L, 486L, 1026L, 2161L, 4548L, 9555L, 20026L, 41874L, 87384L, 182043L, 378648L, 786429L, 1631120L, 3378750L, 6990510L, 14447045L, 29826156L, 61516455L, 126761190L, 260978922L, 536870916L, 1103567983L, 2266788288L, 4652881233L, 9544371772L, 19565962134L };
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

    public class A291014Enumerable : IEnumerable<byte[]>
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

    public class A291014Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291014.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291014.Bytes);

        public long this[int i] => Value[i];
        
        public static A291014Inst Instance = new A291014Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291013
    {
        public static readonly long[] Value = { 0L, 3L, 6L, 15L, 36L, 85L, 198L, 456L, 1040L, 2352L, 5280L, 11776L, 26112L, 57600L, 126464L, 276480L, 602112L, 1306624L, 2826240L, 6094848L, 13107200L, 28114944L, 60162048L, 128450560L, 273678336L, 581959680L, 1235222528L, 2617245696L, 5536481280L, 11693719552L, 24662507520L, 51942260736L };
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

    public class A291013Enumerable : IEnumerable<byte[]>
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

    public class A291013Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291013.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291013.Bytes);

        public long this[int i] => Value[i];
        
        public static A291013Inst Instance = new A291013Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291012
    {
        public static readonly long[] Value = { 2L, 7L, 22L, 68L, 208L, 632L, 1912L, 5768L, 17368L, 52232L, 156952L, 471368L, 1415128L, 4247432L, 12746392L, 38247368L, 114758488L, 344308232L, 1032990232L, 3099101768L, 9297567448L, 27893226632L, 83680728472L, 251044282568L, 753137042008L, 2259419514632L, 6778275321112L };
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

    public class A291012Enumerable : IEnumerable<byte[]>
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

    public class A291012Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291012.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291012.Bytes);

        public long this[int i] => Value[i];
        
        public static A291012Inst Instance = new A291012Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291011
    {
        public static readonly long[] Value = { 4L, 15L, 52L, 172L, 552L, 1736L, 5384L, 16536L, 50440L, 153112L, 463176L, 1397720L, 4210568L, 12668568L, 38083528L, 114414424L, 343587336L, 1031482904L, 3095956040L, 9291013848L, 27879595144L, 83652416920L, 250985562312L, 753015407192L, 2259167856392L, 6777755227416L, 20333785775944L };
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

    public class A291011Enumerable : IEnumerable<byte[]>
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

    public class A291011Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291011.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291011.Bytes);

        public long this[int i] => Value[i];
        
        public static A291011Inst Instance = new A291011Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291010
    {
        public static readonly long[] Value = { 5L, 24L, 108L, 468L, 1980L, 8244L, 33948L, 138708L, 563580L, 2280564L, 9200988L, 37040148L, 148869180L, 597602484L, 2396787228L, 9606280788L, 38482518780L, 154102262004L, 616925608668L, 2469252116628L, 9881657512380L, 39540577187124L, 158204150161308L, 632942124883668L };
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

    public class A291010Enumerable : IEnumerable<byte[]>
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

    public class A291010Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291010.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291010.Bytes);

        public long this[int i] => Value[i];
        
        public static A291010Inst Instance = new A291010Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291009
    {
        public static readonly long[] Value = { 4L, 17L, 70L, 284L, 1144L, 4592L, 18400L, 73664L, 294784L, 1179392L, 4718080L, 18873344L, 75495424L, 301985792L, 1207951360L, 4831821824L, 19327320064L, 77309345792L, 309237514240L, 1236950319104L, 4947801800704L, 19791208251392L, 79164835102720L, 316659344605184L, 1266637386809344L };
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

    public class A291009Enumerable : IEnumerable<byte[]>
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

    public class A291009Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291009.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291009.Bytes);

        public long this[int i] => Value[i];
        
        public static A291009Inst Instance = new A291009Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A291008
    {
        public static readonly long[] Value = { 0L, 7L, 14L, 70L, 224L, 868L, 3080L, 11368L, 41216L, 150640L, 548576L, 2000992L, 7293440L, 26592832L, 96946304L, 353449600L, 1288577024L, 4697851648L, 17127165440L, 62441440768L, 227645874176L, 829940392960L, 3025756030976L, 11031154419712L, 40216845025280L, 146620616568832L };
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

    public class A291008Enumerable : IEnumerable<byte[]>
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

    public class A291008Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A291008.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A291008.Bytes);

        public long this[int i] => Value[i];
        
        public static A291008Inst Instance = new A291008Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290974
    {
        public static readonly BigInteger[] Value = { 1L, -1L, 7L, -217L, 27559L, -14082649L, 28827182503L, -236123451882073L, 7737057147819885991L, BigInteger.Parse("-1014103817421900276726361"), BigInteger.Parse("531681448124675830384033629607"), BigInteger.Parse("-1115016280616112042365706510363949657"), BigInteger.Parse("9353433376690281791373262192784600640357799") };
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
    }

    public class A290974Enumerable : IEnumerable<byte[]>
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

    public class A290974Inst : IEnumerable<BigInteger>
    {
        public static readonly BigInteger[] Value = A290974.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290974.Bytes);

        public BigInteger this[int i] => Value[i];
        
        public static A290974Inst Instance = new A290974Inst();
        
        public IEnumerator<BigInteger> GetEnumerator()
        {
            return (Value as IEnumerable<BigInteger>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290973
    {
        public static readonly long[] Value = { -2L, 1L, 2L, 3L, 4L, 6L, 6L, 10L, 8L, 15L, 10L, 25L, 12L, 28L, 10L, 60L, 16L, 25L, 18L, 125L, 0L, 66L, 22L, 218L, 24L, 91L, -30L, 420L, 28L, -387L, 30L, 2011L, -88L, 153L, 28L, -1894L, 36L, 190L, -182L, 8902L, 40L, -3234L, 42L, 2398L, -132L, 276L, 46L, 2340L, 48L, -2678L, -510L, 4641L, 52L, -1754L, -198L, 108400L };
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

    public class A290973Enumerable : IEnumerable<byte[]>
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

    public class A290973Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290973.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290973.Bytes);

        public long this[int i] => Value[i];
        
        public static A290973Inst Instance = new A290973Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290972
    {
        public static readonly long[] Value = { 2L, 3L, 3331L, 3433L, 11243L, 13241L, 21523L, 22153L, 22531L, 31541L, 32141L, 32411L, 33203L, 34033L, 34141L, 34211L, 35141L, 41341L, 41413L, 42131L, 43411L, 44131L, 51341L, 51413L, 52321L, 54311L, 102253L, 102523L, 104231L, 104513L, 110543L, 111263L, 111623L, 112163L };
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

    public class A290972Enumerable : IEnumerable<byte[]>
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

    public class A290972Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290972.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290972.Bytes);

        public long this[int i] => Value[i];
        
        public static A290972Inst Instance = new A290972Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290971
    {
        public static readonly long[] Value = { 1L, 2L, 0L, 6L, 0L, -6L, 0L, 54L, 0L, -30L, 0L, -114L, 0L, -126L, 0L, 4470L, 0L, -294L, 0L, -5850L, 0L, -2046L, 0L, -92418L, 0L, -8190L, 0L, -247674L, 0L, 2010L, 0L, 30229110L, 0L, -131070L, 0L, -8200914L, 0L, -524286L, 0L, -362617770L, 0L, 183162L, 0L, -354416634L, 0L, -8388606L, 0L, -53614489794L, 0L };
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

    public class A290971Enumerable : IEnumerable<byte[]>
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

    public class A290971Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290971.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290971.Bytes);

        public long this[int i] => Value[i];
        
        public static A290971Inst Instance = new A290971Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290970
    {
        public static readonly long[] Value = { 3L, 15L, 233L, 53081L };
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

    public class A290970Enumerable : IEnumerable<byte[]>
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

    public class A290970Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290970.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290970.Bytes);

        public long this[int i] => Value[i];
        
        public static A290970Inst Instance = new A290970Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290969
    {
        public static readonly long[] Value = { 1L, 7L, 31L, 32767L, 4095L, 435356467L, 16777215L, 68719476735L, 281474976710655L };
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

    public class A290969Enumerable : IEnumerable<byte[]>
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

    public class A290969Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290969.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290969.Bytes);

        public long this[int i] => Value[i];
        
        public static A290969Inst Instance = new A290969Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290968
    {
        public static readonly long[] Value = { 1L, 1L, 1L, -1L, 1L, 1L, 5L, 5L, 9L, 11L, 21L, 33L, 57L, 89L, 145L, 231L, 377L, 609L, 989L, 1597L, 2585L, 4179L, 6765L, 10945L, 17713L, 28657L, 46369L, 75023L, 121393L, 196417L, 317813L, 514229L, 832041L, 1346267L, 2178309L, 3524577L, 5702889L, 9227465L, 14930353L, 24157815L };
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

    public class A290968Enumerable : IEnumerable<byte[]>
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

    public class A290968Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290968.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290968.Bytes);

        public long this[int i] => Value[i];
        
        public static A290968Inst Instance = new A290968Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290967
    {
        public static readonly long[] Value = { 929L, 3833L, 4079L, 6737L, 6983L, 7229L, 8369L, 9887L, 10133L, 11273L, 11519L, 13037L, 14177L, 14423L, 14669L, 15809L, 17327L, 17573L, 18713L, 18959L, 20477L, 21617L, 21863L, 22109L, 24767L, 25013L, 27917L };
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

    public class A290967Enumerable : IEnumerable<byte[]>
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

    public class A290967Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290967.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290967.Bytes);

        public long this[int i] => Value[i];
        
        public static A290967Inst Instance = new A290967Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290966
    {
        public static readonly long[] Value = { 1L, 1L, 3L, 3L, 6L, 6L, 9L, 9L, 12L, 12L, 15L, 15L, 19L, 19L, 23L, 23L, 27L, 27L, 31L, 31L, 35L, 35L, 40L, 40L, 45L, 45L, 50L, 50L, 55L, 55L, 60L, 60L, 65L, 65L, 70L, 70L, 75L, 75L, 80L, 80L, 85L, 85L, 90L, 90L, 95L, 95L, 100L, 100L, 105L, 105L, 110L, 110L, 116L, 116L, 122L, 122L, 129L, 129L, 135L, 135L };
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

    public class A290966Enumerable : IEnumerable<byte[]>
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

    public class A290966Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290966.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290966.Bytes);

        public long this[int i] => Value[i];
        
        public static A290966Inst Instance = new A290966Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290965
    {
        public static readonly long[] Value = { 6L, 12L, 15L, 18L, 21L, 24L, 30L, 35L, 36L, 42L, 45L, 48L, 54L, 55L, 60L, 63L, 65L, 66L, 70L, 72L, 75L, 77L, 78L, 84L, 85L, 90L, 91L, 95L, 96L, 102L, 105L, 108L, 110L, 114L, 115L, 119L, 120L, 126L, 130L, 132L, 133L, 135L, 138L, 140L, 143L, 144L, 147L, 150L, 154L, 156L, 161L, 162L, 165L, 168L, 170L, 174L, 175L, 180L, 182L, 186L, 187L, 189L, 190L, 192L, 195L, 198L, 203L };
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

    public class A290965Enumerable : IEnumerable<byte[]>
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

    public class A290965Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290965.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290965.Bytes);

        public long this[int i] => Value[i];
        
        public static A290965Inst Instance = new A290965Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290964
    {
        public static readonly long[] Value = { 3L, 5L, 6L, 14L, 24L, 84L, 87L, 207L, 734L, 797L, 1743L, 2211L, 3539L, 5871L, 5949L, 6954L, 8309L, 10896L, 12771L, 22382L, 35112L, 38267L, 69866L };
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

    public class A290964Enumerable : IEnumerable<byte[]>
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

    public class A290964Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290964.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290964.Bytes);

        public long this[int i] => Value[i];
        
        public static A290964Inst Instance = new A290964Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290963
    {
        public static readonly long[] Value = { 3L, 7L, 29L, 41L, 53L, 59L, 71L, 83L, 89L, 113L, 131L, 137L, 149L, 157L, 167L, 173L, 179L, 197L, 199L, 227L, 233L, 239L, 251L, 263L, 269L, 281L, 293L, 317L, 347L, 379L, 401L, 409L, 419L, 431L, 457L, 463L, 467L, 479L, 491L, 503L, 509L, 521L, 569L, 617L, 619L, 641L, 643L, 647L, 661L, 677L, 691L, 701L, 733L, 743L, 757L, 761L, 769L, 797L, 823L, 829L, 859L, 883L, 911L };
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

    public class A290963Enumerable : IEnumerable<byte[]>
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

    public class A290963Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290963.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290963.Bytes);

        public long this[int i] => Value[i];
        
        public static A290963Inst Instance = new A290963Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290962
    {
        public static readonly long[] Value = { 1L, 2L, 4L, 5L, 8L, 12L, 55L, 125L, 136L, 221L, 224L, 668L, 1254L, 2639L, 4745L, 5888L, 8526L, 9139L, 13771L, 17936L, 27713L, 38668L, 44680L, 73891L };
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

    public class A290962Enumerable : IEnumerable<byte[]>
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

    public class A290962Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290962.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290962.Bytes);

        public long this[int i] => Value[i];
        
        public static A290962Inst Instance = new A290962Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290961
    {
        public static readonly BigInteger[] Value = { 1L, 1L, 2L, 6L, 24L, 840L, 720L, 5040L, 40320L, 59814720L, 3628800L, 83701537920L, 479001600L, 26980643289600L, 2642646473026560L, 1307674368000L, 20922789888000L, BigInteger.Parse("41837259585747225600"), 6402373705728000L, BigInteger.Parse("598354114828973074790400"), BigInteger.Parse("18160977780223038067507200") };
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
    }

    public class A290961Enumerable : IEnumerable<byte[]>
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

    public class A290961Inst : IEnumerable<BigInteger>
    {
        public static readonly BigInteger[] Value = A290961.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290961.Bytes);

        public BigInteger this[int i] => Value[i];
        
        public static A290961Inst Instance = new A290961Inst();
        
        public IEnumerator<BigInteger> GetEnumerator()
        {
            return (Value as IEnumerable<BigInteger>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290960
    {
        public static readonly long[] Value = { 8L, 32L, 56L, 64L, 96L, 128L, 144L, 155L, 170L, 176L, 192L, 196L, 204L, 215L, 221L, 224L, 238L, 248L, 255L, 256L, 272L, 288L, 320L, 322L, 336L, 341L, 352L, 368L, 372L, 374L, 384L, 432L, 448L, 465L, 476L, 496L, 510L, 512L, 527L, 544L, 574L, 576L, 608L, 612L, 623L, 635L, 640L, 644L, 645L, 658L, 663L, 672L, 682L, 697L, 704L, 714L, 731L, 736L, 744L };
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

    public class A290960Enumerable : IEnumerable<byte[]>
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

    public class A290960Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290960.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290960.Bytes);

        public long this[int i] => Value[i];
        
        public static A290960Inst Instance = new A290960Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290959
    {
        public static readonly long[] Value = { 1L, 2L, 3L, 5L, 7L, 11L, 13L, 17L, 20L, 24L, 26L, 32L, 34L, 38L, 42L, 47L, 49L, 55L, 57L, 63L, 67L, 71L, 73L, 81L, 84L, 88L };
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

    public class A290959Enumerable : IEnumerable<byte[]>
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

    public class A290959Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290959.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290959.Bytes);

        public long this[int i] => Value[i];
        
        public static A290959Inst Instance = new A290959Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290906
    {
        public static readonly long[] Value = { 0L, 3L, 12L, 39L, 132L, 456L, 1572L, 5409L, 18612L, 64053L, 220440L, 758640L, 2610840L, 8985147L, 30922188L, 106418031L, 366235308L, 1260390744L, 4337606988L, 14927778921L, 51373622388L, 176801189997L, 608457401520L, 2093992746720L, 7206429919920L, 24800769855603L, 85351303248012L };
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

    public class A290906Enumerable : IEnumerable<byte[]>
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

    public class A290906Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290906.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290906.Bytes);

        public long this[int i] => Value[i];
        
        public static A290906Inst Instance = new A290906Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290905
    {
        public static readonly long[] Value = { 0L, 1L, 4L, 12L, 36L, 111L, 344L, 1064L, 3288L, 10161L, 31404L, 97060L, 299980L, 927135L, 2865456L, 8856144L, 27371312L, 84595361L, 261455316L, 808068924L, 2497464564L, 7718808463L, 23856195976L, 73731339384L, 227878342920L, 704293989585L, 2176731748220L, 6727533066836L, 20792502889884L };
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

    public class A290905Enumerable : IEnumerable<byte[]>
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

    public class A290905Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290905.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290905.Bytes);

        public long this[int i] => Value[i];
        
        public static A290905Inst Instance = new A290905Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290904
    {
        public static readonly long[] Value = { 0L, 2L, 8L, 24L, 72L, 222L, 688L, 2128L, 6576L, 20322L, 62808L, 194120L, 599960L, 1854270L, 5730912L, 17712288L, 54742624L, 169190722L, 522910632L, 1616137848L, 4994929128L, 15437616926L, 47712391952L, 147462678768L, 455756685840L, 1408587979170L, 4353463496440L, 13455066133672L };
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

    public class A290904Enumerable : IEnumerable<byte[]>
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

    public class A290904Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290904.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290904.Bytes);

        public long this[int i] => Value[i];
        
        public static A290904Inst Instance = new A290904Inst();
        
        public IEnumerator<long> GetEnumerator()
        {
            return (Value as IEnumerable<long>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }

    public static class A290903
    {
        public static readonly long[] Value = { 5L, 35L, 240L, 1645L, 11275L, 77280L, 529685L, 3630515L, 24883920L, 170556925L, 1169014555L, 8012544960L, 54918800165L, 376419056195L, 2580014593200L, 17683683096205L, 121205767080235L, 830756686465440L, 5694091038177845L, 39027880580779475L, 267501073027278480L };
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

    public class A290903Enumerable : IEnumerable<byte[]>
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

    public class A290903Inst : IEnumerable<long>
    {
        public static readonly long[] Value = A290903.Value;

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
        
        public static Stream StreamLazy => new EnumerableStream(A290903.Bytes);

        public long this[int i] => Value[i];
        
        public static A290903Inst Instance = new A290903Inst();
        
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