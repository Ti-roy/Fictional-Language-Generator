using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.RootProperty;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.SyntacticUnit
{
    public static class BasicSyntacticUnitsSingleton
    {
        private static IRootSU _startOfConstructionSyntacticUnit;
        private static IRootSU _anySyntacticUnit;


        public static string PropertyNameForAny { get; set; } = "Any";


        public static string PropertyNameForStartOfConstruction { get; set; } = "Start";


        public static IRootSU StartOfConstractionSyntacticUnit
        {
            get
            {
                InitializeStartOfConstructionIfItsNull();
                return _startOfConstructionSyntacticUnit;
            }
        }


        public static IRootProperty StartOfConstructionProperty
        {
            get
            {
                InitializeStartOfConstructionIfItsNull();
                return _startOfConstructionSyntacticUnit.RootProperty;
            }
        }


        public static IRootSU AnySyntacticUnit
        {
            get
            {
                InitializeAnyIfItsNull();
                return _anySyntacticUnit;
            }
        }


        public static IRootProperty AnyProperty
        {
            get
            {
                InitializeAnyIfItsNull();
                return _anySyntacticUnit.RootProperty;
            }
        }


        private static void InitializeStartOfConstructionIfItsNull()
        {
            if (_startOfConstructionSyntacticUnit == null)
            {
                _startOfConstructionSyntacticUnit = new RootSU.RootSU("", 1, new RootProperty(PropertyNameForStartOfConstruction));
            }
        }


        private static void InitializeAnyIfItsNull()
        {
            if (_anySyntacticUnit == null)
                _anySyntacticUnit = new RootSU.RootSU("", 1, new RootProperty(PropertyNameForAny));
        }
    }
}