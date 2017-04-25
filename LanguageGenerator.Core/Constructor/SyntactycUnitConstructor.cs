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
        IBasicSyntacticUnitsFactory BasicSyntacticUnitsFactory { get; }
        Random _random;


        public SyntactycUnitConstructor(IBasicSyntacticUnitsFactory basicSyntacticUnitsFactory, IInformationAgent informationAgent)
        {
            BasicSyntacticUnitsFactory = basicSyntacticUnitsFactory;
            InformationAgent = informationAgent;
        }


        public SyntactycUnitConstructor(IInformationAgent repository) : this(new BasicSyntacticUnitsFactory(), repository)
        {
        }


        public SyntactycUnitConstructor(Random random) : this(new BasicSyntacticUnitsFactory(), new InformationAgent.InformationAgent(random))
        {
            _random = random;
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
            IProperty startOfConstructionProperty = BasicSyntacticUnitsFactory.GetSyntacticUnitForStartOfConstraction().Property;
            if (!InformationAgent.DoesPropertyCanStartFrom(property, startOfConstructionProperty))
                throw new ArgumentException("The property can`t start from " + startOfConstructionProperty.PropertyName + ".");
        }


        private ISyntacticUnitResult GetFirstParentSU(IList<ISyntacticUnitResult> constructionScale)
        {
            foreach (ISyntacticUnitResult syntacticUnit in constructionScale)
            {
                if (syntacticUnit.ChoosenUnit is IParentSU) return syntacticUnit;
            }
            return null;
        }


        public ISyntacticUnitResultScheme GetResultScaleOfProperty(string propertyName)
        {
            throw new System.NotImplementedException();
        }


        public ISyntacticUnitResultScheme GetResultScaleOfProperty(IProperty property)
        {
            SyntacticUnitResultScheme scheme = new SyntacticUnitResultScheme();
            scheme.ResultScale.Add(new SyntacticUnitResult(BasicSyntacticUnitsFactory.GetSyntacticUnitForStartOfConstraction()));

            CheckIfPropertyCanBeginWithStartOfConstruction(property);

            scheme.ResultScale.Add(new SyntacticUnitResult(InformationAgent.GetRandomSyntacticUnitsOfProperty(property)));

            ISyntacticUnitResult parentSUResult = GetFirstParentSU(scheme.ResultScale);
            if (parentSUResult != null) parentSUResult.Children = CreateChildrenSyntacticUnitsOfParent(parentSUResult).ToList();
            throw new NotImplementedException();
        }


        private IEnumerable<ISyntacticUnitResult> CreateChildrenSyntacticUnitsOfParent(ISyntacticUnitResult parentSUResult)
        {
            List<ISyntacticUnitResult> collectionOfChildrenResults = ((IParentSU) parentSUResult.ChoosenUnit)
                .GetSetOfChildren(_random)
                .Select(SU => new SyntacticUnitResult(SU, parentSUResult))
                .ToList<ISyntacticUnitResult>();
            parentSUResult.Children = collectionOfChildrenResults;
            return collectionOfChildrenResults;
        }


        public IEnumerable<IProperty> GetChildrenPropertiesThatCanStartFrom(IProperty startFrom, IParentProperty parentPropertyWithChilder)
        {
            throw new System.NotImplementedException();
        }
    }
}