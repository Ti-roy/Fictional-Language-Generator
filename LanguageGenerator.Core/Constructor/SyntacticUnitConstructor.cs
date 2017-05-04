using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.Repository.RepositoryLinker;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.Constructor
{
    public class SyntacticUnitConstructor : ISyntacticUnitConstructor
    {
        public SyntacticUnitConstructor(ISyntacticUnitRepository syntacticUnitRepository, IRepositoryLinker repositoryLinker)
        {
            SyntacticUnitRepository = syntacticUnitRepository;
            if (!repositoryLinker.IsRepositoryLinked(SyntacticUnitRepository))
            {
                repositoryLinker.LinkRepository(SyntacticUnitRepository);
            }
        }


        public SyntacticUnitConstructor(ISyntacticUnitRepository syntacticUnitRepository) : this(syntacticUnitRepository, new RepositoryLinker())
        {
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
            {
                throw new InvalidOperationException(
                    "The property" + property.PropertyName + " can`t start from " + startOfConstructionProperty.PropertyName + ".");
            }
        }


        private void DissasembleSchemeToRootSyntacticUnits(SyntacticUnitResultScheme scheme)
        {
            for (ISyntacticUnitResult parentSUResult = FirstOrDefaultParentSyntacticUnit(scheme); parentSUResult != null;
                 parentSUResult = FirstOrDefaultParentSyntacticUnit(scheme))
            {
                List<IProperty> lastProperties = GetScrapeOfLastPropertiesThatGoAfterProperty(scheme, parentSUResult);
                List<ISyntacticUnit> childrenSU =
                    GetOrderedSetOfChildrenSyntacticUnitsThatCanGoAfterLastProperties((IParentSU) parentSUResult.ChoosenUnit, lastProperties);
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
            ISyntacticUnitResult lastRootResult = GetLastRootResultBeforeParentResult(scheme, parentSUResult);
            scrapeOfLastProperties.Add(lastRootResult.Property);
            ISyntacticUnitResult lastResult = lastRootResult;
            while (lastResult.ParentResult != null && !IsResultParentOfResult(lastResult.ParentResult, parentSUResult))
            {
                scrapeOfLastProperties.Add(lastResult.ParentResult.Property);
                lastResult = lastResult.ParentResult;
            }
            return scrapeOfLastProperties;
        }


        private ISyntacticUnitResult GetLastRootResultBeforeParentResult(SyntacticUnitResultScheme scheme, ISyntacticUnitResult parentSUResult)
        {
            return scheme.ResultScale.TakeWhile(result => result != parentSUResult).Last(suResult => suResult.ChoosenUnit is IRootSU);
        }


        private bool IsResultParentOfResult(ISyntacticUnitResult resultThatIsPossibleParent, ISyntacticUnitResult resultThatIsChild)
        {
            return resultThatIsChild.GetAllParentResults().Contains(resultThatIsPossibleParent);
        }


        private List<ISyntacticUnit> GetOrderedSetOfChildrenSyntacticUnitsThatCanGoAfterLastProperties(
            IParentSU parentSU, IEnumerable<IProperty> lastProperties)
        {
            IEnumerable<IProperty> _lastProperties = lastProperties;
            List<ISyntacticUnit> setOfChildrenSyntacticUnits = new List<ISyntacticUnit>();
            List<IProperty> currentPropertyNecessetyProperties = new List<IProperty>(parentSU.ParentProperty.MustContainProperties);
            for (int childrenAmount = parentSU.GetChildrenAmountBasedOnFrequency(); childrenAmount > 0; childrenAmount--)
            {
                IProperty childProperty = GetChildProperty(parentSU, _lastProperties, currentPropertyNecessetyProperties);
                AddToSetSyntacticUnitOfProperty(childProperty, setOfChildrenSyntacticUnits);
                _lastProperties = new[] {childProperty};
            }
            if(currentPropertyNecessetyProperties.Count !=0)
                throw new CouldNotContructParentPropertyWithAllNecesseryPropties("During construction of syntactic unit with property "+ parentSU.ParentProperty.PropertyName +" constructor could not include all necessery properties in result set.");
            return setOfChildrenSyntacticUnits;
        }


        private static void AddToSetSyntacticUnitOfProperty(IProperty childProperty, List<ISyntacticUnit> setOfChildrenSyntacticUnits)
        {
            ISyntacticUnit childPropertySyntacticUnit = childProperty.SyntacticUnits.GetRandomElementBasedOnFrequency();
            setOfChildrenSyntacticUnits.Add(childPropertySyntacticUnit);
        }


        private static IProperty GetChildProperty(
            IParentSU parentSU, IEnumerable<IProperty> _lastProperties, List<IProperty> currentPropertyNecessetyProperties)
        {
            IProperty childProperty = TrySetChildPropertyFromNecessertyProperties(parentSU, _lastProperties, currentPropertyNecessetyProperties);
            if (childProperty == null)
            {
                childProperty = SetFromPossibleChildren(parentSU, _lastProperties);
            }
            return childProperty;
        }


        private static IProperty TrySetChildPropertyFromNecessertyProperties(
            IParentSU parentSU, IEnumerable<IProperty> _lastProperties, List<IProperty> currentPropertyNecessetyProperties)
        {
            IProperty childProperty = parentSU.TryGetNecessaryPropertyThatCanStartFrom(_lastProperties);
            currentPropertyNecessetyProperties.Remove(childProperty);
            return childProperty;
        }


        private static IProperty SetFromPossibleChildren(IParentSU parentSU, IEnumerable<IProperty> _lastProperties)
        {
            return parentSU.GetChildPropertyBasedOnFrequecyThatCanStartFrom(_lastProperties);
        }


        private void ReplaceParent_SU_WithChidrenSU(
            SyntacticUnitResultScheme scheme, ISyntacticUnitResult parentSUResult, List<ISyntacticUnit> childrenSU)
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