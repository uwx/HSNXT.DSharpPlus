// Decompiled with JetBrains decompiler
// Type: TestProj47.BitStream
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System.IO;

namespace HSNXT
{
    /// <inheritdoc />
    /// <summary>Utility that read and write bits in byte array.</summary>
    internal class BitStream : Stream
    {
        /// <summary>The source bytes.</summary>
        private readonly byte[] _source;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HSNXT.BitStream" /> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public BitStream(int capacity)
        {
            this._source = new byte[capacity];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HSNXT.BitStream" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public BitStream(byte[] source)
        {
            this._source = source;
        }

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
        /// </summary>
        /// <value><c>true</c> if this instance can read; otherwise, <c>false</c>.</value>
        public override bool CanRead => true;

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
        /// </summary>
        /// <value><c>true</c> if this instance can seek; otherwise, <c>false</c>.</value>
        public override bool CanSeek => true;

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
        /// </summary>
        /// <value><c>true</c> if this instance can write; otherwise, <c>false</c>.</value>
        public override bool CanWrite => true;

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, gets the length in bytes of the stream.
        /// </summary>
        /// <value>The length.</value>
        public override long Length => this._source.Length * 8;

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, gets or sets the position within the current stream.
        /// </summary>
        /// <value>The position.</value>
        public override long Position { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        public override void Flush()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            var num1 = this.Position + offset;
            var index1 = 0;
            var num2 = 0;
            var index2 = num1 >> 3;
            var num3 = (int) (num1 - (num1 >> 3 << 3));
            while (num1 < this.Position + offset + count && num1 < this.Length)
            {
                buffer[index1] = ((int) this._source[index2] & 1 << 7 - num3) == 0
                    ? (byte) (buffer[index1] & uint.MaxValue - (ulong) (1 << 7 - num2))
                    : (byte) (buffer[index1] | (uint) (1 << 7 - num2));
                ++num1;
                if (num3 == 7)
                {
                    num3 = 0;
                    ++index2;
                }
                else
                    ++num3;
                if (num2 == 7)
                {
                    num2 = 0;
                    ++index1;
                }
                else
                    ++num2;
            }
            var num4 = (int) (num1 - this.Position - offset);
            this.Position = num1;
            return num4;
        }

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
        /// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position.</param>
        /// <returns>The new position within the current stream.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    this.Position = offset;
                    break;
                case SeekOrigin.Current:
                    this.Position += offset;
                    break;
                case SeekOrigin.End:
                    this.Position = this.Length + offset;
                    break;
            }
            return this.Position;
        }

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, sets the length of the current stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes.</param>
        public override void SetLength(long value)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            var position = this.Position;
            var index1 = offset >> 3;
            var num1 = offset - (offset >> 3 << 3);
            var index2 = position >> 3;
            var num2 = (int) (position - (position >> 3 << 3));
            while (position < this.Position + count && position < this.Length)
            {
                this._source[index2] = ((int) buffer[index1] & 1 << 7 - num1) == 0
                    ? (byte) (this._source[index2] & uint.MaxValue - (ulong) (1 << 7 - num2))
                    : (byte) (this._source[index2] | (uint) (1 << 7 - num2));
                ++position;
                if (num2 == 7)
                {
                    num2 = 0;
                    ++index2;
                }
                else
                    ++num2;
                if (num1 == 7)
                {
                    num1 = 0;
                    ++index1;
                }
                else
                    ++num1;
            }
            this.Position = position;
        }
    }
}