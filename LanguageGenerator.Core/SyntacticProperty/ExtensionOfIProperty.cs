using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public static class ExtensionOfIProperty
    {
        public static T PropertyCanGoAfter<T>(this T property, string propertyToGoAfter, int withFrequency = 1000) where T: IProperty
        {
            property.StartsWithFrequencyFromPropertyName.Add(propertyToGoAfter, withFrequency);
            return property;
        }


        public static T PropertyCanGoAfter<T>(this T property, IProperty propertyToGoAfter, int withFrequency = 1000) where T : IProperty
        {
            property.StartsWithFrequencyFrom.Add(propertyToGoAfter, withFrequency);
            return property;
        }


        public static T ChildOfSyntacticUnit<T>(this T property, IParentSU syntacticUnit, int withFrequency = 1000) where T : IProperty
        {
            syntacticUnit.PossibleChildren.Add(property, withFrequency);
            return property;
        }
    }
}