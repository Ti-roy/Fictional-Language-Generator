using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.RootProperty;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits
{
    public class BasicSyntacticUnitsFactory : IBasicSyntacticUnitsFactory
    {
        private readonly string _propertyNameForAny;
        private readonly string _propertyNameForStartOfConstruction;

        public BasicSyntacticUnitsFactory(string propertyNameForAny = "Any", string propertyNameForStartOfConstruction = "StartOfConstruction")
        {
            _propertyNameForAny = propertyNameForAny;
            _propertyNameForStartOfConstruction = propertyNameForStartOfConstruction;
        }


        public IRootSU GetSyntacticUnitForStartOfConstraction(int frequency = 1)
        {
            return  new RootSU.RootSU("", frequency, new RootProperty(_propertyNameForStartOfConstruction, new FrequencyDictionary<IProperty>()));
        }


        public IRootSU GetSyntacticUnitForAny(int frequency = 1)
        {
            return new RootSU.RootSU("", frequency, new RootProperty(_propertyNameForAny, new FrequencyDictionary<IProperty>()));
        }
    }
}