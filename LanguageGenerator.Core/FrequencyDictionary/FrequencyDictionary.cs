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


        public FrequencyDictionary(IEnumerable<KeyValuePair<T,int>> frequencyDictionary)
        {
            Dictionary<T, int> dictionary = new Dictionary<T, int>();
            foreach (KeyValuePair<T, int> rootSyntacticUnit in frequencyDictionary)
            {
                dictionary.Add(rootSyntacticUnit.Key, rootSyntacticUnit.Value);
            }
            _valueAndFrequency = dictionary;
        }


        public T GetRandomElementBasedOnFrequency()
        {
            List<KeyValuePair<T, int>> orderLockedDictionary = _valueAndFrequency
                .Select(keyValuePair => new KeyValuePair<T, int>(keyValuePair.Key, keyValuePair.Value))
                .ToList();
            int randomNumberInRangeOfTotal = RandomNumberInRangeOfTotal(orderLockedDictionary);
            return RandomElementBasedOnFrequency(randomNumberInRangeOfTotal, orderLockedDictionary);
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


        public void Add(T key, int value = 100)
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


        private int RandomNumberInRangeOfTotal(List<KeyValuePair<T, int>> orderLockedDictionary)
        {
            int totalFrequency = orderLockedDictionary.Sum(keyValuePair => keyValuePair.Value);
            if (totalFrequency < 1)
                throw new InvalidOperationException("Total frequency of frequencyDictionary is 0.");
            int randomNumberInRangeOfTotal = RandomSingleton.Random.Next(1, totalFrequency);
            return randomNumberInRangeOfTotal;
        }


        private T RandomElementBasedOnFrequency(int randomNumberInRangeOfTotal, List<KeyValuePair<T, int>> orderLockedDictionary)
        {
            int leftFrequency = randomNumberInRangeOfTotal;
            for (int index = 0;; index++)
            {
                leftFrequency -= orderLockedDictionary[index].Value;
                if (leftFrequency <= 0)
                    return orderLockedDictionary[index].Key;
            }
        }
    }
}