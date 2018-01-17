/*
 Copyright (c) 2003-2017 Niels Kokholm, Peter Sestoft, and Rasmus Lystrøm
 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:
 
 The above copyright notice and this permission notice shall be included in
 all copies or substantial portions of the Software.
 
 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 SOFTWARE.
*/

using System;
using HSNXT.C5;
using NUnit.Framework;
using SCG = System.Collections.Generic;


namespace HSNXT.C5UnitTests.support
{
    namespace bases
    {

        [TestFixture(MemoryType.Normal)]
        [TestFixture(MemoryType.Strict)]
        [TestFixture(MemoryType.Safe)]
        public class ArrayBaseTest : BaseMemoryType
        {

            public ArrayBaseTest(MemoryType memoryType)
                : base(memoryType)
            {

            }
            class ABT : ArrayBase<string>
            {
                public ABT(MemoryType memoryType) : base(8, C5.EqualityComparer<string>.Default, memoryType) { }

                public override string Choose() { if (size > 0) return array[0]; throw new NoSuchItemException(); }

                public string this[int i] { get { return array[i]; } set { array[i] = value; } }


                public int thesize { get { return size; } set { size = value; } }
            }


            [Test]
            public void Check()
            {
                var abt = new ABT(MemoryType);

                abt.thesize = 3;
                abt[2] = "aaa";
                // Assert.IsFalse(abt.Check());
                abt[0] = "##";
                abt[1] = "##";
                Assert.IsTrue(abt.Check());
            }
        }
    }

    namespace itemops
    {
        [TestFixture]
        public class Comparers
        {
            class dbl : IComparable<dbl>
            {
                double d;

                public dbl(double din) { d = din; }

                public int CompareTo(dbl that)
                {
                    return d < that.d ? -1 : d == that.d ? 0 : 1;
                }
                public bool Equals(dbl that) { return d == that.d; }
            }

            [Test]
            public void GenericC()
            {
                SCG.IComparer<dbl> h = SCG.Comparer<dbl>.Default;
                var s = new dbl(3.4);
                var t = new dbl(3.4);
                var u = new dbl(7.4);

                Assert.AreEqual(0, h.Compare(s, t));
                Assert.IsTrue(h.Compare(s, u) < 0);
            }


            [Test]
            public void OrdinaryC()
            {
                SCG.IComparer<string> h = SCG.Comparer<string>.Default;
                var s = "bamse";
                var t = "bamse";
                var u = "bimse";

                Assert.AreEqual(0, h.Compare(s, t));
                Assert.IsTrue(h.Compare(s, u) < 0);
            }


            [Test]
            public void GenericCViaBuilder()
            {
                SCG.IComparer<dbl> h = SCG.Comparer<dbl>.Default;
                var s = new dbl(3.4);
                var t = new dbl(3.4);
                var u = new dbl(7.4);

                Assert.AreEqual(0, h.Compare(s, t));
                Assert.IsTrue(h.Compare(s, u) < 0);
                Assert.AreSame(h, SCG.Comparer<dbl>.Default);
            }


            [Test]
            public void OrdinaryCViaBuilder()
            {
                SCG.IComparer<string> h = SCG.Comparer<string>.Default;
                var s = "bamse";
                var t = "bamse";
                var u = "bimse";

                Assert.AreEqual(0, h.Compare(s, t));
                Assert.IsTrue(h.Compare(s, u) < 0);
                Assert.AreSame(h, SCG.Comparer<string>.Default);

            }

            public void ComparerViaBuilderTest<T>(T item1, T item2)
                where T : IComparable<T>
            {
                SCG.IComparer<T> h = SCG.Comparer<T>.Default;
                Assert.AreSame(h, SCG.Comparer<T>.Default);
                Assert.AreEqual(0, h.Compare(item1, item1));
                Assert.AreEqual(0, h.Compare(item2, item2));
                Assert.IsTrue(h.Compare(item1, item2) < 0);
                Assert.IsTrue(h.Compare(item2, item1) > 0);
                Assert.AreEqual(Math.Sign(item1.CompareTo(item2)), Math.Sign(h.Compare(item1, item2)));
                Assert.AreEqual(Math.Sign(item2.CompareTo(item1)), Math.Sign(h.Compare(item2, item1)));
            }

            [Test]
            public void PrimitiveComparersViaBuilder()
            {
                ComparerViaBuilderTest<char>('A', 'a');
                ComparerViaBuilderTest<sbyte>(-122, 126);
                ComparerViaBuilderTest<byte>(122, 126);
                ComparerViaBuilderTest<short>(-30000, 3);
                ComparerViaBuilderTest<ushort>(3, 50000);
                ComparerViaBuilderTest<int>(-10000000, 10000);
                ComparerViaBuilderTest<uint>(10000000, 3000000000);
                ComparerViaBuilderTest<long>(-1000000000000, 10000000);
                ComparerViaBuilderTest<ulong>(10000000000000UL, 10000000000004UL);
                ComparerViaBuilderTest<float>(-0.001F, 0.00001F);
                ComparerViaBuilderTest<double>(-0.001, 0.00001E-200);
                ComparerViaBuilderTest<decimal>(-20.001M, 19.999M);
            }

            // This test is obsoleted by the one above, but we keep it for good measure
            [Test]
            public void IntComparerViaBuilder()
            {
                SCG.IComparer<int> h = SCG.Comparer<int>.Default;
                var s = 4;
                var t = 4;
                var u = 5;

                Assert.AreEqual(0, h.Compare(s, t));
                Assert.IsTrue(h.Compare(s, u) < 0);
                Assert.AreSame(h, SCG.Comparer<int>.Default);
            }

            [Test]
            public void Nulls()
            {
                Assert.IsTrue(SCG.Comparer<string>.Default.Compare(null, "abe") < 0);
                Assert.IsTrue(SCG.Comparer<string>.Default.Compare(null, null) == 0);
                Assert.IsTrue(SCG.Comparer<string>.Default.Compare("abe", null) > 0);
            }
        }

        [TestFixture]
        public class EqualityComparers
        {
            [Test]
            public void ReftypeequalityComparer()
            {
                var h = C5.EqualityComparer<string>.Default;
                var s = "bamse";
                var t = "bamse";
                var u = "bimse";

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
            }


            [Test]
            public void ValuetypeequalityComparer()
            {
                var h = C5.EqualityComparer<double>.Default;
                var s = 3.4;
                var t = 3.4;
                var u = 5.7;

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
            }

            [Test]
            public void ReftypeequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<string>.Default;
                var s = "bamse";
                var t = "bamse";
                var u = "bimse";

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<string>.Default);
            }


            [Test]
            public void ValuetypeequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<double>.Default;
                var s = 3.4;
                var t = 3.4;
                var u = 5.7;

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<double>.Default);
            }

            [Test]
            public void CharequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<char>.Default;
                var s = 'å';
                var t = 'å';
                var u = 'r';

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<char>.Default);
            }

            [Test]
            public void SbyteequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<sbyte>.Default;
                sbyte s = 3;
                sbyte t = 3;
                sbyte u = -5;

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<sbyte>.Default);
            }

            [Test]
            public void ByteequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<byte>.Default;
                byte s = 3;
                byte t = 3;
                byte u = 5;

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<byte>.Default);
            }

            [Test]
            public void ShortequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<short>.Default;
                short s = 3;
                short t = 3;
                short u = -5;

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<short>.Default);
            }

            [Test]
            public void UshortequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<ushort>.Default;
                ushort s = 3;
                ushort t = 3;
                ushort u = 60000;

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<ushort>.Default);
            }

            [Test]
            public void IntequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<int>.Default;
                var s = 3;
                var t = 3;
                var u = -5;

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<int>.Default);
            }

            [Test]
            public void UintequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<uint>.Default;
                uint s = 3;
                uint t = 3;
                var u = 3000000000;

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<uint>.Default);
            }

            [Test]
            public void LongequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<long>.Default;
                long s = 3;
                long t = 3;
                var u = -500000000000000L;

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<long>.Default);
            }

            [Test]
            public void UlongequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<ulong>.Default;
                ulong s = 3;
                ulong t = 3;
                var u = 500000000000000UL;

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<ulong>.Default);
            }

            [Test]
            public void FloatequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<float>.Default;
                var s = 3.1F;
                var t = 3.1F;
                var u = -5.2F;

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<float>.Default);
            }

            [Test]
            public void DoubleequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<double>.Default;
                var s = 3.12345;
                var t = 3.12345;
                var u = -5.2;

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<double>.Default);
            }

            [Test]
            public void DecimalequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<decimal>.Default;
                var s = 3.0001M;
                var t = 3.0001M;
                var u = -500000000000000M;

                Assert.AreEqual(s.GetHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<decimal>.Default);
            }

            [Test]
            public void UnseqequalityComparerViaBuilder()
            {
                var h = C5.EqualityComparer<C5.ICollection<int>>.Default;
                C5.ICollection<int> s = new LinkedList<int>();
                C5.ICollection<int> t = new LinkedList<int>();
                C5.ICollection<int> u = new LinkedList<int>();
                s.Add(1); s.Add(2); s.Add(3);
                t.Add(3); t.Add(2); t.Add(1);
                u.Add(3); u.Add(2); u.Add(4);
                Assert.AreEqual(s.GetUnsequencedHashCode(), h.GetHashCode(s));
                Assert.IsTrue(h.Equals(s, t));
                Assert.IsFalse(h.Equals(s, u));
                Assert.AreSame(h, C5.EqualityComparer<C5.ICollection<int>>.Default);
            }

            [Test]
            public void SeqequalityComparerViaBuilder2()
            {
                var h = C5.EqualityComparer<LinkedList<int>>.Default;
                var s = new LinkedList<int>();
                s.Add(1); s.Add(2); s.Add(3);
                Assert.AreEqual(CHC.sequencedhashcode(1, 2, 3), h.GetHashCode(s));
            }

            [Test]
            public void UnseqequalityComparerViaBuilder2()
            {
                var h = C5.EqualityComparer<C5.HashSet<int>>.Default;
                var s = new C5.HashSet<int>();
                s.Add(1); s.Add(2); s.Add(3);
                Assert.AreEqual(CHC.unsequencedhashcode(1, 2, 3), h.GetHashCode(s));
            }

            //generic types implementing collection interfaces
            [Test]
            public void SeqequalityComparerViaBuilder3()
            {
                var h = C5.EqualityComparer<C5.IList<int>>.Default;
                C5.IList<int> s = new LinkedList<int>();
                s.Add(1); s.Add(2); s.Add(3);
                Assert.AreEqual(CHC.sequencedhashcode(1, 2, 3), h.GetHashCode(s));
            }

            interface IFoo<T> : C5.ICollection<T> { void Bamse();      }

            class Foo<T> : C5.HashSet<T>, IFoo<T>
            {
                internal Foo() : base() { }
                public void Bamse() { }
            }

            [Test]
            public void UnseqequalityComparerViaBuilder3()
            {
                var h = C5.EqualityComparer<IFoo<int>>.Default;
                IFoo<int> s = new Foo<int>();
                s.Add(1); s.Add(2); s.Add(3);
                Assert.AreEqual(CHC.unsequencedhashcode(1, 2, 3), h.GetHashCode(s));
            }

            //Nongeneric types implementing collection types:
            interface IBaz : ISequenced<int> { void Bamse(); }

            class Baz : LinkedList<int>, IBaz
            {
                internal Baz() : base() { }
                public void Bamse() { }
                //int ISequenced<int>.GetHashCode() { return sequencedhashcode(); }
                //bool ISequenced<int>.Equals(ISequenced<int> that) { return sequencedequals(that); }
            }

            [Test]
            public void SeqequalityComparerViaBuilder4()
            {
                var h = C5.EqualityComparer<IBaz>.Default;
                IBaz s = new Baz();
                s.Add(1); s.Add(2); s.Add(3);
                Assert.AreEqual(CHC.sequencedhashcode(1, 2, 3), h.GetHashCode(s));
            }

            interface IBar : C5.ICollection<int>
            {
                void Bamse();
            }

            class Bar : C5.HashSet<int>, IBar
            {
                internal Bar() : base() { }
                public void Bamse() { }

                //TODO: remove all this workaround stuff:

                bool C5.ICollection<int>.ContainsAll(System.Collections.Generic.IEnumerable<int> items)
                {
                    throw new NotImplementedException();
                }

                void C5.ICollection<int>.RemoveAll(System.Collections.Generic.IEnumerable<int> items)
                {
                    throw new NotImplementedException();
                }

                void C5.ICollection<int>.RetainAll(System.Collections.Generic.IEnumerable<int> items)
                {
                    throw new NotImplementedException();
                }

                void IExtensible<int>.AddAll(SCG.IEnumerable<int> enumerable)
                {
                    throw new NotImplementedException();
                }

            }

            [Test]
            public void UnseqequalityComparerViaBuilder4()
            {
                var h = C5.EqualityComparer<IBar>.Default;
                IBar s = new Bar();
                s.Add(1); s.Add(2); s.Add(3);
                Assert.AreEqual(CHC.unsequencedhashcode(1, 2, 3), h.GetHashCode(s));
            }

            [Test]
            public void StaticEqualityComparerWithNull()
            {
                var arr = new ArrayList<double>();
                var eqc = C5.EqualityComparer<double>.Default;
                Assert.IsTrue(CollectionBase<double>.StaticEquals(arr, arr, eqc));
                Assert.IsTrue(CollectionBase<double>.StaticEquals(null, null, eqc));
                Assert.IsFalse(CollectionBase<double>.StaticEquals(arr, null, eqc));
                Assert.IsFalse(CollectionBase<double>.StaticEquals(null, arr, eqc));
            }

            private class EveryThingIsEqual : SCG.IEqualityComparer<Object>
            {
                public new bool Equals(Object o1, Object o2)
                {
                    return true;
                }

                public int GetHashCode(Object o)
                {
                    return 1;
                }
            }

            [Test]
            public void UnsequencedCollectionComparerEquality()
            {
                // Repro for bug20101103
                SCG.IEqualityComparer<Object> eqc = new EveryThingIsEqual();
                Object o1 = new Object(), o2 = new Object();
                C5.ICollection<Object> coll1 = new ArrayList<Object>(eqc),
                  coll2 = new ArrayList<Object>(eqc);
                coll1.Add(o1);
                coll2.Add(o2);
                Assert.IsFalse(o1.Equals(o2));
                Assert.IsTrue(coll1.UnsequencedEquals(coll2));
            }
        }
    }
}
