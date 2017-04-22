using System;
using System.Collections.Generic;
using LanguageGenerator.Core.Constructor.SyntacticUnitResult;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.Constructor
{
    public class SyntactycUnitConstructor<TBasicUnits> : ISyntactycUnitConstructor where TBasicUnits : IBasicSyntacticUnitsFactory, new()
    {
        public ISyntacticRepository Repository { get; set; }
        IBasicSyntacticUnitsFactory BasicSyntacticUnitsFactory { get; }


        public SyntactycUnitConstructor()
        {
            BasicSyntacticUnitsFactory = new TBasicUnits();
        }


        public string GetStringOfProperty(string propertyName)
        {
            return GetStringOfProperty(Repository.GetPropertyWithName(propertyName));
        }


        public string GetStringOfProperty(IProperty property)
        {
            IRootSU startOfConstruction = BasicSyntacticUnitsFactory.GetSyntacticUnitForStartOfConstraction();
            CheckIfPropertyCanBeginWithStartOfConstruction(property, startOfConstruction);


            List<ISyntacticUnit> constructionScale = new List<ISyntacticUnit>();
            constructionScale.Add(startOfConstruction);
            constructionScale.Add(Repository.GetRandomSyntacticUnitsOfProperty(property));


            return "";
        }


        private void CheckIfPropertyCanBeginWithStartOfConstruction(IProperty property, IRootSU startOfConstruction)
        {
            if (!Repository.DoesPropertyCanStartFrom(property,startOfConstruction.Property)) throw new ArgumentException("The property can`t start from " + startOfConstruction);
        }


        private IParentSU GetFirstNotRootProperty(List<ISyntacticUnit> constructionScale)
        {
            foreach (ISyntacticUnit syntacticUnit in constructionScale)
            {
                if (syntacticUnit is IParentSU) return (IParentSU) syntacticUnit;
            }
            return null;
        }


        public ISyntacticUnitResultScale GetResultScaleOfProperty(string propertyName)
        {
            throw new System.NotImplementedException();
        }


        public ISyntacticUnitResultScale GetResultScaleOfProperty(IProperty property)
        {
            throw new System.NotImplementedException();
        }


        public IEnumerable<IProperty> GetChildrenPropertiesThatCanStartFrom(IProperty startFrom, IParentProperty parentPropertyWithChilder)
        {
            throw new System.NotImplementedException();
        }
    }
}