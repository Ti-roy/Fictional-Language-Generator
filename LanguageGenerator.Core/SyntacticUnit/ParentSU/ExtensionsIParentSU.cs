using System;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;


namespace LanguageGenerator.Core.SyntacticUnit
{
    public static class ExtensionsIParentSU
    {
        public static IParentSU AddChildrenAmount(this IParentSU parentSU, int amount, int frequencyForThatAmount = 1000)
        {
            parentSU.ChildrenAmount.Add(amount, frequencyForThatAmount);
            return parentSU;
        }


        public static IParentSU AddPossibleChild(this IParentSU parentSU, IProperty property, int frequencyForThatAmount = 1000)
        {
            parentSU.PossibleChildren.Add(property, frequencyForThatAmount);
            return parentSU;
        }


        public static IParentSU AddPossibleChild(this IParentSU parentSU, string propertyName, int frequencyForThatAmount = 1000)
        {
            if (parentSU is IChildInfoForLinker)
            {
                IChildInfoForLinker childInfo = (IChildInfoForLinker) parentSU;
                childInfo.PossibleChildrenByPropertyNames.Add(propertyName, frequencyForThatAmount);
                return parentSU;
            }
            throw new InvalidOperationException(
                "Current implementation of IParentSU dont implplement IChildInfoForLinker for linker. You can use overload with property reference.");
        }
    }
}