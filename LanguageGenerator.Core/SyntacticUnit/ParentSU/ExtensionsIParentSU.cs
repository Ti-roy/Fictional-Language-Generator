using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.SyntacticUnit.ParentSU
{
    public static class ExtensionsIParentSU
    {
        public static T AddChildrenAmount<T>(this T parentSU, int amount, int frequencyForThatAmount = 100) where T : IParentSU
        {
            parentSU.ChildrenAmount.Add(amount, frequencyForThatAmount);
            return parentSU;
        }


        public static T AddPossibleChild<T>(this T parentSU, IProperty property, int frequencyForThatAmount = 100) where T : IParentSU
        {
            parentSU.PossibleChildren.Add(property, frequencyForThatAmount);
            return parentSU;
        }


        public static T ForbidDubplicateValues<T>(this T parentSU) where T : IParentSU
        {
            parentSU.DublicateChildrenAllowed = false;
            return parentSU;
        }


        public static T AddPossibleChild<T>(this T childInfo, string propertyName, int frequencyForThatAmount = 100) where T : IChildInfoForLinker
        {
            childInfo.PossibleChildrenByPropertyNames.Add(propertyName, frequencyForThatAmount);
            return childInfo;
        }
    }
}