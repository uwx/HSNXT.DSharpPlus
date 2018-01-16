using System; using HSNXT;
using System.Collections.Generic;
using System.Linq;
using BCLExtensions.Tests.TestHelpers;
using Xunit;

namespace BCLExtensions.Tests.IEnumerableExtensions
{
    public class FullOuterJoinTests
    {
        public class NullArgumentTests {

        readonly IEnumerable<int> _nullEnumerable = null;
        readonly Func<int, int> _nullKeySelector = null;
        readonly Func<int, int, bool> _nullResultSelector = null;

        readonly int[] _validEnumerable = Enumerable.Empty<int>().ToArray();
        readonly Func<int, int> _validKeySelector = FuncHelpers.SelectSelf;
        readonly Func<int, int, bool> _validResultSelector = NumbersMatch;

        readonly Func<IEnumerable<int>, IEnumerable<int>, Func<int, int>, Func<int, int>, Func<int, int, bool>, IEnumerable<bool>> _fullOuterJoin = FullOuterJoinAndEnumerateResults;

        [Fact]
        public void NullFirstEnumerableThrowsException()
        {
            Assert.Throws<ArgumentNullException>(_fullOuterJoin.AsActionUsing(_nullEnumerable, _validEnumerable, _validKeySelector, _validKeySelector, _validResultSelector));
        }

        [Fact]
        public void NullSecondEnumerableThrowsException()
        {
            Assert.Throws<ArgumentNullException>(_fullOuterJoin.AsActionUsing(_validEnumerable, _nullEnumerable, _validKeySelector, _validKeySelector, _validResultSelector));
        }

        [Fact]
        public void NullFirstKeySelectorThrowsException()
        {
            Assert.Throws<ArgumentNullException>(_fullOuterJoin.AsActionUsing(_validEnumerable, _validEnumerable, _nullKeySelector, _validKeySelector, _validResultSelector));
        }

        [Fact]
        public void NullSecondKeySelectorThrowsException()
        {
            Assert.Throws<ArgumentNullException>(_fullOuterJoin.AsActionUsing(_validEnumerable, _validEnumerable, _validKeySelector, _nullKeySelector, _validResultSelector));
        }

        [Fact]
        public void NullResultsSelectorThrowsException()
        {
            Assert.Throws<ArgumentNullException>(_fullOuterJoin.AsActionUsing(_validEnumerable, _validEnumerable, _validKeySelector, _validKeySelector, _nullResultSelector));
        }

        [Fact]
        public void ValidInputReturnsSuccessfully()
        {
            _fullOuterJoin(_validEnumerable, _validEnumerable, _validKeySelector, _validKeySelector, _validResultSelector);
        }

        private static IEnumerable<bool> FullOuterJoinAndEnumerateResults(IEnumerable<int> firstEnumerable, IEnumerable<int> secondEnumerable, Func<int, int> firstKeySelector, Func<int, int> secondKeySelector, Func<int, int, bool> resultSelector)
        {
            return firstEnumerable.FullOuterJoin(secondEnumerable, firstKeySelector, secondKeySelector, resultSelector).ToArray();
        }
}
        private static void AssertAreEqualIgnoringOrdering<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.Equal(
                expected.OrderBy(FuncHelpers.SelectSelf),
                actual.OrderBy(FuncHelpers.SelectSelf)
            );
        }

        [Fact]
        public void MatchingOnBothSides()
        {
            var left = new[] { 1, 2, 3 };
            var right = new[] { 1, 2, 3 };

            var join = left.FullOuterJoin(right, FuncHelpers.SelectSelf, FuncHelpers.SelectSelf, (l, r) => l == r);


            AssertAreEqualIgnoringOrdering(new[] { true, true, true }, join);
        }

        [Fact]
        public void UnmatchingOnTheLeftSide()
        {
            var left = new[] { 1, 2, 3, 4 };
            var right = new[] { 1, 2, 3 };

            var join = left.FullOuterJoin(right, FuncHelpers.SelectSelf, FuncHelpers.SelectSelf, (l, r) => l == r);


            AssertAreEqualIgnoringOrdering(new[] { true, true, true, false }, join);
        }

        [Fact]
        public void UnmatchingOnTheRightSide()
        {
            var left = new[] { 1, 2, 3 };
            var right = new[] { 1, 2, 3, 4 };

            var join = left.FullOuterJoin(right, FuncHelpers.SelectSelf, FuncHelpers.SelectSelf, (l, r) => l == r);

            AssertAreEqualIgnoringOrdering(new[] { true, true, true, false }, join);
        }

        [Fact]
        public void UnmatchingOnBothSides()
        {
            var left = new[] { 1, 2, 3, 99 };
            var right = new[] { 1, 2, 3, 44 };

            var join = left.FullOuterJoin(right, l => l, r => r, (l, r) => l == r);

            AssertAreEqualIgnoringOrdering(new[] { true, true, true, false, false }, join);
        }

        [Fact]
        public void UnsortedInputs()
        {
            var left = new[] { 4, 2, 3, 1 };
            var right = new[] { 3, 1, 4, 2 };

            var join = left.FullOuterJoin(right, l => l, r => r, NumbersMatch);

            AssertAreEqualIgnoringOrdering(new[] { true, true, true, true }, join);
        }

        [Fact]
        public void SelectsProductOfTwoInputsWithRepeatingKeys()
        {
            var left = new[] { 1, 1, 2, 2 };
            var right = new[] { 1, 2 };

            var join = left.FullOuterJoin(right, l => l, r => r, (l, r) => l == r);

            AssertAreEqualIgnoringOrdering(new[] { true, true, true, true }, join);
        }

        public class IntStringTupleEqualityComparer : IEqualityComparer<Tuple<int, string>>
        {
            public bool Equals(Tuple<int, string> x, Tuple<int, string> y)
            {
                return x.Item2 == y.Item2;
            }

            public int GetHashCode(Tuple<int, string> obj)
            {
                var hash = 17;
                hash = hash * 31 + obj.Item1.GetHashCode();
                hash = hash * 31 + obj.Item2.GetHashCode();
                return hash;
            }
        }

        [Fact]
        public void SupportsCustomEqualityComparers()
        {
            var left = new[] {
                //These have matching items on the right
                Tuple.Create(1, "First"),
                Tuple.Create(1, "Second"),

                //These are distinct to the left
                Tuple.Create(1, "Third"),
                Tuple.Create(2, "Fourth"),
            };

            var right = new[] {
                //These have matching items on the left
                Tuple.Create(1, "First"),
                Tuple.Create(1, "Second"),

                //These are distinct to the right
                Tuple.Create(2, "Third"),
                Tuple.Create(3, "Fourth"),
            };

            var comparer = new IntStringTupleEqualityComparer();

            var join = left.FullOuterJoin(right,
                                          FuncHelpers.SelectSelf,
                                          FuncHelpers.SelectSelf,
                                          (l, r) => String.Format("{0}|{1}", l == null ? "NULL" : l.Item2, r == null ? "NULL" : r.Item2),
                                          keyEqualityComparer: comparer);

            AssertAreEqualIgnoringOrdering(
                new[] {
                    "First|First",
                    "Second|Second",
                    "Third|NULL",
                    "Fourth|NULL",
                    "NULL|Third",
                    "NULL|Fourth",
                },
                join
            );
        }
        private static bool NumbersMatch(int l, int r)
        {
            return l == r;
        }

    }
}