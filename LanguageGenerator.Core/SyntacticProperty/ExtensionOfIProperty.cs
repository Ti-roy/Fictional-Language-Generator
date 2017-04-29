using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public static class ExtensionOfIProperty
    {
        public static IProperty PropertyCanGoAfter(this IProperty property, IProperty propertyToGoAfter, int withFrequency)
        {
            property.StartsWithFrequencyFrom.Add(propertyToGoAfter, withFrequency);
            return property;
        }


        public static IProperty ChildrenOfSyntacticUnit(this IProperty property, IParentSU syntacticUnit, int withFrequency)
        {
            syntacticUnit.PossibleChildren.Add(property, withFrequency);
            return property;
        }
    }
}