using System.Collections.Generic;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.SyntacticProperty.RootProperty
{
    //TODO: resolve constrictor hell
    public class RootProperty : BaseProperty, IRootProperty
    {
        public IList<IRootSU> RootSyntacticUnits { get; set; }


        public RootProperty(
            string propertyName,
            IList<IRootSU> rootSyntacticUnits,
            IFrequencyDictionary<IProperty> startsWithFrequencyFrom) : base(propertyName, startsWithFrequencyFrom)
        {
            RootSyntacticUnits = rootSyntacticUnits;
        }


        public RootProperty(string propertyName, IFrequencyDictionary<IProperty> startsWithFrequencyFrom) : base(
            propertyName,
            startsWithFrequencyFrom)
        {
            RootSyntacticUnits = new List<IRootSU>();
        }


        public RootProperty(string propertyName, IList<IRootSU> rootSyntacticUnits) : base(propertyName)
        {
            RootSyntacticUnits = rootSyntacticUnits;
        }


        public RootProperty(string propertyName) : base(propertyName)
        {
        }
    }
}