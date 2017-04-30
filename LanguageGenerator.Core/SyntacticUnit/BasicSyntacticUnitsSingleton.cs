using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.RootProperty;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.SyntacticUnit
{
    public static class BasicSyntacticUnitsSingleton 
    {
        static IRootSU _startOfConstructionSyntacticUnit;
        static IRootSU _anySyntacticUnit;
        private static string _propertyNameForAny = "Any";
        private static string _propertyNameForStartOfConstruction= "StartOfConstruction";


        public static string PropertyNameForAny
        {
            get { return _propertyNameForAny; }
            set { _propertyNameForAny = value; }
        }


        public static string PropertyNameForStartOfConstruction
        {
            get { return _propertyNameForStartOfConstruction; }
            set { _propertyNameForStartOfConstruction = value; }
        }


        public static IRootSU StartOfConstractionSyntacticUnit
        {
            get
            {
                InitializeStartOfConstructionIfItsNull();
                return _startOfConstructionSyntacticUnit;
            }
        }


        private static void InitializeStartOfConstructionIfItsNull()
        {
            if (_startOfConstructionSyntacticUnit == null)
                _startOfConstructionSyntacticUnit = new RootSU.RootSU("", 1, new RootProperty(PropertyNameForStartOfConstruction));
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


        private static void InitializeAnyIfItsNull()
        {
            if (_anySyntacticUnit == null) _anySyntacticUnit = new RootSU.RootSU("", 1, new RootProperty(PropertyNameForAny));
        }


        public static IRootProperty AnyProperty
        {
            get
            {
                InitializeAnyIfItsNull();
                return _anySyntacticUnit.RootProperty;
            }
        }
    }
}