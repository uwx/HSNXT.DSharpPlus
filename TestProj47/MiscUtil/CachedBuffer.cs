using System;

namespace HSNXT.MiscUtil
{
    /// <inheritdoc />
    /// <summary>
    /// Type of buffer returned by CachingBufferManager.
    /// </summary>
    internal class CachedBuffer : IBuffer
    {
        private volatile bool _available;
        private readonly bool _clearOnDispose;

        internal CachedBuffer(int size, bool clearOnDispose)
        {
            Bytes = new byte[size];
            this._clearOnDispose = clearOnDispose;
        }

        internal bool Available
        {
            get => _available;
            set => _available = value;
        }

        public byte[] Bytes { get; }

        public void Dispose()
        {
            if (_clearOnDispose)
            {
                Array.Clear(Bytes, 0, Bytes.Length);
            }
            _available = true;
        }
    }
}
