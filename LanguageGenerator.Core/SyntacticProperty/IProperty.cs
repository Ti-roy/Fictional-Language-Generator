using System;
using System.Collections.Generic;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public interface IProperty : IEquatable<IProperty>,IPropertyWithOrderInfoForLinker
    {
        string PropertyName { get; }
        IFrequencyDictionary<IProperty> StartsWithFrequencyFrom { get; }
        IFrequencyDictionary<ISyntacticUnit> SyntacticUnits { get; }
        int FrequencyToStartFromProperty(IProperty propertyToStartFrom);
        int MaxFrequencyToStartFromAnyOf(IEnumerable<IProperty> propertiesToStartFrom);
    }
}