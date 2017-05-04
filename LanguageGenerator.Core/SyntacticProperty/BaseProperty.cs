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


        public bool CanStartFrom(IProperty propertyToStartFrom)
        {
            IProperty propertyOfAny = BasicSyntacticUnitsSingleton.AnyProperty;
            return StartsWithFrequencyFrom.ContainsKey(propertyOfAny) && StartsWithFrequencyFrom[propertyOfAny] > 0 ||
                   StartsWithFrequencyFrom.ContainsKey(propertyToStartFrom) && StartsWithFrequencyFrom[propertyToStartFrom] > 0;
        }


        public bool CanStartFromAnyOf(IEnumerable<IProperty> propertiesToStartFrom)
        {
            return propertiesToStartFrom.Any(property => CanStartFrom(property));
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