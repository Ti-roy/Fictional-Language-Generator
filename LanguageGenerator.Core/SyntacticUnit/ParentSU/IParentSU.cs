using System;
using System.Collections.Generic;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;


namespace LanguageGenerator.Core.SyntacticUnit
{
    public interface IParentSU : ISyntacticUnit, IEquatable<IParentSU>, IChildInfoForLinker
    {
        bool DublicateChildrenAllowed { get; set; }
        IParentProperty ParentProperty { get; }
        IFrequencyDictionary<IProperty> PossibleChildren { get; }
        IFrequencyDictionary<int> ChildrenAmount { get; }
        int GetChildrenAmountBasedOnFrequency();
        IProperty GetChildPropertyBasedOnFrequecyThatCanStartFrom(IProperty propertyToStartFrom);
        IProperty GetChildPropertyBasedOnFrequecyThatCanStartFromAnyOf(IEnumerable<IProperty> propertiesToStartFrom);
        IProperty TryGetNecessaryPropertyThatCanStartFromAnyOf(IEnumerable<IProperty> propertiesToStartFrom);
    }
}