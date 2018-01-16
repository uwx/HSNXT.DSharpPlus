using System.Collections.Generic;

namespace HSNXT.SuccincT.Options
{
    public static class OptionExtensionsForDictionaryType
    {
        public static Option<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, 
                                                               TKey key) =>
            dictionary.TryGetValue(key, out var value) 
                ? Option<TValue>.Some(value) 
                : Option<TValue>.None();
    }
}
