using System.Collections.Generic;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.SyntacticProperty.RootProperty
{
    //TODO: resolve constrictor hell
    public class RootProperty : BaseProperty, IRootProperty
    {
        public IFrequencyDictionary<IRootSU> RootSyntacticUnits { get;  }
        public override IFrequencyDictionary<ISyntacticUnit> SyntacticUnits { get { return (IFrequencyDictionary<ISyntacticUnit>)RootSyntacticUnits; } }


        public RootProperty(
            string propertyName,
            IFrequencyDictionary<IRootSU> rootSyntacticUnits,
            IFrequencyDictionary<IProperty> startsWithFrequencyFrom) : base(propertyName, startsWithFrequencyFrom)
        {
            RootSyntacticUnits = rootSyntacticUnits;
        }


        public RootProperty(string propertyName, IFrequencyDictionary<IProperty> startsWithFrequencyFrom) : base(
            propertyName,
            startsWithFrequencyFrom)
        {
            RootSyntacticUnits = new FrequencyDictionary<IRootSU>();
        }


        public RootProperty(string propertyName, IFrequencyDictionary<IRootSU> rootSyntacticUnits) : base(propertyName)
        {
            RootSyntacticUnits = rootSyntacticUnits;
        }


        public RootProperty(string propertyName) : this(propertyName, new FrequencyDictionary<IRootSU>())
        {
        }
    }
}