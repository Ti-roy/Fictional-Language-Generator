using System;
using System.Collections.Generic;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.SyntacticUnit
{
    public interface IParentSU : ISyntacticUnit,IEquatable<IParentSU>
    {
        IFrequencyDictionary<IProperty> PossibleChildren { get; }
        IFrequencyDictionary<int> ChildrenAmount { get; }
        int GetChildrenAmountBasedOnFrequency();
        IProperty GetChildPropertyBasedOnFrequecyThatCanStartFrom(IProperty propertyToStartFrom);
        IProperty GetChildPropertyBasedOnFrequecyThatCanStartFrom(IEnumerable<IProperty> propertiesToStartFrom);
    }
}