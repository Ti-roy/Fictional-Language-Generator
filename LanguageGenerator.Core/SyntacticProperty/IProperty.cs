using System;
using LanguageGenerator.Core.FrequencyDictionary;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public interface IProperty :IEquatable<IProperty>
    {
        string PropertyName { get; }
        IFrequencyDictionary<IProperty> StartsWithFrequencyFrom { get;  }
    }
}