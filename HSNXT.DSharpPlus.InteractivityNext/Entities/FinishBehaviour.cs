namespace DSharpPlus.Interactivity
{
    /// <summary>
    /// Behaviour for the pagination message after pagination ends
    /// </summary>
    public enum FinishBehaviour : byte
    {
        /// <summary>
        /// Interactivity ignores message after timeout. No actions are performed
        /// </summary>
        Ignore,
        
        /// <summary>
        /// Interactivity removes all reactions after timeout
        /// </summary>
        DeleteReactions,
        
        /// <summary>
        /// Interactivity deletes the message after timeout
        /// </summary>
        DeleteMessage
    }
}