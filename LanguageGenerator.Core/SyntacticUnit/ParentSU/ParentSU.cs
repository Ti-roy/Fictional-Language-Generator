using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits;


namespace LanguageGenerator.Core.SyntacticUnit.ParentSU
{
    public class ParentSU : IParentSU
    {
        public int Frequency { get; }
        public IProperty Property { get; }


        public IFrequencyDictionary<IProperty> PossibleChildren { get; }
        public IFrequencyDictionary<int> ChildrenAmount { get; }


        public int GetChildrenAmountBasedOnDfrequency()
        {
            return ChildrenAmount.GetRandomElementBasedOnFrequency();
        }


        public IProperty GetChildPropertyBasedOnFrequecyThatCanStartFrom(IProperty propertyToStartFrom)
        {
            IFrequencyDictionary<IProperty> childrenPropertiesThatCanStartFromTheProperty = PossibleChildren
                .Where(prop => prop.Key.CanStartFrom(propertyToStartFrom))
                .ToFrequencyDictionary();
            return childrenPropertiesThatCanStartFromTheProperty.GetRandomElementBasedOnFrequency();
        }


        public ParentSU(
            int frequency,
            IParentProperty parentProperty,
            IFrequencyDictionary<IProperty> possibleChildren,
            IFrequencyDictionary<int> childrenAmount)
        {
            Frequency = frequency;
            Property = parentProperty;
            parentProperty.ParentSyntacticUnits.Add(this, frequency);
            PossibleChildren = possibleChildren;
            ChildrenAmount = childrenAmount;
        }


        public ParentSU(int frequency, IParentProperty parentProperty) : this(
            frequency,
            parentProperty,
            new FrequencyDictionary<IProperty>(),
            new FrequencyDictionary<int>())
        {
        }


        public bool Equals(IParentSU other)
        {
            if (other == null) return false;
            return (Frequency == other.Frequency && Equals(Property, other.Property) && Equals(PossibleChildren, other.PossibleChildren) &&
                    Equals(ChildrenAmount, other.ChildrenAmount));
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is IParentSU)) return false;
            return Equals((IParentSU) obj);
        }


        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Frequency;
                hashCode = (hashCode * 397) ^ (Property != null ? Property.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PossibleChildren != null ? PossibleChildren.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ChildrenAmount != null ? ChildrenAmount.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}