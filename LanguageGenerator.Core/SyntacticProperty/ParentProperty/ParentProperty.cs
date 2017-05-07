using System.Collections.Generic;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.SyntacticProperty.ParentProperty
{
    public class ParentProperty : BaseProperty, IParentProperty
    {
        public ParentProperty(string propertyName, IFrequencyDictionary<IProperty> startsWithFrequencyFrom) : this(
            propertyName, startsWithFrequencyFrom, new FrequencyDictionary<IParentSU>(), new List<IProperty>())
        {
        }


        public ParentProperty(
            string propertyName, IFrequencyDictionary<IProperty> startsWithFrequencyFrom, IFrequencyDictionary<IParentSU> parentSyntacticUnits,
            IList<IProperty> mustContainProperties) : base(propertyName, startsWithFrequencyFrom)
        {
            ParentSyntacticUnits = parentSyntacticUnits;
            MustContainProperties = mustContainProperties;
            MustContainPropertiesWithNames = new List<string>();
        }


        public ParentProperty(string propertyName) : this(propertyName, new FrequencyDictionary<IParentSU>(), new List<IProperty>())
        {
        }


        public ParentProperty(
            string propertyName, IFrequencyDictionary<IParentSU> parentSyntacticUnits, IList<IProperty> mustContainProperties) : base(propertyName)
        {
            ParentSyntacticUnits = parentSyntacticUnits;
            MustContainProperties = mustContainProperties;
            MustContainPropertiesWithNames = new List<string>();
        }


        public IFrequencyDictionary<IParentSU> ParentSyntacticUnits { get; }
        public IList<IProperty> MustContainProperties { get; }
        public IList<string> MustContainPropertiesWithNames { get; }


        public override IFrequencyDictionary<ISyntacticUnit> SyntacticUnits
        {
            get
            {
                FrequencyDictionary<ISyntacticUnit> dictionary = new FrequencyDictionary<ISyntacticUnit>();
                foreach (KeyValuePair<IParentSU, int> rootSyntacticUnit in ParentSyntacticUnits)
                {
                    dictionary.Add(rootSyntacticUnit.Key, rootSyntacticUnit.Value);
                }
                return dictionary;
            }
        }
    }
}