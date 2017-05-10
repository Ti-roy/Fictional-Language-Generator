using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.Repository.RepositoryLinker;
using LanguageGenerator.Core.SUConstroctor.Exceptions;
using LanguageGenerator.Core.SUConstroctor.SyntacticUnitResultNamespace;
using LanguageGenerator.Core.SUConstroctor.SyntacticUnitResultSchemeNamespace;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.SUConstroctor
{
    public class SyntacticUnitConstructor : ISyntacticUnitConstructor
    {
        private readonly IRepositoryLinker linker;


        public SyntacticUnitConstructor(ISyntacticUnitRepository syntacticUnitRepository, IRepositoryLinker repositoryLinker)
        {
            linker = repositoryLinker;
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


        public void LinkRepository()
        {
            linker.LinkRepository(SyntacticUnitRepository);
        }


        public string GetResultStringOfProperty(string propertyName)
        {
            return GetResultStringOfProperty(SyntacticUnitRepository.GetPropertyWithName(propertyName));
        }


        public string GetResultStringOfProperty(IProperty property)
        {
            return GetResultSchemeOfProperty(property).TranformResultScaleToString();
        }


        public ISyntacticUnitResultScheme GetResultSchemeOfProperty(string propertyName)
        {
            return GetResultSchemeOfProperty(SyntacticUnitRepository.GetPropertyWithName(propertyName));
        }


        public ISyntacticUnitResultScheme GetResultSchemeOfProperty(IProperty property)
        {
            SyntacticUnitResultScheme scheme = new SyntacticUnitResultScheme(property);
            BuildSchemeToRootSyntacticUnits(scheme);
            return scheme;
        }


        


        private void BuildSchemeToRootSyntacticUnits(SyntacticUnitResultScheme scheme)
        {
            for (ISyntacticUnitResult parentSUResult = FirstOrDefaultParentSyntacticUnit(scheme); parentSUResult != null;
                 parentSUResult = FirstOrDefaultParentSyntacticUnit(scheme))
            {
                List<IProperty> lastProperties = GetScrapeOfLastPropertiesThatGoBeforeSUResult(scheme, parentSUResult);
                List<ISyntacticUnit> childrenSU =
                    GetOrderedSetOfChildrenSyntacticUnitsThatCanGoAfterLastProperties((IParentSU) parentSUResult.ChoosenUnit, lastProperties);
                ReplaceParent_SU_WithChidrenSU(scheme, parentSUResult, childrenSU);
            }
        }


        private ISyntacticUnitResult FirstOrDefaultParentSyntacticUnit(SyntacticUnitResultScheme scheme)
        {
            return scheme.ResultScale.FirstOrDefault(suResult => suResult.ChoosenUnit is IParentSU);
        }


        private List<IProperty> GetScrapeOfLastPropertiesThatGoBeforeSUResult(SyntacticUnitResultScheme scheme, ISyntacticUnitResult parentSUResult)
        {
            List<IProperty> scrapeOfLastProperties = new List<IProperty>();
            ISyntacticUnitResult lastRootResult = GetLastRootResultBeforeParentResult(scheme, parentSUResult);
            scrapeOfLastProperties.Add(lastRootResult.Property);
            ISyntacticUnitResult lastResult = lastRootResult;
            ISyntacticUnitResult parentResults = parentSUResult.ParentResult;
            while (lastResult.ParentResult != null && parentResults != lastResult.ParentResult)
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


        private List<ISyntacticUnit> GetOrderedSetOfChildrenSyntacticUnitsThatCanGoAfterLastProperties(
            IParentSU parentSU, IEnumerable<IProperty> lastProperties)
        {
            IEnumerable<IProperty> _lastProperties = lastProperties;
            List<ISyntacticUnit> setOfChildrenSyntacticUnits = new List<ISyntacticUnit>();
            List<IProperty> currentNecesseryProperiesForConstruction = CreateNewListIfMustContainPropertiesNotEmpty(parentSU);
            for (int childrenAmount = parentSU.GetChildrenAmountBasedOnFrequency(); childrenAmount > 0; childrenAmount--)
            {
                IProperty childProperty = GetChildProperty(parentSU, _lastProperties, currentNecesseryProperiesForConstruction);
                AddToSetSyntacticUnitOfProperty(childProperty, setOfChildrenSyntacticUnits);
                _lastProperties = new[] {childProperty};
            }
            ThrowExceptionIfNotAllNecessaryPropertiesUsed(parentSU, currentNecesseryProperiesForConstruction);
            return setOfChildrenSyntacticUnits;
        }


        private List<IProperty> CreateNewListIfMustContainPropertiesNotEmpty(IParentSU parentSU)
        {
            if (parentSU.ParentProperty.MustContainProperties.Any())
            {
                return new List<IProperty>(parentSU.ParentProperty.MustContainProperties);
            }
            return null;
        }


        private IProperty GetChildProperty(
            IParentSU parentSU, IEnumerable<IProperty> _lastProperties, List<IProperty> currentNecesseryProperiesForConstruction)
        {
            IProperty childProperty =
                TrySetChildPropertyFromNecessertyProperties(parentSU, _lastProperties, currentNecesseryProperiesForConstruction);
            if (CouldntSetChildPropertyFromNecessetyProperties(childProperty))
            {
                childProperty = SetChildPropertyFromPossibleChildren(parentSU, _lastProperties);
            }
            return childProperty;
        }


        private static bool CouldntSetChildPropertyFromNecessetyProperties(IProperty childProperty)
        {
            return childProperty == null;
        }


        private void ThrowExceptionIfNotAllNecessaryPropertiesUsed(IParentSU parentSU, List<IProperty> currentPropertyNecessetyProperties)
        {
            if (currentPropertyNecessetyProperties != null && currentPropertyNecessetyProperties.Count != 0)
            {
                throw new CouldNotContructParentPropertyWithAllNecesseryProptiesException(
                    "During construction of syntactic unit with property " + parentSU.ParentProperty.PropertyName +
                    " constructor could not include all necessery properties in result set.");
            }
        }


        private void AddToSetSyntacticUnitOfProperty(IProperty childProperty, List<ISyntacticUnit> setOfChildrenSyntacticUnits)
        {
            ISyntacticUnit childPropertySyntacticUnit = childProperty.SyntacticUnits.GetRandomElementBasedOnFrequency();
            setOfChildrenSyntacticUnits.Add(childPropertySyntacticUnit);
        }


        private IProperty TrySetChildPropertyFromNecessertyProperties(
            IParentSU parentSU, IEnumerable<IProperty> _lastProperties, List<IProperty> currentNecesseryProperiesForConstruction)
        {
            if (currentNecesseryProperiesForConstruction == null || currentNecesseryProperiesForConstruction.Count == 0)
            {
                return null;
            }
            IProperty childProperty = parentSU.TryGetNecessaryPropertyThatCanStartFromAnyOf(_lastProperties);
            currentNecesseryProperiesForConstruction.Remove(childProperty);
            return childProperty;
        }


        private IProperty SetChildPropertyFromPossibleChildren(IParentSU parentSU, IEnumerable<IProperty> _lastProperties)
        {
            return parentSU.GetChildPropertyBasedOnFrequecyThatCanStartFromAnyOf(_lastProperties);
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