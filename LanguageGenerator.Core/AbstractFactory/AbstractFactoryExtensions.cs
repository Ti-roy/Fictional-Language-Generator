using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.AbstractFactory
{
    public static class AbstractFactoryExtensions
    {
        public static void CreateRootSyntacticUnitWithLastCreatedProperty(
            this ILanguageFactory factory,
            params KeyValuePair<string, int>[] unitAndFrequencyKeyValuePairs)
        {
            foreach (KeyValuePair<string, int> unitAndFrequencyKeyValuePair in unitAndFrequencyKeyValuePairs)
            {
                factory.CreateRootSyntacticUnitWithLastCreatedProperty(unitAndFrequencyKeyValuePair.Key, unitAndFrequencyKeyValuePair.Value);
            }
        }


        public static void CreateRootSyntacticUnit(
            this ILanguageFactory factory,
            string propertyName,
            params KeyValuePair<string, int>[] unitAndFrequencyKeyValuePairs)
        {
            foreach (KeyValuePair<string, int> unitAndFrequencyKeyValuePair in unitAndFrequencyKeyValuePairs)
            {
                factory.CreateRootSyntacticUnit(unitAndFrequencyKeyValuePair.Key, propertyName, unitAndFrequencyKeyValuePair.Value);
            }
        }


        public static void CreateRootSyntacticUnitsOfProperty(
            this ILanguageFactory factory,
            IRootProperty propertyName,
            params KeyValuePair<string, int>[] unitAndFrequencyKeyValuePairs)
        {
            foreach (KeyValuePair<string, int> unitAndFrequencyKeyValuePair in unitAndFrequencyKeyValuePairs)
            {
                factory.CreateRootSyntacticUnit(unitAndFrequencyKeyValuePair.Key, propertyName, unitAndFrequencyKeyValuePair.Value);
            }
        }
    }
}