using System.Threading.Tasks;
using DSharpPlus.EventArgs;

namespace DSharpPlus.Interactivity
{
    /// <summary>
    /// <p>
    ///     Represents a generic pseudo-event listener for <see cref="DiscordAwaiterHolder{TMachine,TEventArgs}"/>.
    /// </p>
    /// <p>
    ///     It registers itself to the awaiter holder on execution, then is automatically unregistered on completion or
    ///     timeout. Completion happens when the implementation of <see cref="CheckResult"/> returns a non-null value.
    /// </p>
    /// <p>
    ///     On completion, the underlying <see cref="TaskCompletionSource{TResult}"/> is transitioned into the
    ///     "completed" state, and the awaiter is considered to have finished execution.
    /// </p>
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <typeparam name="TContextResult"></typeparam>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// internal class MessageVerifier : DiscordEventAwaiter<MessageCreateEventArgs, MessageContext>
    /// {
    ///     public MessageVerifier(InteractivityExtension interactivity) : base(interactivity)
    ///     {
    ///     }
    /// 
    ///     protected async override Task<MessageContext> CheckResult(MessageCreateEventArgs e) 
    ///     {
    ///         if (!condition) return null; // no result, continue execution
    ///
    ///         // success, will complete Task and remove self from the DiscordAwaiterHolder
    ///         return new MessageContext(Interactivity, e.Message);  
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    internal abstract class DiscordEventAwaiter<TMachine, TEventArgs, TContextResult> : IAsyncVerifyMachine<TEventArgs>
        where TMachine : class, IAsyncVerifyMachine<TEventArgs>
        where TEventArgs : DiscordEventArgs
        where TContextResult : InteractivityContext
    {
        protected InteractivityExtension Interactivity { get; }
		
        private readonly TaskCompletionSource<TContextResult> _result = new TaskCompletionSource<TContextResult>();

        protected DiscordEventAwaiter(InteractivityExtension interactivity)
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

        public Task<TContextResult> ExecuteAsync(ISubscribable<TMachine> master)
        {
            master.Subscribe(this as TMachine);
            return _result.Task;
        }
        
        // TODO a subclass of this that uses Task.WhenAll and Unsubscribe and Task.Delay to time out
    }
}