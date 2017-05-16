using System;
using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.SyntacticUnit.ParentSU
{
    public static class ExtensionsIParentSU
    {
        [Obsolete]
        public static T AddChildrenAmount<T>(this T parentSU, int amount, int frequencyForThatAmount = 100) where T : IParentSU
        {
            parentSU.ChildrenAmount.Add(amount, frequencyForThatAmount);
            return parentSU;
        }

        
        public static void AddChildrenAmount<T>(this T parentSU, params KeyValuePair<int, int>[] amountAndFrequencyKeyValuePairs) where T : IParentSU
        {
            foreach (KeyValuePair<int, int> amountAndFrequency in amountAndFrequencyKeyValuePairs)
            {
                parentSU.ChildrenAmount.Add(amountAndFrequency.Key, amountAndFrequency.Value);
            }
        }


        [Obsolete]
        public static T AddPossibleChild<T>(this T parentSU, IProperty property, int frequencyForThatAmount = 100) where T : IParentSU
        {
            parentSU.PossibleChildren.Add(property, frequencyForThatAmount);
            return parentSU;
        }

        [Obsolete]
        public static T AddPossibleChild<T>(this T childInfo, string propertyName, int frequencyForThatAmount = 100) where T : IChildInfoForLinker
        {
            childInfo.PossibleChildrenByPropertyNames.Add(propertyName, frequencyForThatAmount);
            return childInfo;
        }


        public static void AddPossibleChild<T>(this T childInfo, params KeyValuePair<string, int>[] propertyNamesAndFrequencyPairs)
            where T : IChildInfoForLinker
        {
            foreach (KeyValuePair<string, int> propertyNamesAndFrequencyPair in propertyNamesAndFrequencyPairs)
            {
                childInfo.PossibleChildrenByPropertyNames.Add(propertyNamesAndFrequencyPair.Key, propertyNamesAndFrequencyPair.Value);
            }
        }

        public static T AddPossibleChildren<T>(this T parentSU, params KeyValuePair<IProperty, int>[] propertyAndfrequencyPairs) where T : IParentSU
        {
            foreach (KeyValuePair<IProperty, int> propertyAndfrequencyPair in propertyAndfrequencyPairs)
            {
                parentSU.PossibleChildren.Add(propertyAndfrequencyPair.Key, propertyAndfrequencyPair.Value);
            }
            return parentSU;
        }
    }
}