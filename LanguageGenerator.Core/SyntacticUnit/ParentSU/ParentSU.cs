using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;


namespace LanguageGenerator.Core.SyntacticUnit.ParentSU
{
    public class ParentSU : IParentSU
    {
        public ParentSU(
            int frequency, IParentProperty parentProperty, IFrequencyDictionary<IProperty> possibleChildren, IFrequencyDictionary<int> childrenAmount)
        {
            Frequency = frequency;
            ParentProperty = parentProperty;
            Property = parentProperty;
            parentProperty.ParentSyntacticUnits.Add(this, frequency);
            PossibleChildren = possibleChildren;
            ChildrenAmount = childrenAmount;
            PossibleChildrenByPropertyNames = new FrequencyDictionary<string>();
        }


        public ParentSU(int frequency, IParentProperty parentProperty) : this(
            frequency, parentProperty, new FrequencyDictionary<IProperty>(), new FrequencyDictionary<int>())
        {
        }


        public IFrequencyDictionary<string> PossibleChildrenByPropertyNames { get; }
        public int Frequency { get; }
        public IProperty Property { get; }


        public bool DublicateChildrenAllowed { get; set; }
        public IParentProperty ParentProperty { get; }
        public IFrequencyDictionary<IProperty> PossibleChildren { get; }
        public IFrequencyDictionary<int> ChildrenAmount { get; }


        public int GetChildrenAmountBasedOnFrequency()
        {
            return ChildrenAmount.GetRandomElementBasedOnFrequency();
        }


        public IProperty GetChildPropertyBasedOnFrequecyThatCanStartFrom(IProperty propertyToStartFrom)
        {
            IFrequencyDictionary<IProperty> childrenPropertiesThatCanStartFromTheProperty = PossibleChildren
                .Where(prop => prop.Key.CanStartFrom(propertyToStartFrom))
                .ToFrequencyDictionary();
            if (childrenPropertiesThatCanStartFromTheProperty.Count == 0)
            {
                throw new InvalidOperationException(
                    "During construction children sequence of property " + Property.PropertyName + " no children were found that can go after " +
                    propertyToStartFrom.PropertyName + ".");
            }
            return childrenPropertiesThatCanStartFromTheProperty.GetRandomElementBasedOnFrequency();
        }


        public IProperty GetChildPropertyBasedOnFrequecyThatCanStartFrom(IEnumerable<IProperty> propertiesToStartFrom)
        {
            IFrequencyDictionary<IProperty> childrenPropertiesThatCanStartFromTheProperty = PossibleChildren
                .Where(prop =>  prop.Key.CanStartFromAnyOf(propertiesToStartFrom))
                .ToFrequencyDictionary();
            if (childrenPropertiesThatCanStartFromTheProperty.Count == 0)
            {
                throw new InvalidOperationException(
                    "During construction children sequence of property " + Property.PropertyName + " no children were found that can go after " +
                    string.Join(" or ", propertiesToStartFrom.Select(prop => prop.PropertyName)) + ".");
            }
            return childrenPropertiesThatCanStartFromTheProperty.GetRandomElementBasedOnFrequency();
        }


        public IProperty TryGetNecessaryPropertyThatCanStartFrom(IEnumerable<IProperty> propertiesToStartFrom)
        {
            return ParentProperty.MustContainProperties.FirstOrDefault(property => property.CanStartFromAnyOf(propertiesToStartFrom));
        }


        public bool Equals(IParentSU other)
        {
            if (other == null)
            {
                return false;
            }
            return Frequency == other.Frequency && Equals(Property, other.Property) && Equals(PossibleChildren, other.PossibleChildren) &&
                   Equals(ChildrenAmount, other.ChildrenAmount);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (!(obj is IParentSU))
            {
                return false;
            }
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