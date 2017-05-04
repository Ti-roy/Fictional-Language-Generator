using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;


namespace LanguageGenerator.Core.Repository.RepositoryLinker
{
    public class RepositoryLinker : IRepositoryLinker
    {
        //TODO:Better to cover this class with tests
        public bool IsRepositoryLinked(ISyntacticUnitRepository repository)
        {
            return IsAllChildLinkInfoEmpty(repository) && IsAllOrderLinkInfoEmpty(repository) && IsAllMustContainLinkInfoEmpty(repository);
        }


        public void LinkRepository(ISyntacticUnitRepository repository)
        {
            SetOrderFromOrderInfo(repository);
            SetMustContainInfo(repository);
            SetChildInfoForParentSyntacticUnits(repository);
        }


        //IsAllMustContainLinkInfoEmpty and IsAllOrderLinkInfoEmpty can be joined to not iterate through Properties twice. Left for better code understanding.
        private bool IsAllMustContainLinkInfoEmpty(ISyntacticUnitRepository repository)
        {
            return !repository.Properties.OfType<IPropertyMustContainInfoForLinker>().Any(info => info.MustContainPropertiesWithNames.Any());
        }


        private bool IsAllOrderLinkInfoEmpty(ISyntacticUnitRepository repository)
        {
            return !repository.Properties.OfType<IPropertyWithOrderInfoForLinker>().Any(info => info.StartsWithFrequencyFromPropertyName.Any());
        }


        private bool IsAllChildLinkInfoEmpty(ISyntacticUnitRepository repository)
        {
            return !repository.SyntacticUnits.OfType<IChildInfoForLinker>().Any(info => info.PossibleChildrenByPropertyNames.Any());
        }


        private void SetChildInfoForParentSyntacticUnits(ISyntacticUnitRepository repository)
        {
            foreach (ISyntacticUnit repositorySyntacticUnit in repository.SyntacticUnits)
            {
                if (repositorySyntacticUnit is IParentSU)
                {
                    IParentSU parentSu = (IParentSU) repositorySyntacticUnit;
                    IChildInfoForLinker childInfo = (IChildInfoForLinker) repositorySyntacticUnit;
                    foreach (KeyValuePair<string, int> keyValuePair in childInfo.PossibleChildrenByPropertyNames)
                    {
                        parentSu.PossibleChildren.Add(repository.GetPropertyWithName(keyValuePair.Key), keyValuePair.Value);
                    }
                    childInfo.PossibleChildrenByPropertyNames.Clear();
                }
            }
        }


        private void SetOrderFromOrderInfo(ISyntacticUnitRepository repository)
        {
            foreach (IProperty repositoryProperty in repository.Properties)
            {
                IPropertyWithOrderInfoForLinker propertyWithOrderInfo = repositoryProperty;
                foreach (KeyValuePair<string, int> keyValuePair in propertyWithOrderInfo.StartsWithFrequencyFromPropertyName)
                {
                    repositoryProperty.StartsWithFrequencyFrom.Add(repository.GetPropertyWithName(keyValuePair.Key), keyValuePair.Value);
                }
                propertyWithOrderInfo.StartsWithFrequencyFromPropertyName.Clear();
            }
        }


        private void SetMustContainInfo(ISyntacticUnitRepository repository)
        {
            foreach (IProperty repositoryProperty in repository.Properties)
            {
                if (repositoryProperty is IParentProperty)
                {
                    IParentProperty parentProperty = (IParentProperty)repositoryProperty;
                    foreach (string propertyName in parentProperty.MustContainPropertiesWithNames)
                    {
                        parentProperty.MustContainProperties.Add(repository.GetPropertyWithName(propertyName));
                    }
                    parentProperty.MustContainPropertiesWithNames.Clear();
                }
            }
        }
    }
}