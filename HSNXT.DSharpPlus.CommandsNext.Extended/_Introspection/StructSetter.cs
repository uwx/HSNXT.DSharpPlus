namespace DSharpPlus.CommandsNext._Introspection
{
    public delegate void StructSetter<T, in TV>(ref T instance, TV value);
}