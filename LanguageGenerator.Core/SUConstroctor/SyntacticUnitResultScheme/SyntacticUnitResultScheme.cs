using System;
using System.Collections.Generic;
using System.Text;
using LanguageGenerator.Core.SUConstroctor.SyntacticUnitResultNamespace;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.SUConstroctor.SyntacticUnitResultSchemeNamespace
{
    internal class SyntacticUnitResultScheme : ISyntacticUnitResultScheme
    {
        public SyntacticUnitResultScheme(IProperty firstPropertyToAdd, IList<ISyntacticUnitResult> resultScale)
        {
            ResultScale = resultScale;
            AddToSchemeStartSyntacticUnits(firstPropertyToAdd);
        }


        public SyntacticUnitResultScheme(IProperty firstPropertyToAdd) : this(firstPropertyToAdd, new List<ISyntacticUnitResult>())
        {
        }


        public IList<ISyntacticUnitResult> ResultScale { get; }


        public string TranformResultScaleToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ISyntacticUnitResult syntacticUnitResult in ResultScale)
            {
                stringBuilder.Append(((IRootSU) syntacticUnitResult.ChoosenUnit).StringRepresentation);
            }
            return stringBuilder.ToString();
        }


        private void AddToSchemeStartSyntacticUnits(IProperty property)
        {
            ResultScale.Add(new SyntacticUnitResult(BasicSyntacticUnitsSingleton.StartOfConstractionSyntacticUnit));
            CheckIfPropertyCanBeginWithStartOfConstruction(property);
            ResultScale.Add(new SyntacticUnitResult(property.SyntacticUnits.GetRandomElementBasedOnFrequency(), null));
        }


        private void CheckIfPropertyCanBeginWithStartOfConstruction(IProperty property)
        {
            IProperty startOfConstructionProperty = BasicSyntacticUnitsSingleton.StartOfConstructionProperty;
            if (property.FrequencyToStartFromProperty(startOfConstructionProperty) == 0)
            {
                throw new InvalidOperationException(
                    "The property " + property.PropertyName + " can`t start from " + startOfConstructionProperty.PropertyName + ".");
            }
        }
    }
}