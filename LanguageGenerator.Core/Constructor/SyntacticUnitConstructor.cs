using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.AbstractFactory;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.Repository.RepositoryLinker;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.Constructor
{
    public class SyntacticUnitConstructor : ISyntacticUnitConstructor
    {
        public SyntacticUnitConstructor(
            ISyntacticUnitRepository syntacticUnitRepository, IRepositoryLinker repositoryLinker) : this(syntacticUnitRepository)
        {
            if (!repositoryLinker.IsRepositoryLinked(syntacticUnitRepository))
                repositoryLinker.LinkRepository(syntacticUnitRepository);
        }


        public SyntacticUnitConstructor(ISyntacticUnitRepository syntacticUnitRepository)
        {
            SyntacticUnitRepository = syntacticUnitRepository;
        }


        public ISyntacticUnitRepository SyntacticUnitRepository { get; }


        public string GetStringOfProperty(string propertyName)
        {
            return GetStringOfProperty(SyntacticUnitRepository.GetPropertyWithName(propertyName));
        }


        public string GetStringOfProperty(IProperty property)
        {
            return GetResultSchemeOfProperty(property).TranformResultScaleToString();
        }


        public ISyntacticUnitResultScheme GetResultSchemeOfProperty(string propertyName)
        {
            return GetResultSchemeOfProperty(SyntacticUnitRepository.GetPropertyWithName(propertyName));
        }


        public ISyntacticUnitResultScheme GetResultSchemeOfProperty(IProperty property)
        {
            SyntacticUnitResultScheme scheme = new SyntacticUnitResultScheme();
            AddToSchemeStartSyntacticUnits(scheme, property);
            DissasembleSchemeToRootSyntacticUnits(scheme);
            return scheme;
        }


        private void AddToSchemeStartSyntacticUnits(SyntacticUnitResultScheme scheme, IProperty property)
        {
            scheme.ResultScale.Add(new SyntacticUnitResult(BasicSyntacticUnitsSingleton.StartOfConstractionSyntacticUnit));
            CheckIfPropertyCanBeginWithStartOfConstruction(property);
            scheme.ResultScale.Add(new SyntacticUnitResult(property.SyntacticUnits.GetRandomElementBasedOnFrequency(), scheme.ResultScale[0]));
        }


        private void CheckIfPropertyCanBeginWithStartOfConstruction(IProperty property)
        {
            IProperty startOfConstructionProperty = BasicSyntacticUnitsSingleton.StartOfConstructionProperty;
            if (!property.CanStartFrom(startOfConstructionProperty))
                throw new InvalidOperationException(
                    "The property" + property + " can`t start from " + startOfConstructionProperty.PropertyName + ".");
        }


        private void DissasembleSchemeToRootSyntacticUnits(SyntacticUnitResultScheme scheme)
        {
            for (ISyntacticUnitResult parentSUResult = FirstOrDefaultParentSyntacticUnit(scheme); parentSUResult != null;
                 parentSUResult = FirstOrDefaultParentSyntacticUnit(scheme))
            {
                List<IProperty> lastProperties = GetScrapeOfLastPropertiesThatGoAfterProperty(scheme, parentSUResult);
                IEnumerable<ISyntacticUnit> childrenSU =
                    GetChildrenSyntacticUnitsThatCanGoAfterlastProperties((IParentSU) parentSUResult.ChoosenUnit, lastProperties);
                ReplaceParent_SU_WithChidrenSU(scheme, parentSUResult, childrenSU);
            }
        }


        private ISyntacticUnitResult FirstOrDefaultParentSyntacticUnit(SyntacticUnitResultScheme scheme)
        {
            return scheme.ResultScale.FirstOrDefault(suResult => suResult.ChoosenUnit is IParentSU);
        }


        private List<IProperty> GetScrapeOfLastPropertiesThatGoAfterProperty(SyntacticUnitResultScheme scheme, ISyntacticUnitResult parentSUResult)
        {
            List<IProperty> scrapeOfLastProperties = new List<IProperty>();
            ISyntacticUnitResult lastRootResult = GetLastRootResult(scheme);
            scrapeOfLastProperties.Add(lastRootResult.Property);
            ISyntacticUnitResult lastResult = lastRootResult;
            while (lastResult.ParentResult != null && !IsResultParentOfResult(lastResult.ParentResult, parentSUResult))
            {
                scrapeOfLastProperties.Add(lastResult.Property);
                lastResult = lastResult.ParentResult;
            }
            return scrapeOfLastProperties;
        }


        private ISyntacticUnitResult GetLastRootResult(SyntacticUnitResultScheme scheme)
        {
            return scheme.ResultScale.Last(suResult => suResult.ChoosenUnit is IRootSU);
        }


        private bool IsResultParentOfResult(ISyntacticUnitResult resultThatIsPossibleParent, ISyntacticUnitResult resultThatIsChild)
        {
            return resultThatIsChild.GetAllParentResults().Contains(resultThatIsPossibleParent);
        }


        private IEnumerable<ISyntacticUnit> GetChildrenSyntacticUnitsThatCanGoAfterlastProperties(
            IParentSU parentSU, IEnumerable<IProperty> lastProperties)
        {
            IEnumerable<IProperty> _lastProperties = lastProperties;
            List<ISyntacticUnit> setOfChildrenSyntacticUnits = new List<ISyntacticUnit>();
            for (int childrenAmount = parentSU.GetChildrenAmountBasedOnFrequency(); childrenAmount > 0; childrenAmount--)
            {
                IProperty childProperty = parentSU.GetChildPropertyBasedOnFrequecyThatCanStartFrom(_lastProperties);
                ISyntacticUnit childPropertySyntacticUnit = childProperty.SyntacticUnits.GetRandomElementBasedOnFrequency();
                setOfChildrenSyntacticUnits.Add(childPropertySyntacticUnit);
                _lastProperties = new[] {childProperty};
            }
            return setOfChildrenSyntacticUnits;
        }


        private IEnumerable<IProperty> GetSUResultPropertyAndItsParentsExceptTopParent(ISyntacticUnitResult suResult)
        {
            List<IProperty> properties = new List<IProperty>();
            AddAllParentsOfSuResultThatHaveParents(suResult, properties);
            properties.Add(suResult.Property);
            return properties;
        }


        private void AddAllParentsOfSuResultThatHaveParents(ISyntacticUnitResult suResult, List<IProperty> properties)
        {
            ISyntacticUnitResult currentResult = suResult.ParentResult;
            while (currentResult.ParentResult != null)
            {
                properties.Add(currentResult.Property);
                currentResult = currentResult.ParentResult;
            }
        }


        private void ReplaceParent_SU_WithChidrenSU(
            SyntacticUnitResultScheme scheme, ISyntacticUnitResult parentSUResult, IEnumerable<ISyntacticUnit> childrenSU)
        {
            int indexOfTheParentSU = scheme.ResultScale.IndexOf(parentSUResult);
            scheme.ResultScale.RemoveAt(indexOfTheParentSU);
            foreach (ISyntacticUnit syntacticUnit in childrenSU)
            {
                scheme.ResultScale.Insert(indexOfTheParentSU, new SyntacticUnitResult(syntacticUnit, parentSUResult));
                indexOfTheParentSU++;
            }
        }
    }
}