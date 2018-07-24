using System.Threading.Tasks;
using DSharpPlus.EventArgs;

namespace DSharpPlus.Interactivity
{
    internal abstract class BaseVerifyMachine<TEventArgs, TContextResult> : IAsyncVerifyMachine<TEventArgs>
        where TEventArgs : DiscordEventArgs
        where TContextResult : InteractivityContext
    {
        protected InteractivityExtension Interactivity { get; }
		
        private readonly TaskCompletionSource<TContextResult> _result = new TaskCompletionSource<TContextResult>();

        protected BaseVerifyMachine(InteractivityExtension interactivity)
        {
            Interactivity = interactivity;
        }

        protected abstract Task<TContextResult> CheckResult(TEventArgs args);

        public async Task<bool> MoveNext(TEventArgs args)
        {
            var result = await CheckResult(args);
            if (result == null) return false;
			
            _result.SetResult(result);
            return true;
        }

        public Task<TContextResult> ExecuteAsync(ISubscribable<IAsyncVerifyMachine<TEventArgs>> master)
        {
            master.Subscribe(this);
            return _result.Task;
        }
    }
}