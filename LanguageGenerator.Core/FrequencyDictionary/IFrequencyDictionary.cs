using System.Collections.Generic;


namespace LanguageGenerator.Core.FrequencyDictionary
{
    public interface IFrequencyDictionary<T> : IDictionary<T, int>
    {
    }
}