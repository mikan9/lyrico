using System.Collections.Generic;

namespace Lyrico.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool IsNullOrEmpty<TKey, TValue> (this IDictionary<TKey, TValue> dict)
        {
            return (dict == null || dict.Count < 1);
        }
    }
}
