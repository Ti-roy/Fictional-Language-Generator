using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;


namespace LanguageGenerator.Core.SyntacticUnit
{
    public static class ExtensionsIParentSU
    {
        public static T AddChildrenAmount<T>(this T parentSU, int amount, int frequencyForThatAmount = 1000) where T : IParentSU
        {
            parentSU.ChildrenAmount.Add(amount, frequencyForThatAmount);
            return parentSU;
        }


        public static T AddPossibleChild<T>(this T parentSU, IProperty property, int frequencyForThatAmount = 1000) where T : IParentSU
        {
            parentSU.PossibleChildren.Add(property, frequencyForThatAmount);
            return parentSU;
        }


        public static T ForbidDubplicateValues<T>(this T parentSU) where T : IParentSU
        {
            parentSU.DublicateChildrenAllowed = false;
            return parentSU;
        }


        public static T AddPossibleChild<T>(this T childInfo, string propertyName, int frequencyForThatAmount = 1000) where T : IChildInfoForLinker
        {
            childInfo.PossibleChildrenByPropertyNames.Add(propertyName, frequencyForThatAmount);
            return childInfo;
        }
    }
}