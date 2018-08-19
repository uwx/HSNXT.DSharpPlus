namespace DSharpPlus.Interactivity
{
    public enum TimeoutBehaviour : byte
    {
        // is this actually needed?
        //Default, // ignore
        Ignore,
        DeleteReactions,
        DeleteMessage
    }
}