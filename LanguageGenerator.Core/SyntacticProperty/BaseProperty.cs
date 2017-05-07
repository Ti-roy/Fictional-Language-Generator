using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public abstract class BaseProperty : IProperty
    {
        protected BaseProperty(string propertyName) : this(propertyName, new FrequencyDictionary<IProperty>())
        {
        }


        protected BaseProperty(string propertyName, IFrequencyDictionary<IProperty> startsWithFrequencyFrom)
        {
            PropertyName = propertyName;
            StartsWithFrequencyFrom = startsWithFrequencyFrom;
            StartsWithFrequencyFromPropertyName = new FrequencyDictionary<string>();
        }


        public IFrequencyDictionary<string> StartsWithFrequencyFromPropertyName { get; }
        public string PropertyName { get; }
        public IFrequencyDictionary<IProperty> StartsWithFrequencyFrom { get; }
        public abstract IFrequencyDictionary<ISyntacticUnit> SyntacticUnits { get; }





        public int FrequencyToStartFromProperty(IProperty propertyToStartFrom)
        {
            IProperty propertyOfAny = BasicSyntacticUnitsSingleton.AnyProperty;
            if (StartsWithFrequencyFrom.ContainsKey(propertyToStartFrom))
                return StartsWithFrequencyFrom[propertyToStartFrom];
            if (StartsWithFrequencyFrom.ContainsKey(propertyOfAny))
                return StartsWithFrequencyFrom[propertyOfAny];
            return 0;
        }


        public int MaxFrequencyToStartFromAnyOf(IEnumerable<IProperty> propertiesToStartFrom)
        {
            IProperty propertyOfAny = BasicSyntacticUnitsSingleton.AnyProperty;
            IEnumerable<KeyValuePair<IProperty, int>> pairsThatIncludedArgumentSet = StartsWithFrequencyFrom.Where(pair => propertiesToStartFrom.Contains(pair.Key)).ToArray();
            if (pairsThatIncludedArgumentSet.Any())
                return pairsThatIncludedArgumentSet.Max(prop=>prop.Value);
            if (StartsWithFrequencyFrom.ContainsKey(propertyOfAny))
                return StartsWithFrequencyFrom[propertyOfAny];
            return 0;
        }


        public bool Equals(IProperty baseProperty)
        {
            if (ReferenceEquals(null, baseProperty))
                return false;
            if (ReferenceEquals(this, baseProperty))
                return true;
            return PropertyName == baseProperty.PropertyName;
        }


        public override bool Equals(object obj)
        {
            IProperty baseProperty = obj as IProperty;
            return Equals(baseProperty);
        }


        public override int GetHashCode()
        {
            return PropertyName.GetHashCode();
        }
    }
}