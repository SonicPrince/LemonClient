using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Lemon
{
    [Serializable]
    public class MultiDictionary<TKey, TValue> : Dictionary<TKey, List<TValue>>, IDictionary
    {
        public MultiDictionary()
        {
        }

        public MultiDictionary(int capacity) : base(capacity)
        {
        }

        protected MultiDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public void Add(TKey key, TValue value)
        {
            List<TValue> c = null;
            if (!TryGetValue(key, out c))
            {
                c = new List<TValue>();
                Add(key, c);
            }
            c.Add(value);
        }

        public void Remove(TKey key, TValue value)
        {
            List<TValue> c = null;
            TryGetValue(key, out c);

            if (c != null)
                c.Remove(value);
        }

        void IDictionary.Add(object key, object value)
        {
            Add((TKey)key, (TValue)value);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            var data = new List<DictionaryEntry>();

            foreach (var kv in this)
            {
                var key = kv.Key;
                data.AddRange(kv.Value.Select(val => new DictionaryEntry(key, val)));
            }

            return new MultiDictionaryEnumerator(data.GetEnumerator());
        }

        class MultiDictionaryEnumerator : IDictionaryEnumerator
        {
            private IEnumerator<DictionaryEntry> _enumerator;

            public MultiDictionaryEnumerator(IEnumerator<DictionaryEntry> enumerator)
            {
                _enumerator = enumerator;
            }

            public DictionaryEntry Entry { get { return _enumerator.Current; } }

            public object Key { get { return _enumerator.Current.Key; } }
            public object Value { get { return _enumerator.Current.Value; } }
            public object Current { get { return _enumerator.Current; } }

            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                _enumerator.Reset();
            }
        }
    }
}
