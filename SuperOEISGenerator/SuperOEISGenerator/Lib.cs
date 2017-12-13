using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using SuperOEISGenerator.IO;
namespace OEISReader.DatabaseX
{

 public static class A291692
 {
 public static readonly long[] Value={ 1L,1L,0L,0L,0L,0L,0L,0L,8L,8L,0L,0L,0L,0L,0L,0L,28L,28L,0L,0L,0L,0L,0L,0L,56L,56L,0L,27L,27L,0L,0L,0L,70L,70L,0L,216L,216L,0L,0L,0L,56L,56L,0L,756L,756L,0L,0L,0L,28L,28L,0L,1512L,1512L,0L,351L,351L,8L,8L,0L,1890L,1890L,0L,2808L,2808L,65L,65L,0L,1512L,1512L,0L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291692Enumerable : IEnumerable<byte[]>
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
 public class A291692Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291692.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291692.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291692Inst Instance=new A291692Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291690
 {
 public static readonly long[] Value={ 5L,2L,3L,17L,2L,6L,3L,10L,10L,3L,13L,13L,12L,5L,5L,2L,2L,2L,7L,11L,28L,6L,6L,7L,7L,11L,5L,6L,6L,3L,6L,6L,3L,2L,12L,6L,18L,20L,5L,2L,2L,21L,19L,5L,3L,3L,3L,5L,6L,6L,21L,7L,14L,6L,5L,7L,15L,6L,11L,3L,3L,5L,22L,17L,14L,3L,29L,15L,2L,13L,13L,19L,6L,2L,10L,10L,18L,6L,21L,26L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291690Enumerable : IEnumerable<byte[]>
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
 public class A291690Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291690.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291690.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291690Inst Instance=new A291690Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291689
 {
 public static readonly long[] Value={ 23L,37L,43L,52L,73L,74L,82L,88L,92L,98L,107L,108L,109L,113L,122L,123L,124L,128L,129L,133L,136L,137L,152L,157L,166L,178L,179L,183L,198L,201L,202L,205L,208L,211L,212L,213L,214L,217L,222L,223L,224L,227L,228L,229L,235L,238L,239L,243L,250L,251L,252L,253L,254L,255L,256L,257L,261L,262L,270L,271L,274L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291689Enumerable : IEnumerable<byte[]>
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
 public class A291689Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291689.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291689.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291689Inst Instance=new A291689Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291688
 {
 public static readonly BigInteger[] Value={ 1L,1L,5L,36L,327L,3392L,38795L,469662L,5935728L,77416352L,1035050705L,14094000938L,195075365778L,2734475097609L,38747262233793L,554199475506095L,7990492729051526L,115995691148658656L,1694340616136589743L,BigInteger.Parse("24882428969673439384"),BigInteger.Parse("367160435328847044586") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291688Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291688.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291688Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291688.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291688.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291688Inst Instance=new A291688Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291687
 {
 public static readonly BigInteger[] Value={ 5L,7L,3469L,9949L,65839L,1514209L,5221129L,40883539L,151412629L,44358635479L,16713607661375629L,BigInteger.Parse("36453104912477522894629"),BigInteger.Parse("1027438963906784290227656915629"),BigInteger.Parse("7419136758370889359733910587728129"),BigInteger.Parse("4551830726072842264843919206776501006328129") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291687Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291687.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291687Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291687.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291687.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291687Inst Instance=new A291687Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291685
 {
 public static readonly long[] Value={ 1L,1L,2L,5L,16L,52L,189L,683L,2621L,10061L,40031L,159201L,650880L,2657089L,11062682L,46065143L,194595138L,822215099L,3513875245L,15021070567L,64785349064L,279575206629L,1214958544538L,5283266426743L,23106210465665L,101120747493793L,444614706427665L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291685Enumerable : IEnumerable<byte[]>
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
 public class A291685Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291685.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291685.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291685Inst Instance=new A291685Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291684
 {
 public static readonly long[] Value={ 1L,0L,1L,0L,1L,1L,0L,1L,2L,2L,0L,1L,5L,5L,5L,0L,1L,9L,12L,14L,16L,0L,1L,17L,36L,36L,47L,52L,0L,1L,31L,81L,98L,117L,166L,189L,0L,1L,57L,174L,327L,327L,425L,627L,683L,0L,1L,101L,413L,788L,988L,1116L,1633L,2400L,2621L,0L,1L,185L,889L,1890L,3392L,3392L,4291L,6471L,9459L,10061L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291684Enumerable : IEnumerable<byte[]>
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
 public class A291684Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291684.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291684.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291684Inst Instance=new A291684Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291683
 {
 public static readonly long[] Value={ 0L,0L,1L,3L,9L,25L,71L,205L,607L,1833L,5635L,17577L,55515L,177191L,570699L,1852571L,6055079L,19910729L,65823751L,218654099L,729459551L,2443051213L,8210993363L,27685671843L,93625082139L,317470233149L,1079183930827L,3676951654519L,12554734605495L,42952566314235L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291683Enumerable : IEnumerable<byte[]>
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
 public class A291683Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291683.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291683.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291683Inst Instance=new A291683Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291680
 {
 public static readonly long[] Value={ 1L,0L,1L,0L,1L,1L,0L,1L,3L,2L,0L,1L,9L,8L,4L,0L,1L,25L,36L,20L,10L,0L,1L,71L,156L,108L,58L,26L,0L,1L,205L,666L,586L,340L,170L,74L,0L,1L,607L,2860L,3098L,2014L,1078L,528L,218L,0L,1L,1833L,12336L,16230L,11888L,6772L,3550L,1672L,672L,0L,1L,5635L,53518L,85150L,69274L,42366L,23284L,11840L,5454L,2126L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291680Enumerable : IEnumerable<byte[]>
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
 public class A291680Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291680.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291680.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291680Inst Instance=new A291680Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291679
 {
 public static readonly long[] Value={ 1L,1L,1L,-2L,-11L,-24L,-8L,141L,573L,1087L,-174L,-8700L,-31328L,-52740L,36387L,534198L,1742445L,2540583L,-3626189L,-33115232L,-97968686L,-118497822L,301668764L,2060526393L,5526622320L,5165256226L,-23033840842L,-127995025736L,-310560935969L,-193716799472L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291679Enumerable : IEnumerable<byte[]>
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
 public class A291679Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291679.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291679.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291679Inst Instance=new A291679Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291678
 {
 public static readonly long[] Value={ 1L,1L,0L,1L,1L,0L,1L,2L,0L,0L,1L,3L,1L,-1L,0L,1L,4L,3L,-2L,0L,0L,1L,5L,6L,-2L,-2L,1L,0L,1L,6L,10L,0L,-6L,2L,1L,0L,1L,7L,15L,5L,-11L,0L,5L,-1L,0L,1L,8L,21L,14L,-15L,-8L,12L,0L,-2L,0L,1L,9L,28L,28L,-15L,-24L,18L,9L,-8L,0L,0L,1L,10L,36L,48L,-7L,-48L,15L,32L,-15L,-6L,2L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291678Enumerable : IEnumerable<byte[]>
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
 public class A291678Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291678.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291678.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291678Inst Instance=new A291678Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291677
 {
 public static readonly BigInteger[] Value={ 1L,1L,7L,148L,6171L,425976L,43979902L,6346283560L,1219725741715L,301190499710320L,92921064554444490L,BigInteger.Parse("35025128774218944648"),BigInteger.Parse("15838288022236083603486"),BigInteger.Parse("8462453158197423495502224"),BigInteger.Parse("5274234568391796228927038748"),BigInteger.Parse("3792391176672742840187796835728") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291677Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291677.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291677Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291677.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291677.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291677Inst Instance=new A291677Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291676
 {
 public static readonly BigInteger[] Value={ 0L,1L,10L,3527L,123296356L,757031629267449L,BigInteger.Parse("1263498691933197473321646"),BigInteger.Parse("823340843273442113630752833831086703"),BigInteger.Parse("285062591046216676379736060856308430344065653125000") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291676Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291676.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291676Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291676.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291676.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291676Inst Instance=new A291676Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291675
 {
 public static readonly long[] Value={ 4L,14L,40L,96L,222L,488L,1052L,2222L,4640L,9592L,19694L,40208L,81748L,165646L,334776L,675184L,1359486L,2733720L,5491308L,11021230L,22104944L,44310984L,88785550L,177835776L,356099812L,712892558L,1426906312L,2855626752L,5714188830L,11433127112L,22873939004L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291675Enumerable : IEnumerable<byte[]>
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
 public class A291675Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291675.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291675.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291675Inst Instance=new A291675Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291674
 {
 public static readonly long[] Value={ 1L,1L,3L,2L,3L,3L,2L,2L,4L,3L,19L,3L,6L,2L,3L,3L,7L,4L,10L,3L,4L,19L,43L,3L,19L,6L,10L,2L,39L,3L,19L,4L,19L,7L,6L,4L,18L,10L,6L,3L,19L,4L,13L,19L,6L,43L,137L,3L,26L,19L,7L,6L,103L,10L,19L,2L,10L,39L,173L,3L,38L,19L,4L,4L,6L,19L,86L,7L,43L,6L,139L,4L,10L,18L,19L,10L,25L,6L,206L,3L,34L,19L,163L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291674Enumerable : IEnumerable<byte[]>
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
 public class A291674Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291674.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291674.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291674Inst Instance=new A291674Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291673
 {
 public static readonly long[] Value={ 0L,1L,2L,3L,5L,21L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291673Enumerable : IEnumerable<byte[]>
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
 public class A291673Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291673.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291673.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291673Inst Instance=new A291673Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291603
 {
 public static readonly long[] Value={ 1L,2L,3L,5L,7L,4L,8L,6L,9L,10L,11L,13L,15L,17L,19L,23L,25L,14L,16L,21L,27L,12L,18L,20L,22L,26L,28L,24L,29L,30L,31L,32L,33L,34L,36L,37L,39L,35L,41L,38L,40L,43L,44L,47L,49L,53L,55L,51L,57L,45L,59L,61L,63L,65L,67L,71L,73L,42L,46L,77L,79L,48L,50L,69L,75L,52L,56L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291603Enumerable : IEnumerable<byte[]>
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
 public class A291603Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291603.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291603.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291603Inst Instance=new A291603Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291602
 {
 public static readonly long[] Value={ 1105L,13981L,68101L,137149L,149281L,158369L,266305L,285541L,423793L,617093L,625921L,852841L,1052503L,1052929L,1104349L,1128121L,1306801L,1746289L,2940337L,3048841L,3828001L,4072729L,4154161L,4209661L,4682833L,6183601L,6236473L,6617929L,7803769L,9106141L,11157721L,11644921L,12096613L,12932989L,13554781L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291602Enumerable : IEnumerable<byte[]>
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
 public class A291602Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291602.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291602.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291602Inst Instance=new A291602Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291601
 {
 public static readonly long[] Value={ 341L,1105L,1387L,2047L,2701L,3277L,4033L,4369L,4681L,5461L,7957L,8321L,10261L,13747L,13981L,14491L,15709L,18721L,19951L,23377L,31417L,31609L,31621L,35333L,42799L,49141L,49981L,60701L,60787L,65077L,65281L,68101L,80581L,83333L,85489L,88357L,90751L,104653L,123251L,129889L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291601Enumerable : IEnumerable<byte[]>
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
 public class A291601Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291601.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291601.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291601Inst Instance=new A291601Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291600
 {
 public static readonly long[] Value={ 3141592653L,4392366484L,9526413073L,7454969632L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291600Enumerable : IEnumerable<byte[]>
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
 public class A291600Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291600.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291600.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291600Inst Instance=new A291600Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291599
 {
 public static readonly long[] Value={ 314159L,949129L,266830L,178653L,872117L,872117L,872117L,919441L,919441L,735287L,820737L,420516L,802307L,556505L,267638L,107072L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291599Enumerable : IEnumerable<byte[]>
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
 public class A291599Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291599.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291599.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291599Inst Instance=new A291599Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291597
 {
 public static readonly long[] Value={ 5865L,10005L,15045L,28815L,37995L,45645L,50235L,170085L,310845L,347565L,521985L,613785L,627555L,707115L,791265L,797385L,830415L,873885L,994755L,1014645L,1066665L,1078815L,1202835L,1323705L,1366545L,1542495L,1689465L,1730865L,1819605L,2001495L,2013735L,2246295L,2264655L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291597Enumerable : IEnumerable<byte[]>
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
 public class A291597Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291597.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291597.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291597Inst Instance=new A291597Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291596
 {
 public static readonly long[] Value={ 0L,0L,0L,0L,0L,4L,7L,19L,35L,69L,116L,204L,323L,523L,799L,1225L,1809L,2675L,3843L,5515L,7756L,10869L,14998L,20621L,27996L,37865L,50701L,67612L,89419L,117806L,154101L,200838L,260168L,335824L,431202L,551824L,702890L,892503L,1128577L,1422846L,1787183L,2238554L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291596Enumerable : IEnumerable<byte[]>
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
 public class A291596Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291596.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291596.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291596Inst Instance=new A291596Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291595
 {
 public static readonly long[] Value={ 1L,6L,35L,336L,8095L,389502L,41541383L,7705628640L,3104759041723L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291595Enumerable : IEnumerable<byte[]>
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
 public class A291595Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291595.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291595.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291595Inst Instance=new A291595Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291594
 {
 public static readonly long[] Value={ 1L,3L,9L,25L,69L,219L,832L,3894L,23202L,176838L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291594Enumerable : IEnumerable<byte[]>
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
 public class A291594Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291594.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291594.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291594Inst Instance=new A291594Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291593
 {
 public static readonly BigInteger[] Value={ 1L,6L,397L,64627L,33548446L,68719441230L,562949953224709L,18446744073708514623UL,BigInteger.Parse("2417851639229258344134994"),BigInteger.Parse("1267650600228229401496677070990"),BigInteger.Parse("2658455991569831745807614120434011325"),BigInteger.Parse("22300745198530623141535718272648360902487971") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291593Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291593.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291593Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291593.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291593.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291593Inst Instance=new A291593Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291592
 {
 public static readonly long[] Value={ 0L,0L,1L,1L,2L,2L,1L,2L,2L,2L,1L,4L,2L,2L,3L,3L,1L,4L,1L,4L,3L,2L,1L,6L,2L,2L,3L,4L,1L,6L,1L,4L,3L,2L,3L,7L,1L,2L,3L,6L,1L,6L,1L,4L,5L,2L,1L,8L,2L,4L,3L,4L,1L,6L,3L,6L,3L,2L,1L,10L,1L,2L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291592Enumerable : IEnumerable<byte[]>
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
 public class A291592Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291592.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291592.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291592Inst Instance=new A291592Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291591
 {
 public static readonly long[] Value={ 71831760L,1675212000L,6913932480L,4323749790360L,2678930100000L,175434192299520L,503151375767040L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291591Enumerable : IEnumerable<byte[]>
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
 public class A291591Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291591.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291591.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291591Inst Instance=new A291591Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291590
 {
 public static readonly long[] Value={ 0L,0L,0L,0L,2L,5L,11L,22L,40L,70L,116L,187L,292L,448L,670L,988L,1432L,2051L,2896L,4052L,5603L,7687L,10446L,14096L,18870L,25108L,33176L,43601L,56960L,74051L,95762L,123300L,158011L,201692L,256368L,324682L,409642L,515116L,645509L,806430L,1004292L,1247146L,1544237L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291590Enumerable : IEnumerable<byte[]>
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
 public class A291590Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291590.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291590.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291590Inst Instance=new A291590Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291589
 {
 public static readonly long[] Value={ 0L,0L,0L,2L,3L,8L,13L,26L,40L,69L,104L,165L,241L,363L,517L,750L,1046L,1473L,2018L,2779L,3746L,5063L,6733L,8959L,11769L,15454L,20082L,26068L,33549L,43108L,54997L,70037L,88645L,111979L,140714L,176462L,220280L,274418L,340480L,421593L,520154L,640481L,786104L,962976L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291589Enumerable : IEnumerable<byte[]>
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
 public class A291589Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291589.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291589.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291589Inst Instance=new A291589Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291588
 {
 public static readonly long[] Value={ 1L,2L,3L,5L,4L,7L,11L,6L,13L,17L,8L,19L,9L,10L,23L,29L,14L,27L,25L,16L,31L,37L,12L,35L,41L,22L,43L,39L,20L,47L,49L,32L,33L,53L,26L,59L,61L,15L,67L,71L,28L,73L,45L,34L,79L,77L,38L,65L,83L,46L,89L,21L,40L,97L,91L,44L,51L,95L,58L,101L,103L,18L,55L,107L,52L,109L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291588Enumerable : IEnumerable<byte[]>
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
 public class A291588Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291588.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291588.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291588Inst Instance=new A291588Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291587
 {
 public static readonly BigInteger[] Value={ 0L,1L,244L,762743L,12820180976L,757031629267449L,BigInteger.Parse("121921454556651769524"),BigInteger.Parse("45268703999809586294371407"),BigInteger.Parse("34375967164840303438628549400000"),BigInteger.Parse("48808991831991566280900452880679940625"),BigInteger.Parse("120855944455445379138034328603009420077012500") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291587Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291587.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291587Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291587.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291587.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291587Inst Instance=new A291587Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291475
 {
 public static readonly long[] Value={ 19L,19L,19L,16L,19L,19L,7L,8L,9L,10L,11L,12L,13L,7L,15L,8L,17L,9L,19L,10L,7L,11L,19L,8L,19L,13L,9L,7L,19L,10L,19L,8L,11L,17L,7L,9L,19L,19L,13L,8L,19L,7L,19L,11L,9L,19L,19L,8L,7L,10L,17L,13L,19L,9L,11L,7L,19L,19L,19L,10L,19L,19L,7L,8L,13L,11L,19L,16L,19L,7L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291475Enumerable : IEnumerable<byte[]>
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
 public class A291475Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291475.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291475.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291475Inst Instance=new A291475Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291474
 {
 public static readonly long[] Value={ 13L,13L,13L,12L,13L,13L,7L,8L,9L,10L,11L,12L,13L,7L,13L,8L,13L,9L,13L,10L,7L,11L,13L,8L,13L,13L,9L,7L,13L,10L,13L,8L,11L,13L,7L,9L,13L,13L,13L,8L,13L,7L,13L,11L,9L,13L,13L,8L,7L,10L,13L,12L,13L,9L,11L,7L,13L,13L,13L,10L,13L,13L,7L,8L,13L,11L,13L,12L,13L,7L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291474Enumerable : IEnumerable<byte[]>
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
 public class A291474Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291474.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291474.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291474Inst Instance=new A291474Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291473
 {
 public static readonly long[] Value={ 41L,34L,27L,34L,41L,6L,7L,8L,9L,10L,11L,6L,13L,7L,15L,8L,17L,6L,19L,10L,7L,11L,23L,6L,25L,13L,9L,7L,29L,6L,31L,8L,11L,17L,7L,6L,37L,19L,13L,8L,41L,6L,41L,11L,9L,23L,41L,6L,7L,10L,17L,13L,41L,6L,11L,7L,19L,29L,41L,6L,41L,31L,7L,8L,13L,6L,41L,17L,23L,7L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291473Enumerable : IEnumerable<byte[]>
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
 public class A291473Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291473.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291473.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291473Inst Instance=new A291473Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291472
 {
 public static readonly long[] Value={ 36L,30L,24L,30L,36L,6L,7L,8L,9L,10L,11L,6L,13L,7L,15L,8L,17L,6L,19L,10L,7L,11L,23L,6L,25L,13L,9L,7L,29L,6L,31L,8L,11L,17L,7L,6L,36L,19L,13L,8L,36L,6L,36L,11L,9L,23L,36L,6L,7L,10L,17L,13L,36L,6L,11L,7L,19L,29L,36L,6L,36L,30L,7L,8L,13L,6L,36L,17L,23L,7L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291472Enumerable : IEnumerable<byte[]>
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
 public class A291472Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291472.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291472.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291472Inst Instance=new A291472Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291471
 {
 public static readonly long[] Value={ 31L,26L,21L,26L,31L,6L,7L,8L,9L,10L,11L,6L,13L,7L,15L,8L,17L,6L,19L,10L,7L,11L,23L,6L,25L,13L,9L,7L,29L,6L,31L,8L,11L,17L,7L,6L,31L,19L,13L,8L,31L,6L,31L,11L,9L,23L,31L,6L,7L,10L,17L,13L,31L,6L,11L,7L,19L,26L,31L,6L,31L,26L,7L,8L,13L,6L,31L,17L,21L,7L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291471Enumerable : IEnumerable<byte[]>
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
 public class A291471Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291471.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291471.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291471Inst Instance=new A291471Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291470
 {
 public static readonly long[] Value={ 26L,22L,18L,22L,26L,6L,7L,8L,9L,10L,11L,6L,13L,7L,15L,8L,17L,6L,19L,10L,7L,11L,23L,6L,25L,13L,9L,7L,26L,6L,26L,8L,11L,17L,7L,6L,26L,19L,13L,8L,26L,6L,26L,11L,9L,22L,26L,6L,7L,10L,17L,13L,26L,6L,11L,7L,18L,22L,26L,6L,26L,22L,7L,8L,13L,6L,26L,17L,18L,7L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291470Enumerable : IEnumerable<byte[]>
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
 public class A291470Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291470.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291470.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291470Inst Instance=new A291470Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291469
 {
 public static readonly long[] Value={ 21L,18L,15L,18L,21L,6L,7L,8L,9L,10L,11L,6L,13L,7L,15L,8L,17L,6L,19L,10L,7L,11L,21L,6L,21L,13L,9L,7L,21L,6L,21L,8L,11L,17L,7L,6L,21L,18L,13L,8L,21L,6L,21L,11L,9L,18L,21L,6L,7L,10L,15L,13L,21L,6L,11L,7L,15L,18L,21L,6L,21L,18L,7L,8L,13L,6L,21L,17L,15L,7L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291469Enumerable : IEnumerable<byte[]>
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
 public class A291469Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291469.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291469.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291469Inst Instance=new A291469Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291468
 {
 public static readonly long[] Value={ 16L,14L,12L,14L,16L,6L,7L,8L,9L,10L,11L,6L,13L,7L,12L,8L,16L,6L,16L,10L,7L,11L,16L,6L,16L,13L,9L,7L,16L,6L,16L,8L,11L,14L,7L,6L,16L,14L,12L,8L,16L,6L,16L,11L,9L,14L,16L,6L,7L,10L,12L,13L,16L,6L,11L,7L,12L,14L,16L,6L,16L,14L,7L,8L,13L,6L,16L,14L,12L,7L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291468Enumerable : IEnumerable<byte[]>
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
 public class A291468Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291468.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291468.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291468Inst Instance=new A291468Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291467
 {
 public static readonly long[] Value={ 11L,10L,9L,10L,11L,6L,7L,8L,9L,10L,11L,6L,11L,7L,9L,8L,11L,6L,11L,10L,7L,10L,11L,6L,11L,10L,9L,7L,11L,6L,11L,8L,9L,10L,7L,6L,11L,10L,9L,8L,11L,6L,11L,10L,9L,10L,11L,6L,7L,10L,9L,10L,11L,6L,11L,7L,9L,10L,11L,6L,11L,10L,7L,8L,11L,6L,11L,10L,9L,7L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291467Enumerable : IEnumerable<byte[]>
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
 public class A291467Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291467.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291467.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291467Inst Instance=new A291467Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291465
 {
 public static readonly long[] Value={ 1L,2L,4L,9L,14L,25L,36L,45L,52L,61L,62L,89L,90L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291465Enumerable : IEnumerable<byte[]>
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
 public class A291465Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291465.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291465.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291465Inst Instance=new A291465Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291461
 {
 public static readonly long[] Value={ 1L,512L,91125L,26198073L,12519490248L,20301732352L,87824421125L,93824221184L,121213882349L,128711132649L,162324571375L,171323771464L,368910352448L,19902511000000L,87782935806307L,171471879319616L,220721185826504L,470511577514952L,2977097087043793L,9063181647017784L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291461Enumerable : IEnumerable<byte[]>
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
 public class A291461Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291461.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291461.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291461Inst Instance=new A291461Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291460
 {
 public static readonly long[] Value={ 16L,128L,384L,512L,1024L,1536L,1792L,2176L,2560L,2912L,3072L,5120L,7168L,8192L,9216L,11264L,13312L,15360L,15616L,16384L,17408L,19456L,21504L,23552L,25600L,27648L,28672L,29696L,31744L,33792L,35840L,37376L,37888L,39936L,41984L,43392L,57344L,66560L,90112L,98304L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291460Enumerable : IEnumerable<byte[]>
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
 public class A291460Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291460.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291460.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291460Inst Instance=new A291460Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291459
 {
 public static readonly long[] Value={ 294053760L,575134560L,739458720L,882161280L,1193512320L,1314593280L,1725403680L,2539555200L,2588105520L,2646483840L,2711348640L,3008396160L,3891888000L,4053329280L,4214770560L,4648644000L,4802878080L,5176211040L,5194949760L,5258373120L,6470263800L,6768891360L,7900532640L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291459Enumerable : IEnumerable<byte[]>
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
 public class A291459Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291459.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291459.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291459Inst Instance=new A291459Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291458
 {
 public static readonly long[] Value={ 27720L,60480L,65520L,90720L,98280L,105840L,115920L,120120L,120960L,128520L,131040L,143640L,151200L,163800L,180180L,191520L,205920L,207900L,211680L,218400L,229320L,235620L,241920L,249480L,264600L,272160L,289800L,292320L,312480L,332640L,360360L,372960L,393120L,414960L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291458Enumerable : IEnumerable<byte[]>
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
 public class A291458Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291458.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291458.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291458Inst Instance=new A291458Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291457
 {
 public static readonly long[] Value={ 180L,240L,360L,420L,480L,540L,600L,660L,780L,840L,1080L,1320L,1560L,1890L,1920L,2016L,2040L,2184L,2280L,2352L,2376L,2688L,2760L,2856L,3000L,3192L,3360L,3480L,3720L,3744L,4284L,4320L,4440L,4680L,4704L,4896L,4920L,5160L,5292L,5640L,5796L,6048L,6360L,6552L,7080L,7128L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291457Enumerable : IEnumerable<byte[]>
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
 public class A291457Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291457.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291457.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291457Inst Instance=new A291457Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291456
 {
 public static readonly BigInteger[] Value={ 0L,1L,65L,47449L,194397760L,3037656102976L,141727869124448256L,BigInteger.Parse("16674281388691716870144"),BigInteger.Parse("4371079210518164503303028736"),BigInteger.Parse("2322975003299339366419974718488576"),BigInteger.Parse("2322977286679362958150790503464960000000") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291456Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291456.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291456Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291456.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291456.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291456Inst Instance=new A291456Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291328
 {
 public static readonly long[] Value={ 1L,1L,2L,1L,3L,1L,4L,3L,5L,1L,6L,1L,7L,5L,8L,1L,9L,1L,10L,7L,11L,3L,12L,5L,13L,9L,14L,3L,15L,3L,16L,11L,17L,7L,18L,3L,19L,13L,20L,5L,21L,5L,22L,15L,23L,5L,24L,7L,25L,17L,26L,5L,27L,11L,28L,19L,29L,7L,30L,7L,31L,21L,32L,13L,33L,7L,34L,23L,35L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291328Enumerable : IEnumerable<byte[]>
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
 public class A291328Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291328.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291328.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291328Inst Instance=new A291328Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291327
 {
 public static readonly long[] Value={ 1L,1L,2L,1L,3L,1L,4L,3L,5L,1L,6L,1L,7L,5L,8L,1L,9L,3L,10L,7L,11L,3L,12L,5L,13L,9L,14L,3L,15L,3L,16L,11L,17L,7L,18L,5L,19L,13L,20L,5L,21L,5L,22L,15L,23L,5L,24L,7L,25L,17L,26L,7L,27L,11L,28L,19L,29L,7L,30L,7L,31L,21L,32L,13L,33L,9L,34L,23L,35L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291327Enumerable : IEnumerable<byte[]>
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
 public class A291327Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291327.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291327.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291327Inst Instance=new A291327Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291326
 {
 public static readonly long[] Value={ 1L,1L,2L,1L,3L,1L,4L,3L,5L,1L,6L,1L,7L,5L,8L,3L,9L,3L,10L,7L,11L,3L,12L,5L,13L,9L,14L,3L,15L,5L,16L,11L,17L,7L,18L,5L,19L,13L,20L,5L,21L,5L,22L,15L,23L,7L,24L,7L,25L,17L,26L,7L,27L,11L,28L,19L,29L,9L,30L,9L,31L,21L,32L,13L,33L,9L,34L,23L,35L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291326Enumerable : IEnumerable<byte[]>
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
 public class A291326Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291326.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291326.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291326Inst Instance=new A291326Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291325
 {
 public static readonly long[] Value={ 1L,1L,2L,1L,3L,1L,4L,3L,5L,1L,6L,1L,7L,5L,8L,3L,9L,3L,10L,7L,11L,3L,12L,5L,13L,9L,14L,5L,15L,5L,16L,11L,17L,7L,18L,5L,19L,13L,20L,7L,21L,7L,22L,15L,23L,7L,24L,7L,25L,17L,26L,9L,27L,11L,28L,19L,29L,9L,30L,9L,31L,21L,32L,13L,33L,11L,34L,23L,35L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291325Enumerable : IEnumerable<byte[]>
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
 public class A291325Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291325.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291325.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291325Inst Instance=new A291325Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291324
 {
 public static readonly long[] Value={ 1L,1L,2L,1L,3L,1L,4L,3L,5L,1L,6L,3L,7L,5L,8L,3L,9L,3L,10L,7L,11L,5L,12L,5L,13L,9L,14L,5L,15L,5L,16L,11L,17L,7L,18L,7L,19L,13L,20L,7L,21L,9L,22L,15L,23L,9L,24L,9L,25L,17L,26L,11L,27L,11L,28L,19L,29L,11L,30L,11L,31L,21L,32L,15L,33L,13L,34L,23L,35L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291324Enumerable : IEnumerable<byte[]>
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
 public class A291324Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291324.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291324.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291324Inst Instance=new A291324Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291323
 {
 public static readonly long[] Value={ 1L,1L,2L,1L,3L,1L,4L,3L,5L,3L,6L,3L,7L,5L,8L,3L,9L,5L,10L,7L,11L,5L,12L,5L,13L,9L,14L,7L,15L,7L,16L,11L,17L,9L,18L,9L,19L,13L,20L,9L,21L,11L,22L,15L,23L,11L,24L,11L,25L,17L,26L,13L,27L,15L,28L,19L,29L,15L,30L,15L,31L,21L,32L,15L,33L,17L,34L,23L,35L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291323Enumerable : IEnumerable<byte[]>
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
 public class A291323Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291323.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291323.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291323Inst Instance=new A291323Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291322
 {
 public static readonly long[] Value={ 3L,4L,6L,8L,9L,10L,18L,21L,23L,26L,46L,70L,83L,156L,553L,591L,741L,790L,1430L,2139L,5509L,11429L,11881L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291322Enumerable : IEnumerable<byte[]>
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
 public class A291322Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291322.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291322.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291322Inst Instance=new A291322Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291321
 {
 public static readonly BigInteger[] Value={ 1012658227848L,1139240506329L,BigInteger.Parse("10126582278481012658227848"),BigInteger.Parse("11392405063291139240506329"),BigInteger.Parse("101265822784810126582278481012658227848"),BigInteger.Parse("113924050632911392405063291139240506329"),BigInteger.Parse("1012658227848101265822784810126582278481012658227848") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291321Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291321.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291321Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291321.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291321.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291321Inst Instance=new A291321Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291320
 {
 public static readonly long[] Value={ 2L,600L,25584L,97464L,826560L,1249920L,50725248L,1372734720L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291320Enumerable : IEnumerable<byte[]>
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
 public class A291320Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291320.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291320.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291320Inst Instance=new A291320Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291319
 {
 public static readonly long[] Value={ 42L,70L,78L,110L,114L,130L,154L,170L,222L,230L,258L,310L,322L,370L,374L,402L,406L,410L,418L,442L,470L,474L,530L,582L,598L,610L,618L,638L,646L,654L,670L,682L,730L,742L,754L,762L,782L,826L,830L,874L,902L,970L,978L,986L,994L,1010L,1030L,1034L,1070L,1158L,1222L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291319Enumerable : IEnumerable<byte[]>
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
 public class A291319Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291319.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291319.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291319Inst Instance=new A291319Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291318
 {
 public static readonly long[] Value={ 4L,9L,15L,33L,35L,49L,51L,65L,77L,87L,91L,95L,119L,123L,143L,161L,177L,185L,209L,213L,215L,217L,221L,247L,259L,287L,303L,321L,329L,335L,341L,361L,371L,377L,395L,403L,407L,411L,427L,437L,447L,469L,473L,485L,511L,515L,527L,533L,537L,545L,551L,573L,581L,591L,611L,629L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291318Enumerable : IEnumerable<byte[]>
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
 public class A291318Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291318.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291318.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291318Inst Instance=new A291318Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291317
 {
 public static readonly long[] Value={ 1L,1L,1L,3L,4L,3L,7L,7L,6L,10L,7L,12L,3L,10L,11L,7L,11L,1L,12L,6L,21L,1L,7L,12L,25L,3L,25L,28L,16L,26L,25L,6L,32L,19L,15L,21L,28L,3L,12L,21L,24L,13L,21L,36L,17L,45L,41L,45L,8L,40L,11L,6L,25L,41L,23L,4L,43L,52L,51L,57L,28L,21L,11L,47L,26L,29L,57L,51L,48L,56L,12L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291317Enumerable : IEnumerable<byte[]>
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
 public class A291317Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291317.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291317.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291317Inst Instance=new A291317Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291316
 {
 public static readonly long[] Value={ 1L,1L,1L,2L,0L,1L,3L,-1L,1L,2L,0L,2L,1L,0L,-1L,4L,2L,-1L,2L,-3L,4L,3L,-1L,2L,0L,1L,1L,2L,-2L,2L,5L,2L,-3L,0L,1L,-1L,6L,0L,4L,-2L,-1L,3L,-1L,2L,0L,4L,-2L,2L,4L,-2L,1L,5L,-2L,-2L,-2L,3L,6L,1L,3L,-2L,4L,-3L,-1L,-2L,3L,6L,2L,0L,-4L,5L,1L,3L,-1L,0L,0L,4L,-1L,-2L,4L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291316Enumerable : IEnumerable<byte[]>
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
 public class A291316Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291316.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291316.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291316Inst Instance=new A291316Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291315
 {
 public static readonly BigInteger[] Value={ 1L,3L,27L,312L,4140L,58806L,876492L,13524300L,214168536L,3460901967L,56842100298L,946076020551L,15922147355532L,270496012834332L,4632597495220104L,79896692540736729L,1386424262414762046L,BigInteger.Parse("24188862129358547349"),BigInteger.Parse("424059773742487363743"),BigInteger.Parse("7466416997545500727257"),BigInteger.Parse("131972899585564980561060") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291315Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291315.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291315Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291315.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291315.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291315Inst Instance=new A291315Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291314
 {
 public static readonly BigInteger[] Value={ 1L,2L,8L,56L,400L,3072L,24544L,203520L,1728256L,14967296L,131689472L,1173936128L,10579907072L,96238768128L,882437177344L,8147574407168L,75685465759744L,706854135595008L,6633217371029504L,62514337980088320L,591441701724880896L,5615172282703937536L,BigInteger.Parse("53480608406362914816"),BigInteger.Parse("510849109679635693568"),BigInteger.Parse("4892689722718505271296") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291314Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291314.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291314Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291314.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291314.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291314Inst Instance=new A291314Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291313
 {
 public static readonly BigInteger[] Value={ 1L,4L,36L,480L,6896L,106432L,1718784L,28718592L,492201856L,8605925376L,152904727040L,2752754089984L,50106792767488L,920624074653696L,17051087289835520L,318014241007730688L,5967490401704681472L,BigInteger.Parse("112584565019407941632"),BigInteger.Parse("2134274190939740995584"),BigInteger.Parse("40633890811539769786368"),BigInteger.Parse("776619666947548902981632"),BigInteger.Parse("14895370245374436645535744"),BigInteger.Parse("286602399114033680102719488"),BigInteger.Parse("5530627126602146509305675776"),BigInteger.Parse("107011451193255026335799050240") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291313Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291313.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291313Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291313.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291313.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291313Inst Instance=new A291313Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291216
 {
 public static readonly long[] Value={ 0L,1L,4L,9L,16L,49L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291216Enumerable : IEnumerable<byte[]>
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
 public class A291216Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291216.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291216.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291216Inst Instance=new A291216Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291215
 {
 public static readonly BigInteger[] Value={ BigInteger.Parse("1014492753623188405797"),BigInteger.Parse("1159420289855072463768"),BigInteger.Parse("1304347826086956521739"),BigInteger.Parse("10144927536231884057971014492753623188405797"),BigInteger.Parse("11594202898550724637681159420289855072463768"),BigInteger.Parse("13043478260869565217391304347826086956521739"),BigInteger.Parse("101449275362318840579710144927536231884057971014492753623188405797") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291215Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291215.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291215Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291215.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291215.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291215Inst Instance=new A291215Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291214
 {
 public static readonly BigInteger[] Value={ 1L,112L,5868L,250128L,10020912L,399379728L,16255733440L,684615750832L,30031767680256L,1376568893633760L,66017645596167168L,3313241694194681184L,BigInteger.Parse("173934275433107845120"),BigInteger.Parse("9543378596912872361440"),BigInteger.Parse("546711252967087466397696"),BigInteger.Parse("32663132242303127521217184") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291214Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291214.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291214Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291214.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291214.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291214Inst Instance=new A291214Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291213
 {
 public static readonly long[] Value={ 1L,3L,36L,6L,20L,72L,1168L,11L,216L,35L,576L,143L,111L,2422L,1657L,19L,336L,378L,6253L,66L,51L,1167L,820L,241L,24096L,180L,18805L,215L,3833L,3488L,368905L,31L,3460L,575L,426L,716L,576L,12387L,57556L,110L,10513L,83L,8948L,2303L,1782L,1656L,175195L,387L,1647L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291213Enumerable : IEnumerable<byte[]>
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
 public class A291213Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291213.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291213.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291213Inst Instance=new A291213Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291212
 {
 public static readonly long[] Value={ 3L,7L,8L,18L,19L,50L,140L,141L,391L,1079L,21986L,59824L,59825L,162694L,162695L,442342L,1202520L,3268920L,24154825L,65659825L,485165015L,1318815534L,1318815535L,26489121866L,72004899050L,195729609117L,532048240264L,1446257063927L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291212Enumerable : IEnumerable<byte[]>
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
 public class A291212Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291212.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291212.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291212Inst Instance=new A291212Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291211
 {
 public static readonly long[] Value={ 2L,5L,6L,15L,16L,46L,135L,136L,385L,1072L,21976L,59813L,59814L,162682L,162683L,442329L,1202506L,3268905L,24154808L,65659807L,485164995L,1318815513L,1318815514L,26489121842L,72004899025L,195729609091L,532048240237L,1446257063899L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291211Enumerable : IEnumerable<byte[]>
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
 public class A291211Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291211.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291211.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291211Inst Instance=new A291211Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291210
 {
 public static readonly long[] Value={ 2L,4L,10L,27L,80L,230L,644L,1780L,4879L,13315L,36261L,98650L,268260L,729326L,1982655L,5389579L,14650584L,39824632L,108254817L,294267376L,799901968L,2174359323L,5910521810L,16066464445L,43673178798L,118716008808L,322703570021L,877199250941L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291210Enumerable : IEnumerable<byte[]>
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
 public class A291210Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291210.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291210.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291210Inst Instance=new A291210Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291209
 {
 public static readonly long[] Value={ 9504L,16560L,41400L,5048568L,10889856L,11941344L,16255080L,131473152L,5517818880L,107561120688L,612014161920L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291209Enumerable : IEnumerable<byte[]>
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
 public class A291209Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291209.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291209.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291209Inst Instance=new A291209Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291208
 {
 public static readonly long[] Value={ 0L,1L,1L,2L,1L,3L,1L,2L,2L,3L,1L,5L,1L,3L,3L,3L,1L,5L,1L,5L,3L,3L,1L,6L,2L,3L,2L,5L,1L,7L,1L,4L,3L,3L,3L,8L,1L,3L,3L,6L,1L,7L,1L,5L,5L,3L,1L,8L,2L,5L,3L,5L,1L,6L,3L,6L,3L,3L,1L,11L,1L,3L,5L,4L,3L,7L,1L,5L,3L,7L,1L,10L,1L,3L,5L,5L,3L,7L,1L,8L,3L,3L,1L,11L,3L,3L,3L,6L,1L,11L,3L,5L,3L,3L,3L,10L,1L,5L,5L,8L,1L,7L,1L,6L,7L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291208Enumerable : IEnumerable<byte[]>
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
 public class A291208Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291208.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291208.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291208Inst Instance=new A291208Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291207
 {
 public static readonly long[] Value={ 1L,1L,-1L,1L,-1L,0L,1L,-1L,-1L,1L,1L,-1L,-3L,5L,0L,1L,-1L,-7L,27L,17L,-2L,1L,-1L,-15L,167L,441L,-121L,0L,1L,-1L,-31L,1071L,10673L,-11529L,-721L,5L,1L,-1L,-63L,6815L,262305L,-1337713L,-442827L,6845L,0L,1L,-1L,-127L,42687L,6525377L,-161721441L,-297209047L,23444883L,58337L,-14L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291207Enumerable : IEnumerable<byte[]>
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
 public class A291207Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291207.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291207.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291207Inst Instance=new A291207Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291206
 {
 public static readonly long[] Value={ 2L,17L,257L,337L,881L,1297L,2657L,6577L,10657L,14897L,16561L,28817L,65537L,65617L,66161L,80177L,83777L,149057L,160001L,166561L,260017L,280097L,331777L,391921L,394721L,411361L,463537L,596977L,614657L,621217L,847601L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291206Enumerable : IEnumerable<byte[]>
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
 public class A291206Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291206.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291206.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291206Inst Instance=new A291206Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291205
 {
 public static readonly long[] Value={ 2L,13L,37L,50L,58L,61L,67L,73L,74L,89L,97L,111L,113L,122L,151L,157L,169L,173L,181L,183L,193L,229L,233L,241L,250L,257L,259L,274L,277L,283L,298L,307L,313L,314L,317L,337L,349L,353L,373L,386L,389L,394L,397L,401L,409L,421L,427L,433L,449L,453L,457L,466L,481L,487L,507L,509L,514L,541L,543L,547L,554L,557L,562L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291205Enumerable : IEnumerable<byte[]>
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
 public class A291205Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291205.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291205.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291205Inst Instance=new A291205Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291204
 {
 public static readonly long[] Value={ 1L,0L,1L,0L,0L,0L,1L,0L,1L,0L,0L,0L,0L,1L,0L,1L,3L,0L,1L,0L,0L,0L,0L,0L,1L,0L,1L,7L,6L,0L,4L,4L,0L,1L,0L,0L,0L,0L,0L,0L,1L,0L,1L,15L,25L,10L,0L,14L,30L,10L,0L,8L,5L,0L,1L,0L,0L,0L,0L,0L,0L,0L,1L,0L,1L,31L,90L,65L,15L,0L,51L,174L,120L,20L,0L,54L,63L,15L,0L,13L,6L,0L,1L,0L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291204Enumerable : IEnumerable<byte[]>
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
 public class A291204Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291204.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291204.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291204Inst Instance=new A291204Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291203
 {
 public static readonly long[] Value={ 1L,0L,1L,0L,0L,0L,1L,0L,2L,0L,0L,0L,0L,1L,0L,3L,6L,0L,6L,0L,0L,0L,0L,0L,1L,0L,4L,24L,12L,0L,36L,24L,0L,24L,0L,0L,0L,0L,0L,0L,1L,0L,5L,80L,90L,20L,0L,200L,300L,60L,0L,300L,120L,0L,120L,0L,0L,0L,0L,0L,0L,0L,1L,0L,6L,240L,540L,240L,30L,0L,1170L,3000L,1260L,120L,0L,3360L,2520L,360L,0L,2520L,720L,0L,720L,0L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291203Enumerable : IEnumerable<byte[]>
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
 public class A291203Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291203.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291203.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291203Inst Instance=new A291203Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291202
 {
 public static readonly long[] Value={ 2L,3L,8L,11L,17L,41L,57L,62L,77L,101L,329L,333L,359L,365L,968L,1169L,1190L,1772L,2237L,12075L,30848L,63200L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291202Enumerable : IEnumerable<byte[]>
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
 public class A291202Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291202.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291202.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291202Inst Instance=new A291202Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291201
 {
 public static readonly long[] Value={ 1L,4L,7L,9L,10L,13L,27L,35L,94L,150L,198L,258L,673L,1194L,1492L,2320L,2727L,3767L,6246L,6877L,14481L,34327L,57634L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291201Enumerable : IEnumerable<byte[]>
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
 public class A291201Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291201.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291201.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291201Inst Instance=new A291201Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291082
 {
 public static readonly long[] Value={ 1L,2L,2L,1L,9L,12L,9L,4L,1L,51L,76L,69L,44L,20L,6L,1L,323L,512L,518L,392L,230L,104L,35L,8L,1L,2188L,3610L,3915L,3288L,2235L,1242L,560L,200L,54L,10L,1L,15511L,26324L,29964L,27016L,20240L,12804L,6853L,3080L,1143L,340L,77L,12L,1L,113634L,196938L,232323L,220584L,177177L,122694L,73710L,38376L,17199L,6552L,2079L,532L,104L,14L,1L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291082Enumerable : IEnumerable<byte[]>
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
 public class A291082Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291082.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291082.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291082Inst Instance=new A291082Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291081
 {
 public static readonly long[] Value={ 1L,1L,7L,6L,3L,1L,51L,45L,30L,15L,5L,1L,393L,357L,266L,161L,77L,28L,7L,1L,3139L,2907L,2304L,1554L,882L,414L,156L,45L,9L,1L,25653L,24068L,19855L,14355L,9042L,4917L,2277L,880L,275L,66L,11L,1L,212941L,201643L,171106L,129844L,87802L,52624L,27742L,12727L,5005L,1651L,442L,91L,13L,1L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291081Enumerable : IEnumerable<byte[]>
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
 public class A291081Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291081.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291081.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291081Inst Instance=new A291081Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291080
 {
 public static readonly long[] Value={ 1L,3L,2L,1L,19L,16L,10L,4L,1L,141L,126L,90L,50L,21L,6L,1L,1107L,1016L,784L,504L,266L,112L,36L,8L,1L,8953L,8350L,6765L,4740L,2850L,1452L,615L,210L,55L,10L,1L,73789L,69576L,58278L,43252L,28314L,16236L,8074L,3432L,1221L,352L,78L,12L,1L,616227L,585690L,502593L,388752L,270270L,168168L,93093L,45474L,19383L,7098L,2184L,546L,105L,14L,1L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291080Enumerable : IEnumerable<byte[]>
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
 public class A291080Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291080.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291080.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291080Inst Instance=new A291080Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291079
 {
 public static readonly long[] Value={ 1L,2L,4L,14L,22L,43L,70L,139L,181L,281L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291079Enumerable : IEnumerable<byte[]>
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
 public class A291079Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291079.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291079.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291079Inst Instance=new A291079Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291078
 {
 public static readonly BigInteger[] Value={ 3L,18L,2192L,2690028L,33891544512L,4169295414184440L,BigInteger.Parse("4883659780216684278864"),BigInteger.Parse("53651309692070478619341132840"),BigInteger.Parse("5474401089420219382077156121856117424"),BigInteger.Parse("5153775207320113310364604118676335808983329056"),BigInteger.Parse("44553974378043749018508590814287728257805180848046038672") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291078Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291078.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291078Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291078.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291078.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291078Inst Instance=new A291078Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291077
 {
 public static readonly BigInteger[] Value={ 8L,116L,2192L,44220L,956576L,21522344L,498111952L,11767874940L,282429537936L,6863037256208L,168456380799344L,4169295414184440L,103911670590189280L,2605214026691600584L,BigInteger.Parse("65651393478908052800"),BigInteger.Parse("1661800897428959110140"),BigInteger.Parse("42229293393638385042560") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291077Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291077.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291077Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291077.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291077.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291077Inst Instance=new A291077Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291076
 {
 public static readonly long[] Value={ 3L,18L,116L,810L,5880L,44220L,341484L,2690010L,21522228L,174336264L,1426403748L,11767874940L,97764009000L,817028131140L,6863037256208L,57906879556410L,490505340309600L,4169295414140220L,35548729381861332L,303941636389253448L,2605214026691600584L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291076Enumerable : IEnumerable<byte[]>
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
 public class A291076Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291076.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291076.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291076Inst Instance=new A291076Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291075
 {
 public static readonly long[] Value={ 211L,2122L,22122L,222122L,1222122L,212211L,2112122L,21222122L,221222122L,2221222122L,12221222122L,2122212211L,22122112122L,221121222122L,1212221222122L,222122212211L,1222122112122L,212211212211L,2112122112122L,21221121222122L,211212221222122L,2122212221222122L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291075Enumerable : IEnumerable<byte[]>
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
 public class A291075Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291075.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291075.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291075Inst Instance=new A291075Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291074
 {
 public static readonly long[] Value={ -1L,22L,1L,1L,222L,222L,11L,11L,11L,11L,1222L,1222L,1222L,1222L,111L,211L,111L,211L,111L,211L,111L,211L,11222L,21222L,11222L,21222L,11222L,21222L,11222L,21222L,1111L,1211L,2111L,2211L,1111L,1211L,2111L,2211L,1111L,1211L,2111L,2211L,1111L,1211L,2111L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291074Enumerable : IEnumerable<byte[]>
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
 public class A291074Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291074.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291074.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291074Inst Instance=new A291074Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291073
 {
 public static readonly long[] Value={ -1L,21L,1L,1L,221L,221L,11L,11L,11L,11L,2221L,2221L,2221L,2221L,111L,211L,111L,211L,111L,211L,111L,211L,12221L,22221L,12221L,22221L,12221L,22221L,12221L,22221L,1111L,1211L,2111L,2211L,1111L,1211L,2111L,2211L,1111L,1211L,2111L,2211L,1111L,1211L,2111L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291073Enumerable : IEnumerable<byte[]>
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
 public class A291073Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291073.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291073.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291073Inst Instance=new A291073Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291072
 {
 public static readonly long[] Value={ -1L,22L,1L,1L,122L,122L,11L,11L,11L,11L,2122L,2122L,2122L,2122L,111L,211L,111L,211L,111L,211L,111L,211L,12122L,22122L,12122L,22122L,12122L,22122L,12122L,22122L,1111L,1211L,2111L,2211L,1111L,1211L,2111L,2211L,1111L,1211L,2111L,2211L,1111L,1211L,2111L,2211L,112122L,122122L,212122L,222122L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291072Enumerable : IEnumerable<byte[]>
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
 public class A291072Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291072.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291072.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291072Inst Instance=new A291072Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291071
 {
 public static readonly BigInteger[] Value={ 54L,3966L,261522L,16768860L,1073708010L,68718945018L,4398044397642L,281474943095280L,18014398374741048L,1152921502458345570L,BigInteger.Parse("73786976286244079562"),BigInteger.Parse("4722366482732172984420"),BigInteger.Parse("302231454903107470761930"),BigInteger.Parse("19342813113825270435966978"),BigInteger.Parse("1237940039285345088379356750") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291071Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291071.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291071Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291071.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291071.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291071Inst Instance=new A291071Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291070
 {
 public static readonly BigInteger[] Value={ 30L,990L,32730L,1047540L,33554370L,1073708010L,34359738210L,1099510578960L,35184372055560L,1125899873286210L,36028797018961890L,1152921503532053580L,BigInteger.Parse("36893488147419095010"),BigInteger.Parse("1180591620683051547810"),BigInteger.Parse("37778931862957128089670"),BigInteger.Parse("1208925819613529663013120") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291070Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A291070.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A291070Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A291070.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291070.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A291070Inst Instance=new A291070Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291069
 {
 public static readonly long[] Value={ 5L,4L,4L,14L,13L,12L,25L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291069Enumerable : IEnumerable<byte[]>
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
 public class A291069Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291069.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291069.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291069Inst Instance=new A291069Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291068
 {
 public static readonly long[] Value={ 6L,5L,4L,15L,14L,13L,26L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291068Enumerable : IEnumerable<byte[]>
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
 public class A291068Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291068.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291068.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291068Inst Instance=new A291068Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A291067
 {
 public static readonly long[] Value={ 6L,5L,177L,178L,175L,174L,177L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A291067Enumerable : IEnumerable<byte[]>
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
 public class A291067Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A291067.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A291067.Bytes);
 public long this[int i]=>Value[i];
 
 public static A291067Inst Instance=new A291067Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A290958
 {
 public static readonly BigInteger[] Value={ 1L,-2L,6L,-26L,100L,-460L,2258L,-11558L,60786L,-326826L,1785930L,-9893778L,55447800L,-313817720L,1791442406L,-10303155322L,59642852324L,-347233450156L,2031756438046L,-11941773701426L,70471288256196L,-417379686511812L,2480161711278070L,-14781955283569090L,88343937381017274L,-529319474378769346L,3178848917169132254L,BigInteger.Parse("-19131855254581689246") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A290958Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A290958.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A290958Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A290958.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A290958.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A290958Inst Instance=new A290958Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A290957
 {
 public static readonly BigInteger[] Value={ 1L,2L,2L,6L,40L,208L,798L,3122L,15038L,77830L,381798L,1819998L,8925172L,45280900L,231030138L,1171823534L,5962836408L,30668699312L,158951012362L,825830001086L,4298605879552L,22459588992656L,117842770179898L,620193719988230L,3271151667546526L,17291851589803030L,91629268113394082L,486633483668452306L,2589396122840425628L,13802082307489152876UL,BigInteger.Parse("73692343820697785462"),BigInteger.Parse("394098991084750746722") };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(e=>e.ToByteArray());
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A290957Enumerable : IEnumerable<byte[]>
 {
 public IEnumerator<byte[]> GetEnumerator()
 {
 foreach (var b in A290957.Bytes) {
 yield return b; 
 }
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return GetEnumerator();
 }
 }
 public class A290957Inst : IEnumerable<BigInteger>
 {
 public static readonly BigInteger[] Value=A290957.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 for (var i=0; i < Value.Length; i++)
 {
 var b=Value[i].ToByteArray();
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A290957.Bytes);
 public BigInteger this[int i]=>Value[i];
 
 public static A290957Inst Instance=new A290957Inst();
 
 public IEnumerator<BigInteger> GetEnumerator()
 {
 return (Value as IEnumerable<BigInteger>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A290956
 {
 public static readonly long[] Value={ 1L,3L,4L,18L,28L,40L,45L,49L,78L,165L,312L,469L,855L,899L,1137L,1314L,1410L,3832L,10518L,24163L,28792L,36947L,56909L,58103L,59797L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A290956Enumerable : IEnumerable<byte[]>
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
 public class A290956Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A290956.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A290956.Bytes);
 public long this[int i]=>Value[i];
 
 public static A290956Inst Instance=new A290956Inst();
 
 public IEnumerator<long> GetEnumerator()
 {
 return (Value as IEnumerable<long>).GetEnumerator();
 }
 IEnumerator IEnumerable.GetEnumerator()
 {
 return Value.GetEnumerator();
 }
 }

 public static class A290955
 {
 public static readonly long[] Value={ 0L,1L,3L,7L,12L,13L,21L,40L,67L,69L,132L,213L,259L,1056L,1639L,1653L,2913L,6183L,7086L,8466L,27475L,55390L };
 public static readonly IEnumerable<byte[]> Bytes=Value.Select(BitConverter.GetBytes);
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(Bytes);
 }
 public class A290955Enumerable : IEnumerable<byte[]>
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
 public class A290955Inst : IEnumerable<long>
 {
 public static readonly long[] Value=A290955.Value;
 public static Stream Stream
 {
 get
 {
 var ms=new MemoryStream();
 // ReSharper disable once ForCanBeConvertedToForeach
 for (var i=0; i < Value.Length; i++)
 {
 var b=BitConverter.GetBytes(Value[i]);
 ms.Write(b,0,b.Length);
 }
 return ms;
 }
 }
 
 public static Stream StreamLazy=>new EnumerableStream(A290955.Bytes);
 public long this[int i]=>Value[i];
 
 public static A290955Inst Instance=new A290955Inst();
 
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