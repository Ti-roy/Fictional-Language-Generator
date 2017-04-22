using System.Collections.Generic;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.SyntacticProperty.ParentProperty
{
    public class ParentProperty : BaseProperty, IParentProperty
    {
        public IList<IParentSU> ParentSyntacticUnits { get; }
        public IList<IProperty> MustContainProperties { get; }


        public ParentProperty(string propertyName, IFrequencyDictionary<IProperty> startsWithFrequencyFrom) : this(
            propertyName,
            startsWithFrequencyFrom,
            new List<IParentSU>(),
            new List<IProperty>())
        {
            PropertyName = propertyName;
        }


        public ParentProperty(
            string propertyName,
            IFrequencyDictionary<IProperty> startsWithFrequencyFrom,
            IList<IParentSU> parentSyntacticUnits,
            IList<IProperty> mustContainProperties) : base(propertyName, startsWithFrequencyFrom)
        {
            PropertyName = propertyName;
            ParentSyntacticUnits = parentSyntacticUnits;
            MustContainProperties = mustContainProperties;
        }


        public ParentProperty(string propertyName) : this(propertyName, new List<IParentSU>(), new List<IProperty>())
        {
            PropertyName = propertyName;
        }


        public ParentProperty(
            string propertyName,
            IList<IParentSU> parentSyntacticUnits,
            IList<IProperty> mustContainProperties) : base(propertyName)
        {
            PropertyName = propertyName;
            ParentSyntacticUnits = parentSyntacticUnits;
            MustContainProperties = mustContainProperties;
        }
    }
}