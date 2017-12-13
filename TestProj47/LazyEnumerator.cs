using System.Collections;
using System.Collections.Generic;

namespace HSNXT
{
    public class LazyEnumerator : IEnumerator<string>
    {
        private readonly string _self;
        private readonly char _c;
        private int _position;

        public LazyEnumerator(string self, char c)
        {
            _self = self;
            _c = c;
        }

        public bool MoveNext()
        {
            var idx = _self.IndexOf(_c, _position);
            if (idx == -1) return false;

            Current = _self.Substring(_position, idx);

            _position = idx + 1; // + c.Length
            return true;
        }

        public void Reset() => _position = 0;

        public string Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}