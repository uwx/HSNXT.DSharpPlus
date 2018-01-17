using Xunit;
using XunitShould;

namespace HSNXT.Tests
{
    public class MathExtensionsTestFixture
    {
        [Fact]
        public void MultiplyBy100ShouldSetCorrectValue()
        {
            decimal x = 10;
            var y = x.MultiplyBy(100);
            y.ShouldEqual(1000);
        }

        [Fact]
        public void DivideBy100ShouldSetCorrectValue()
        {
            decimal x = 1000;
            var y = x.DivideBy(100);
            y.ShouldEqual(10);
        }

        [Fact]
        public void RoundUpShouldRoundDecimalValueUp()
        {
            var value = 123.3333333M;
            var result = value.RoundUp(2);
            result.ShouldEqual(123.34M);
        }

        [Fact]
        public void RoundUpShouldRoundDoubleValueUp()
        {
            var value = 123.3333333;
            var result = value.RoundUp(2);
            result.ShouldEqual(123.34);
        }

        [Fact]
        public void RoundUpShouldRoundEdgeCaseDecmialUp()
        {
            var value = 123.000001M;
            var result = value.RoundUp(2);
            result.ShouldEqual(123.01M);
        }

        [Fact]
        public void RoundUpShouldRoundEdgeCaseDoubleUp()
        {
            var value = 123.000001;
            var result = value.RoundUp(2);
            result.ShouldEqual(123.01);
        }

        [Fact]
        public void IsBetweenCorrectlyIdentifiesBetweenValue()
        {
            var x = 10;
            var result = x.IsBetween(9, 11);
            result.ShouldEqual(true);
        }

        [Fact]
        public void IsBetweenWorksForBoundaryCase()
        {
            var x = 10;
            var result = x.IsBetween(10, 12, true);
            result.ShouldEqual(true);
        }

        [Fact]
        public void IsBetweenWorksForBoundaryCaseWhenExcludingBoundaries()
        {
            var x = 10;
            var result = x.IsBetween(10, 12, false);
            result.ShouldEqual(false);
        }

        [Fact]
        public void IsBetweenWorksWhenValueNotBetween()
        {
            var x = 10;
            var result = x.IsBetween(100, 110);
            result.ShouldEqual(false);
        }

        [Fact]
        public void TruncateWorksForSimpleDecimal()
        {
            var x = 15.123456M;
            var result = x.TruncateDecimal(2);
            result.ShouldEqual(15.12M);
        }

        [Fact]
        public void TruncateWorksForSimpleDecimalAndDoesNotRoundUp()
        {
            var x = 15.1999M;
            var result = x.TruncateDecimal(1);
            result.ShouldEqual(15.1M);
        }

        [Fact]
        public void TruncateWorksForDecimalWithZeroIntegral()
        {
            var x = 0.0047568M;
            var result = x.TruncateDecimal(4);
            result.ShouldEqual(0.0047M);
        }

        [Fact]
        public void RoundToNearest100Works()
        {
            var x = 792m;
            var result = x.RoundDownToNearest(100);

            result.ShouldEqual(700);
        }

        [Fact]
        public void RoundToNearest1000Works()
        {
            var x = 792m;
            var result = x.RoundDownToNearest(1000);

            result.ShouldEqual(0);
        }

        [Fact]
        public void RoundToNearest10Works()
        {
            var x = 792m;
            var result = x.RoundDownToNearest(10);

            result.ShouldEqual(790);

        }

        [Fact]
        public void RoundToNearest5Works()
        {
            var x = 797m;
            var result = x.RoundDownToNearest(5);

            result.ShouldEqual(795);

        }

        [Fact]
        public void RoundToNearestExactWorks()
        {
            var x = 795m;
            var result = x.RoundDownToNearest(5);

            result.ShouldEqual(795);

        }

        [Fact]
        public void RoundUpToNearest100Works()
        {
            var x = 792m;
            var result = x.RoundUpToNearest(100);

            result.ShouldEqual(800);
        }

        [Fact]
        public void RoundUpToNearest1000Works()
        {
            var x = 792m;
            var result = x.RoundUpToNearest(1000);

            result.ShouldEqual(1000);
        }

        [Fact]
        public void RoundUpToNearest10Works()
        {
            var x = 792m;
            var result = x.RoundUpToNearest(10);

            result.ShouldEqual(800);

        }

        [Fact]
        public void RoundUpToNearest5Works()
        {
            var x = 792m;
            var result = x.RoundUpToNearest(5);

            result.ShouldEqual(795);

        }

        [Fact]
        public void RoundUpToNearestExactWorks()
        {
            var x = 795m;
            var result = x.RoundUpToNearest(5);

            result.ShouldEqual(795);

        }

        [Fact]
        public void AddWithFunc_Works_ByAddingOne()
        {
            var x = 1.2m;
            var result = x.Add(a => a%1 == 0 ? 0 : 1);
            result.ShouldEqual(2.2m);
        }

        [Fact]
        public void AddWithFunc_Works_ByAddingZero()
        {
            var x = 1.0m;
            var result = x.Add(a => a % 1 == 0 ? 0 : 1);
            result.ShouldEqual(1m);
        }
    }
}
