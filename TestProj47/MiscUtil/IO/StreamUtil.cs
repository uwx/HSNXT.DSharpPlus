using System;
using System.IO;

namespace HSNXT.MiscUtil.IO
{
    /// <summary>
    /// Collection of utility methods which operate on streams.
    /// (With C# 3.0, these could well become extension methods on Stream.)
    /// </summary>
    public static class StreamUtil
    {
        private const int DefaultBufferSize = 8*1024;

        /// <summary>
        /// Reads the given stream up to the end, returning the data as a byte
        /// array.
        /// </summary>
        /// <param name="input">The stream to read from</param>
        /// <exception cref="ArgumentNullException">input is null</exception>
        /// <exception cref="IOException">An error occurs while reading from the stream</exception>
        /// <returns>The data read from the stream</returns>
        public static byte[] ReadFully(Stream input)
        {
            return ReadFully(input, DefaultBufferSize);
        }

        /// <summary>
        /// Reads the given stream up to the end, returning the data as a byte
        /// array, using the given buffer size.
        /// </summary>
        /// <param name="input">The stream to read from</param>
        /// <param name="bufferSize">The size of buffer to use when reading</param>
        /// <exception cref="ArgumentNullException">input is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">bufferSize is less than 1</exception>
        /// <exception cref="IOException">An error occurs while reading from the stream</exception>
        /// <returns>The data read from the stream</returns>
        public static byte[] ReadFully(Stream input, int bufferSize)
        {
            if (bufferSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            }
            return ReadFully(input, new byte[bufferSize]);
        }

        /// <summary>
        /// Reads the given stream up to the end, returning the data as a byte
        /// array, using the given buffer for transferring data. Note that the
        /// current contents of the buffer is ignored, so the buffer needn't
        /// be cleared beforehand.
        /// </summary>
        /// <param name="input">The stream to read from</param>
        /// <param name="buffer">The buffer to use to transfer data</param>
        /// <exception cref="ArgumentNullException">input is null</exception>
        /// <exception cref="ArgumentNullException">buffer is null</exception>
        /// <exception cref="IOException">An error occurs while reading from the stream</exception>
        /// <returns>The data read from the stream</returns>
        public static byte[] ReadFully(Stream input, IBuffer buffer)
        {
            if (buffer==null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            return ReadFully(input, buffer.Bytes);
        }

        /// <summary>
        /// Reads the given stream up to the end, returning the data as a byte
        /// array, using the given buffer for transferring data. Note that the
        /// current contents of the buffer is ignored, so the buffer needn't
        /// be cleared beforehand.
        /// </summary>
        /// <param name="input">The stream to read from</param>
        /// <param name="buffer">The buffer to use to transfer data</param>
        /// <exception cref="ArgumentNullException">input is null</exception>
        /// <exception cref="ArgumentNullException">buffer is null</exception>
        /// <exception cref="ArgumentException">buffer is a zero-length array</exception>
        /// <exception cref="IOException">An error occurs while reading from the stream</exception>
        /// <returns>The data read from the stream</returns>
        public static byte[] ReadFully(Stream input, byte[] buffer)
        {
            if (buffer==null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (input==null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (buffer.Length==0)
            {
                throw new ArgumentException("Buffer has length of 0");
            }
            // We could do all our own work here, but using MemoryStream is easier
            // and likely to be just as efficient.
            using (var tempStream = new MemoryStream())
            {
                Copy(input, tempStream, buffer);
                // No need to copy the buffer if it's the right size
                if (tempStream.Length==tempStream.GetBuffer().Length)
                {
                    return tempStream.GetBuffer();
                }
                // Okay, make a copy that's the right size
                return tempStream.ToArray();
            }
        }

        /// <summary>
        /// Copies all the data from one stream into another.
        /// </summary>
        /// <param name="input">The stream to read from</param>
        /// <param name="output">The stream to write to</param>
        /// <exception cref="ArgumentNullException">input is null</exception>
        /// <exception cref="ArgumentNullException">output is null</exception>
        /// <exception cref="IOException">An error occurs while reading or writing</exception>
        public static void Copy(Stream input, Stream output)
        {
            Copy(input, output, DefaultBufferSize);
        }

        /// <summary>
        /// Copies all the data from one stream into another, using a buffer
        /// of the given size.
        /// </summary>
        /// <param name="input">The stream to read from</param>
        /// <param name="output">The stream to write to</param>
        /// <param name="bufferSize">The size of buffer to use when reading</param>
        /// <exception cref="ArgumentNullException">input is null</exception>
        /// <exception cref="ArgumentNullException">output is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">bufferSize is less than 1</exception>
        /// <exception cref="IOException">An error occurs while reading or writing</exception>
        public static void Copy(Stream input, Stream output, int bufferSize)
        {
            if (bufferSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            }
            Copy(input, output, new byte[bufferSize]);
        }

        /// <summary>
        /// Copies all the data from one stream into another, using the given 
        /// buffer for transferring data. Note that the current contents of 
        /// the buffer is ignored, so the buffer needn't be cleared beforehand.
        /// </summary>
        /// <param name="input">The stream to read from</param>
        /// <param name="output">The stream to write to</param>
        /// <param name="buffer">The buffer to use to transfer data</param>
        /// <exception cref="ArgumentNullException">input is null</exception>
        /// <exception cref="ArgumentNullException">output is null</exception>
        /// <exception cref="ArgumentNullException">buffer is null</exception>
        /// <exception cref="IOException">An error occurs while reading or writing</exception>
        public static void Copy(Stream input, Stream output, IBuffer buffer)
        {
            if (buffer==null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            Copy(input, output, buffer.Bytes);
        }

        /// <summary>
        /// Copies all the data from one stream into another, using the given 
        /// buffer for transferring data. Note that the current contents of 
        /// the buffer is ignored, so the buffer needn't be cleared beforehand.
        /// </summary>
        /// <param name="input">The stream to read from</param>
        /// <param name="output">The stream to write to</param>
        /// <param name="buffer">The buffer to use to transfer data</param>
        /// <exception cref="ArgumentNullException">input is null</exception>
        /// <exception cref="ArgumentNullException">output is null</exception>
        /// <exception cref="ArgumentNullException">buffer is null</exception>
        /// <exception cref="ArgumentException">buffer is a zero-length array</exception>
        /// <exception cref="IOException">An error occurs while reading or writing</exception>
        public static void Copy(Stream input, Stream output, byte[] buffer)
        {
            if (buffer==null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (input==null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (output==null)
            {
                throw new ArgumentNullException(nameof(output));
            }
            if (buffer.Length==0)
            {
                throw new ArgumentException("Buffer has length of 0");
            }
            int read;
            while ( (read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        /// <summary>
        /// Reads exactly the given number of bytes from the specified stream.
        /// If the end of the stream is reached before the specified amount
        /// of data is read, an exception is thrown.
        /// </summary>
        /// <param name="input">The stream to read from</param>
        /// <param name="bytesToRead">The number of bytes to read</param>
        /// <exception cref="ArgumentNullException">input is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">bytesToRead is less than 1</exception>
        /// <exception cref="EndOfStreamException">The end of the stream is reached before 
        /// enough data has been read</exception>
        /// <exception cref="IOException">An error occurs while reading from the stream</exception>
        /// <returns>The data read from the stream</returns>
        public static byte[] ReadExactly(Stream input, int bytesToRead)
        {
            if (input==null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (bytesToRead < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(bytesToRead));
            }
            var ret = new byte[bytesToRead];
            var index=0;
            while (index < bytesToRead)
            {
                var read = input.Read(ret, index, bytesToRead-index);
                if (read==0)
                {
                    throw new EndOfStreamException
                        ($"End of stream reached with {bytesToRead - index} byte{(bytesToRead - index == 1 ? "s" : "")} left to read.");
                }
                index += read;
            }
            return ret;
        }
    }
}
