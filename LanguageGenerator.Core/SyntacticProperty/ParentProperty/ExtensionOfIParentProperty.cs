namespace LanguageGenerator.Core.SyntacticProperty.ParentProperty
{
    public static class ExtensionOfIParentProperty
    {
        public static T MustContainProperty<T>(this T mustContainInfo, string propetyNameToContain) where T : IPropertyMustContainInfoForLinker
        {
            mustContainInfo.MustContainPropertiesWithNames.Add(propetyNameToContain);
            return mustContainInfo;
        }


        public static T MustContainProperty<T>(this T parentProperty, IProperty propetyToContain) where T : IParentProperty
        {
            parentProperty.MustContainProperties.Add(propetyToContain);
            return parentProperty;
        }
    }
}