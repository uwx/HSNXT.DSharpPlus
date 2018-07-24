#if NETSTANDARD1_1 || NETSTANDARD1_3
using System;
using System.Runtime.Serialization;

namespace DSharpPlus.Interactivity
{
    /// <inheritdoc />
    /// <summary>Thrown when a thread on which an operation should execute no longer exists or has no message loop.</summary>
    public class InvalidAsynchronousStateException : ArgumentException
    {
        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException"></see> class.</summary>
        public InvalidAsynchronousStateException() : base()
        {
        }

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException"></see> class with the specified detailed description.</summary>
        /// <param name="message">A detailed description of the error.</param>
        public InvalidAsynchronousStateException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException"></see> class with the specified detailed description and the specified exception.</summary>
        /// <param name="message">A detailed description of the error.</param>
        /// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
        public InvalidAsynchronousStateException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}
#endif