using System.Collections;
using System.Collections.Generic;

// ReSharper disable StringCompareIsCultureSpecific.1
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace HSNXT
{
    public class LazySplitter : IEnumerable<string>
    {
        private readonly string _self;
        private readonly char _c;

        public LazySplitter(string self, char c)
        {
            _self = self;
            _c = c;
        }

        public IEnumerator<string> GetEnumerator() => new LazyEnumerator(_self, _c);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}