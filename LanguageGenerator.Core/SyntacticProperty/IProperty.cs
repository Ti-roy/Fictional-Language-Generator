using System;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public interface IProperty :IEquatable<IProperty>
    {
        string PropertyName { get; }
        IFrequencyDictionary<IProperty> StartsWithFrequencyFrom { get;  }
        IFrequencyDictionary<ISyntacticUnit> SyntacticUnits { get; }
        bool CanStartFrom(IProperty propertyToStartFrom);
    }
}