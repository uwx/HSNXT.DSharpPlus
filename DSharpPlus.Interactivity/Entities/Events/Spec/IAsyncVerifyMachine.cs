using System.Threading.Tasks;

namespace DSharpPlus.Interactivity
{
    internal interface IAsyncVerifyMachine<in TEventArgs>
    {
        Task<bool> MoveNext(TEventArgs args);
    }
}