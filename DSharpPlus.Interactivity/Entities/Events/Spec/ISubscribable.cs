namespace DSharpPlus.Interactivity
{
    internal interface ISubscribable<in T>
    {
        void Subscribe(T handler);
        void Unsubscribe(T handler);
    }
}