using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSharpPlus.Interactivity
{
    /// <summary>
    /// Represents a machine that takes in arguments, then either returns <c>true</c> to finish execution or
    /// <c>false</c> to continue
    /// </summary>
    /// <typeparam name="TArgs">Arguments to take in</typeparam>
    internal interface IAsyncVerifyMachine<in TArgs>
    {
        Task<bool> MoveNext(TArgs args);
    }
}