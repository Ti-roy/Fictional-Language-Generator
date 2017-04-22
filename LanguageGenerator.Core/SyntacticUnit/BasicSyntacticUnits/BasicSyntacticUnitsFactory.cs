using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.RootProperty;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits
{
    class BasicSyntacticUnitsFactory : IBasicSyntacticUnitsFactory
    {
        readonly IRootProperty _propertyForStartOfConstruction;
        readonly IRootProperty _propertyForAny;


        public BasicSyntacticUnitsFactory(string propertyNameForAny, string propertyNameForStartOfConstruction)
        {
            _propertyForStartOfConstruction = new RootProperty(propertyNameForStartOfConstruction, new FrequencyDictionary<IProperty>());
            _propertyForAny = new RootProperty(propertyNameForAny, new FrequencyDictionary<IProperty>());
        }


        public IRootSU GetSyntacticUnitForStartOfConstraction()
        {
            return new RootSU.RootSU("", 0, _propertyForStartOfConstruction);
        }


        public IRootSU GetSyntacticUnitForAny()
        {
            return new RootSU.RootSU("", 0, _propertyForAny);
        }
    }
}