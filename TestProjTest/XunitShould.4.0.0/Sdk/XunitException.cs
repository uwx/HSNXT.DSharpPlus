using System.Runtime.Serialization;
using NUnit.Framework;

namespace Xunit.Sdk
{
    internal class XunitException : AssertionException
    {
        public XunitException() : base("No msg") { }

        public XunitException(string message)
            : base(message) { }

        protected XunitException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}