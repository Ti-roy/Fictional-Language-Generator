using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.InformationAgent;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits;


namespace LanguageGenerator.Core.Constructor
{
    public class SyntactycUnitConstructor : ISyntactycUnitConstructor
    {
        public IInformationAgent InformationAgent { get; }


        public SyntactycUnitConstructor(IInformationAgent informationAgent)
        {
            InformationAgent = informationAgent;
        }


        public string GetStringOfProperty(string propertyName)
        {
            throw new System.NotImplementedException();
        }


        public string GetStringOfProperty(IProperty property)
        {
            throw new System.NotImplementedException();
        }


        private void CheckIfPropertyCanBeginWithStartOfConstruction(IProperty property)
        {
            IProperty startOfConstructionProperty = BasicSyntacticUnitsSingleton.StartOfConstractionProperty;
            if (property.CanStartFrom(startOfConstructionProperty))
                throw new ArgumentException("The property can`t start from " + startOfConstructionProperty.PropertyName + ".");
        }


        public ISyntacticUnitResultScheme GetResultScaleOfProperty(string propertyName)
        {
            throw new System.NotImplementedException();
        }


        public ISyntacticUnitResultScheme GetResultScaleOfProperty(IProperty property)
        {
            SyntacticUnitResultScheme scheme = new SyntacticUnitResultScheme();

            scheme.ResultScale.Add(new SyntacticUnitResult(BasicSyntacticUnitsSingleton.StartOfConstractionSyntacticUnit));

            CheckIfPropertyCanBeginWithStartOfConstruction(property);

            scheme.ResultScale.Add(new SyntacticUnitResult(property.SyntacticUnits.GetRandomElementBasedOnFrequency()));

            DissasembleSchemeToRootSU(scheme);

            return scheme;
        }


        private void DissasembleSchemeToRootSU(SyntacticUnitResultScheme scheme)
        {
            ISyntacticUnitResult parentSUResult = scheme.ResultScale.FirstOrDefault(suResult => suResult.ChoosenUnit is IParentSU);
            while (parentSUResult != null)
            {
                IEnumerable<ISyntacticUnit> childrenSU = GetChildrenSyntacticUnits((IParentSU) parentSUResult.ChoosenUnit);
                scheme.ResultScale.Remove(parentSUResult);
                foreach (ISyntacticUnit syntacticUnit in childrenSU)
                {
                    scheme.ResultScale.Add(new SyntacticUnitResult(syntacticUnit));
                }
                parentSUResult = scheme.ResultScale.FirstOrDefault(suResult => suResult.ChoosenUnit is IParentSU);
            }
        }


        private IEnumerable<ISyntacticUnit> GetChildrenSyntacticUnits(IParentSU parentSU)
        {
            IProperty lastProperty = BasicSyntacticUnitsSingleton.StartOfConstractionProperty;
            List<ISyntacticUnit> setOfChildrenSyntacticUnits = new List<ISyntacticUnit>();
            int childrenAmount = parentSU.GetChildrenAmountBasedOnDfrequency();
            for (; childrenAmount > 0; childrenAmount--)
            {
                IProperty childProperty = parentSU.GetChildPropertyBasedOnFrequecyThatCanStartFrom(
                    lastProperty);
                ISyntacticUnit childPropertyaSyntacticUnit = childProperty.SyntacticUnits.GetRandomElementBasedOnFrequency();
                setOfChildrenSyntacticUnits.Add(childPropertyaSyntacticUnit);
                lastProperty = childProperty;
            }
            return setOfChildrenSyntacticUnits;
        }


        public IEnumerable<IProperty> GetChildrenPropertiesThatCanStartFrom(IProperty startFrom, IParentProperty parentPropertyWithChilder)
        {
            throw new System.NotImplementedException();
        }
    }
}