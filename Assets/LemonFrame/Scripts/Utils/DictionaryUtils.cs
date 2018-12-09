using System;
using System.Collections.Generic;

namespace Lemon
{
    public static class DictionaryUtils
    {
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            var result = default(TValue);
            dict.TryGetValue(key, out result);
            return result;
        }

        public static Dictionary<K, V> Create<K, V>(List<V> data, Func<V, K> key)
        {
            var result = new Dictionary<K, V>();

            foreach (var d in data)
                result.Add(key(d), d);

            return result;
        }

        public static MultiDictionary<K, V> CreateMulti<K, V>(List<V> data, Func<V, K> key)
        {
            var result = new MultiDictionary<K, V>();

            foreach (var d in data)
                result.Add(key(d), d);

            return result;
        }
    }
}
