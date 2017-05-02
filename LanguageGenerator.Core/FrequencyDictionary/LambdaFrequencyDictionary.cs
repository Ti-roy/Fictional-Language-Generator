using System.Collections.Generic;


namespace LanguageGenerator.Core.FrequencyDictionary
{
    public static class LambdaFrequencyDictionary
    {
        public static FrequencyDictionary<T> ToFrequencyDictionary<T>(this IEnumerable<KeyValuePair<T, int>> collectionOfKeyValuePairs)
        {
            FrequencyDictionary<T> frequencyDictionary = new FrequencyDictionary<T>();
            foreach (KeyValuePair<T, int> collectionOfKeyValuePair in collectionOfKeyValuePairs)
            {
                frequencyDictionary.Add(collectionOfKeyValuePair.Key, collectionOfKeyValuePair.Value);
            }
            return frequencyDictionary;
        }
    }
}