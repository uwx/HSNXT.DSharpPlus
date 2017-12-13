using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HSNXT
{
    public class ReadOnlyDictionaryBuilder<TKey, TVal> : Dictionary<TKey, TVal>
    {
        public static implicit operator ReadOnlyDictionary<TKey, TVal>(ReadOnlyDictionaryBuilder<TKey, TVal> self) =>
            new ReadOnlyDictionary<TKey, TVal>(self);
    }
}