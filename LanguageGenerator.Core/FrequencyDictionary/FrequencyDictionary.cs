using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace LanguageGenerator.Core.FrequencyDictionary
{
    public class FrequencyDictionary<T> : IFrequencyDictionary<T>
    {
        private readonly Dictionary<T, int> _valueAndFrequency;


        public FrequencyDictionary()
        {
            _valueAndFrequency = new Dictionary<T, int>();
        }


        public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
        {
            return _valueAndFrequency.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        void ICollection<KeyValuePair<T, int>>.Add(KeyValuePair<T, int> item)
        {
            Add(item.Key, item.Value);
        }


        public void Clear()
        {
            _valueAndFrequency.Clear();
        }


        bool ICollection<KeyValuePair<T, int>>.Contains(KeyValuePair<T, int> item)
        {
            return _valueAndFrequency.Contains(item);
        }


        void ICollection<KeyValuePair<T, int>>.CopyTo(KeyValuePair<T, int>[] array, int arrayIndex)
        {
            ((ICollection) _valueAndFrequency).CopyTo(array, arrayIndex);
        }


        bool ICollection<KeyValuePair<T, int>>.Remove(KeyValuePair<T, int> item)
        {
            return ((IDictionary<T, int>) _valueAndFrequency).Remove(item);
        }


        public int Count
        {
            get { return _valueAndFrequency.Count; }
        }


        public bool IsReadOnly
        {
            get { return ((IDictionary) _valueAndFrequency).IsReadOnly; }
        }


        public bool ContainsKey(T key)
        {
            return _valueAndFrequency.ContainsKey(key);
        }


        public void Add(T key, int value = 1000)
        {
            if (value < 0)
                throw new InvalidOperationException("Negative values are not allowed as frequency.");
            _valueAndFrequency.Add(key, value);
        }


        public bool Remove(T key)
        {
            return _valueAndFrequency.Remove(key);
        }


        public bool TryGetValue(T key, out int value)
        {
            return _valueAndFrequency.TryGetValue(key, out value);
        }


        public int this[T key]
        {
            get { return _valueAndFrequency[key]; }
            set { _valueAndFrequency[key] = value; }
        }


        public ICollection<T> Keys
        {
            get { return _valueAndFrequency.Keys; }
        }


        public ICollection<int> Values
        {
            get { return _valueAndFrequency.Values; }
        }
    }
}