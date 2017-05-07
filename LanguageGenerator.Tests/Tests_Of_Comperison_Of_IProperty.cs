using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Tests
{
    class Tests_Of_Comperison_Of_IProperty
    {
        protected bool IsTwoIPropertyImplementationsEqual(IProperty property1, IProperty property2)
        {
            return property1.Equals(property2) && property1.GetHashCode() == property2.GetHashCode();
        }
    }
}