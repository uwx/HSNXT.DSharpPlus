using System;

namespace HSNXT
{
    public partial class Extensions
    {
        /// <summary>
        /// Ensures that the instance is not null, or throws an exception.
        /// </summary>
        /// <param name="instance">The object to verify.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when instance is null.</exception>
        public static void EnsureIsNotNull(this object instance)
        {
            if (instance.IsNull())
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Ensures that the instance is not null, or throws an exception.
        /// </summary>
        /// <param name="instance">The object to verify.</param>
        /// <param name="argumentName">The argumentName to use when exception is thrown.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when argumentName is null, or instance is null.</exception>
        public static void EnsureIsNotNull(this object instance, string argumentName)
        {
            if (instance.IsNull())
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
