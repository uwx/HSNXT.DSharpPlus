using BCLExtensions.Tests.TestHelpers;
using FakeItEasy;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace BCLExtensions.Tests.TestHelperTests
{
    public class CustomICollectionTests
    {
        [Fact]
        public void CountPassesThrough()
        {
            var expectedCount = 42;
            var fakeCollection = A.Fake<ICollection<int>>();
            A.CallTo(() => fakeCollection.Count).Returns(expectedCount);

            var customCollection = new CustomICollection<int>(fakeCollection);
            var actualCount = customCollection.Count;

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void GetEnumeratorPassesThrough()
        {
            var expectedEnumerator = new List<int>().GetEnumerator();
            var fakeCollection = A.Fake<ICollection<int>>();
            A.CallTo(() => fakeCollection.GetEnumerator()).Returns(expectedEnumerator);

            var customCollection = new CustomICollection<int>(fakeCollection);
            var actualEnumerator = customCollection.GetEnumerator();

            Assert.Equal(expectedEnumerator, actualEnumerator);
        }

        [Fact]
        public void EnumerableGetEnumeratorPassesThrough()
        {
            var expectedEnumerator = new List<int>().GetEnumerator();
            var fakeCollection = A.Fake<ICollection<int>>();
            A.CallTo(() => fakeCollection.GetEnumerator()).Returns(expectedEnumerator);

            var customCollection = new CustomICollection<int>(fakeCollection);
            var actualEnumerator = ((IEnumerable)customCollection).GetEnumerator();

            Assert.Equal(expectedEnumerator, actualEnumerator);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void IsSynchronizedPassesThrough(bool expectedState)
        {
            var fakeCollection = A.Fake<ICollection>(x => x.Implements(typeof(ICollection<int>)));
            A.CallTo(() => fakeCollection.IsSynchronized).Returns(expectedState);

            var customCollection = new CustomICollection<int>((ICollection<int>)fakeCollection);
            var actualIsSynchronized = customCollection.IsSynchronized;

            Assert.Equal(expectedState, actualIsSynchronized);
        }

        [Fact]
        public void SyncRootPassesThrough()
        {
            var expectedSyncRoot = new object();
            var fakeCollection = A.Fake<ICollection>(x => x.Implements(typeof(ICollection<int>)));
            A.CallTo(() => fakeCollection.SyncRoot).Returns(expectedSyncRoot);

            var customCollection = new CustomICollection<int>((ICollection<int>)fakeCollection);
            var actualSyncRoot = customCollection.SyncRoot;

            Assert.Equal(expectedSyncRoot, actualSyncRoot);
        }

        [Fact]
        public void AddCallPassedThrough()
        {
            var fakeCollection = A.Fake<ICollection<int>>();
            
            var customCollection = new CustomICollection<int>(fakeCollection);
            customCollection.Add(42);

            A.CallTo(() => fakeCollection.Add(A<int>._)).MustHaveHappened();
        }

        [Fact]
        public void CopyToCallPassedThrough()
        {
            var fakeCollection = A.Fake<ICollection>(x => x.Implements(typeof(ICollection<int>)));

            var customCollection = new CustomICollection<int>((ICollection<int>)fakeCollection);
            customCollection.CopyTo(new int[5], 3);

            A.CallTo(() => fakeCollection.CopyTo(A<int[]>._,A<int>._)).MustHaveHappened();
        }
    }
}
