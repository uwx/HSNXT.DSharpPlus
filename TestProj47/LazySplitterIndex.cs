using System.Collections;
using System.Collections.Generic;

namespace HSNXT
{
    public class LazySplitterIndex : IEnumerable<(string, int)>
    {
        private readonly string _self;
        private readonly char _c;

        public LazySplitterIndex(string self, char c)
        {
            _self = self;
            _c = c;
        }

        public IEnumerator<(string, int)> GetEnumerator() => new LazyEnumeratorIndex(_self, _c);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}