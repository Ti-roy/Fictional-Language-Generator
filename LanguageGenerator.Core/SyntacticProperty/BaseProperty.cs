using LanguageGenerator.Core.FrequencyDictionary;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public abstract class BaseProperty : IProperty
    {
        public string PropertyName { get; set; }
        public IFrequencyDictionary<IProperty> StartsWithFrequencyFrom { get; set; }


        protected BaseProperty(string propertyName) : this(propertyName, new FrequencyDictionary<IProperty>())
        {
        }

        protected BaseProperty(string propertyName, IFrequencyDictionary<IProperty> startsWithFrequencyFrom)
        {
            PropertyName = propertyName;
            StartsWithFrequencyFrom = startsWithFrequencyFrom;
        }
    }
}