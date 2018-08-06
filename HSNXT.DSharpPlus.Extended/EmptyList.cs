using System;
using System.Collections;
using System.Collections.Generic;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended
{
    internal class EmptyList<T> : IReadOnlyList<DiscordMessage>
    {
        public IEnumerator<DiscordMessage> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int Count => 0;
        public DiscordMessage this[int index] => throw new IndexOutOfRangeException();
    }
}