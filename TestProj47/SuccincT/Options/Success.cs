using System;

namespace HSNXT.SuccincT.Options
{
    public static class Success
    {
        public static Success<T> CreateFailure<T>(T error) =>
            error != null ? new Success<T>(error) : throw new ArgumentNullException(nameof(error));
    }
}
