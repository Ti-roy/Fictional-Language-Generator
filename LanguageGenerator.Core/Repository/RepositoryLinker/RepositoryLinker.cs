using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;


namespace LanguageGenerator.Core.Repository.RepositoryLinker
{
    public class RepositoryLinker : IRepositoryLinker
    {
        //TODO:Better to cover this class with tests
        public bool IsRepositoryLinked(ISyntacticUnitRepository repository)
        {
            return IsAllChildLinkInfoEmpty(repository) && IsAllOrderLinkInfoEmpty(repository);
        }


        public void LinkRepository(ISyntacticUnitRepository repository)
        {
            SetOrderFromOrderInfo(repository);
            SetChildInfoForParentSyntacticUnits(repository);
        }


        private bool IsAllOrderLinkInfoEmpty(ISyntacticUnitRepository repository)
        {
            return !repository.Properties.OfType<IOrderInfoForLinker>().Any(info => info.StartsWithFrequencyFromPropertyName.Any());
        }


        private bool IsAllChildLinkInfoEmpty(ISyntacticUnitRepository repository)
        {
            return !repository.SyntacticUnits.OfType<IChildInfoForLinker>().Any(info => info.PossibleChildrenByPropertyNames.Any());
        }


        private void SetChildInfoForParentSyntacticUnits(ISyntacticUnitRepository repository)
        {
            foreach (ISyntacticUnit repositorySyntacticUnit in repository.SyntacticUnits)
            {
                if (repositorySyntacticUnit is IParentSU && repositorySyntacticUnit is IChildInfoForLinker)
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
                if (repositoryProperty is IOrderInfoForLinker)
                {
                    IOrderInfoForLinker orderInfo = (IOrderInfoForLinker) repositoryProperty;
                    foreach (KeyValuePair<string, int> keyValuePair in orderInfo.StartsWithFrequencyFromPropertyName)
                    {
                        repositoryProperty.StartsWithFrequencyFrom.Add(repository.GetPropertyWithName(keyValuePair.Key), keyValuePair.Value);
                    }
                    orderInfo.StartsWithFrequencyFromPropertyName.Clear();
                }
            }
        }
    }
}