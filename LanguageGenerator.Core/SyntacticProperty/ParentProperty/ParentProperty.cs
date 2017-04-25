using System.Collections.Generic;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.SyntacticProperty.ParentProperty
{
    public class ParentProperty : BaseProperty, IParentProperty
    {
        public IFrequencyDictionary<IParentSU> ParentSyntacticUnits { get; }
        public IList<IProperty> MustContainProperties { get; }


        public ParentProperty(string propertyName, IFrequencyDictionary<IProperty> startsWithFrequencyFrom) : this(
            propertyName,
            startsWithFrequencyFrom,
            new FrequencyDictionary<IParentSU>(),
            new List<IProperty>())
        {
        }


        public ParentProperty(
            string propertyName,
            IFrequencyDictionary<IProperty> startsWithFrequencyFrom,
            IFrequencyDictionary<IParentSU> parentSyntacticUnits,
            IList<IProperty> mustContainProperties) : base(propertyName, startsWithFrequencyFrom)
        {
            ParentSyntacticUnits = parentSyntacticUnits;
            MustContainProperties = mustContainProperties;
        }


        public ParentProperty(string propertyName) : this(propertyName, new FrequencyDictionary<IParentSU>(), new List<IProperty>())
        {
        }


        public ParentProperty(
            string propertyName,
            IFrequencyDictionary<IParentSU> parentSyntacticUnits,
            IList<IProperty> mustContainProperties) : base(propertyName)
        {
            ParentSyntacticUnits = parentSyntacticUnits;
            MustContainProperties = mustContainProperties;
        }
    }
}