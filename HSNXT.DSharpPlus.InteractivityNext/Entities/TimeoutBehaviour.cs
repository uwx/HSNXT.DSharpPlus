namespace DSharpPlus.Interactivity
{
    public enum TimeoutBehaviour : byte
    {
        /// <summary>
        /// Doesn't do anything when the time runs out.
        /// </summary>
        Ignore,
        /// <summary>
        /// Deletes the reactions on the message when the time runs out (this includes non-pagination reactions)
        /// </summary>
        DeleteReactions,
        /// <summary>
        /// Deletes the entire message when the time runs out (does not include the caller message)
        /// </summary>
        DeleteMessage
    }
}