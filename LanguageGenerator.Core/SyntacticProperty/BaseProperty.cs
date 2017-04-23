using System;
using LanguageGenerator.Core.FrequencyDictionary;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public abstract class BaseProperty : IProperty
    {
        public string PropertyName { get; }
        public IFrequencyDictionary<IProperty> StartsWithFrequencyFrom { get; }


        protected BaseProperty(string propertyName) : this(propertyName, new FrequencyDictionary<IProperty>())
        {
        }


        protected BaseProperty(string propertyName, IFrequencyDictionary<IProperty> startsWithFrequencyFrom)
        {
            PropertyName = propertyName;
            StartsWithFrequencyFrom = startsWithFrequencyFrom;
        }


        public override bool Equals(object obj)
        {
            IProperty baseProperty = obj as IProperty;
            return Equals(baseProperty);
        }


        public bool Equals(IProperty baseProperty)
        {
            if (ReferenceEquals(null, baseProperty)) return false;
            if (ReferenceEquals(this, baseProperty)) return true;
            return PropertyName == baseProperty.PropertyName;
        }


        public override int GetHashCode()
        {
            return PropertyName.GetHashCode();
        }
    }
}