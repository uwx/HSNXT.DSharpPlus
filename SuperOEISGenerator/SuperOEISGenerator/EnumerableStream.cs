using System;
using System.Collections.Generic;
using System.IO;

namespace SuperOEISGenerator.IO
{
    public class EnumerableStream : Stream
    {
        private bool _isComplete;
        private bool _isComplete2;
        private readonly List<byte> _remainingBytes;
        private readonly IEnumerator<byte[]> _enumerator;

        public EnumerableStream(IEnumerable<byte[]> enumerable)
        {
            _remainingBytes = new List<byte>();
            _enumerator = enumerable.GetEnumerator();
        }
        public EnumerableStream(List<byte> initialContent, IEnumerable<byte[]> enumerable)
        {
            _remainingBytes = initialContent;
            _enumerator = enumerable.GetEnumerator();
        }
        
        private bool _disposed = false;

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("The stream has been disposed.");
            }
            if (_isComplete2)
            {
                throw new EndOfStreamException("The stream is empty and has been marked complete for adding.");
            }
            if (count == 0)
            {
                return 0;
            }

            //var tCount = _remainingBytes.Count;
            while (_remainingBytes.Count < count)
            {
                if (_enumerator.MoveNext())
                {
                    var toAdd = _enumerator.Current ?? throw new ArgumentNullException(nameof(_enumerator.Current), "enumerator can't give out null, please mark as complete instead");
                    _remainingBytes.AddRange(toAdd);
                    //tCount += toAdd.Length;
                }
                else
                {
                    // if has already read the last bits,
                    // return 0 once before starting to error
                    if (_isComplete)
                    {
                        _isComplete2 = true;
                        return 0;
                    }
                    _isComplete = true;
                    break;
                }
            }
            var minCount = Math.Min(_remainingBytes.Count, count);
            
            //CopyTo(int index, T[] array, int arrayIndex, int count)
            _remainingBytes.CopyTo(0, buffer, offset, minCount);
            _remainingBytes.RemoveRange(0, minCount);

            return minCount;
        }

        public override void Write(byte[] buffer, int offset, int count) => throw new NotImplementedException();

        public void CompleteAdding() => throw new NotImplementedException();

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override void Flush() { /* does nothing */ }

        public override long Length => throw new NotImplementedException();

        public override long Position
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin) => throw new NotImplementedException();

        public override void SetLength(long value) => throw new NotImplementedException();

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;
            base.Dispose(disposing);
            _disposed = true;
        }
    }
}