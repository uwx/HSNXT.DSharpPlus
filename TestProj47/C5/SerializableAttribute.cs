#if NETSTANDARD1_0 || PROFILE328 || PROFILE259
namespace TestProj47.C5
{
    /// <summary>
    /// Stub SerializableAttribute for those profiles that don't expose one.
    /// </summary>
    internal sealed class SerializableAttribute : Attribute
    {
    }
}

#endif