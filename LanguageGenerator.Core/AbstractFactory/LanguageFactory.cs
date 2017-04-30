using System;
using System.Collections.Generic;
using LanguageGenerator.Core.InformationAgent;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticProperty.RootProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.AbstractFactory
{
    public interface IRepositoryLinker
    {
        bool IsRepositoryLinked(ISyntacticUnitRepository repository);
        void LinkRepository(ISyntacticUnitRepository repository);
    }
    public class RepositoryLinker :IRepositoryLinker
    {
        public bool IsRepositoryLinked(ISyntacticUnitRepository repository)
        {
            //TODO: implement this
            return false;
        }


        public void LinkRepository(ISyntacticUnitRepository repository)
        {
            SetOrderFromOrderInfo(repository);
            SetChildInfoForParentSyntacticUnits(repository);
        }


        private static void SetChildInfoForParentSyntacticUnits(ISyntacticUnitRepository repository)
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
                }
            }
        }
    }

    public class LanguageFactory : ILanguageFactory
    {
        ISyntacticUnitRepository _repository;


        public ISyntacticUnitRepository Repository
        {
            get { return _repository; }
        }


        public LanguageFactory(ISyntacticUnitRepository repository)
        {
            _repository = repository;
        }


        public LanguageFactory() : this(new SyntacticUnitRepository())
        {
        }


        public IRootProperty CreateRootProperty(string propertyName)
        {
            IRootProperty rootProperty = new RootProperty(propertyName);
            _repository.Properties.Add(rootProperty);
            return rootProperty;
        }


        public IParentProperty CreateParentProperty(string propertyName)
        {
            IParentProperty parentProperty = new ParentProperty(propertyName);
            _repository.Properties.Add(parentProperty);
            return parentProperty;
        }


        public IRootSU CreateRootSyntacticUnit(string stringRepresentation, int frequency, IRootProperty itsProperty)
        {
            IRootSU rootSyntacticUnit = new RootSU(stringRepresentation, frequency, itsProperty);
            _repository.SyntacticUnits.Add(rootSyntacticUnit);
            return rootSyntacticUnit;
        }


        public IRootSU CreateRootSyntacticUnit(string stringRepresentation, int frequency, string itsPropertyName)
        {
            IRootProperty rootProperty = (IRootProperty) _repository.GetPropertyWithName(itsPropertyName);
            return CreateRootSyntacticUnit(stringRepresentation, frequency, rootProperty);
        }


        public IParentSU CreateParentSyntacticUnit(int frequency, string itsPropertyName)
        {
            IParentProperty parentProperty = (IParentProperty) _repository.GetPropertyWithName(itsPropertyName);

            return CreateParentSyntacticUnit(frequency, parentProperty);
        }


        public IParentSU CreateParentSyntacticUnit(int frequency, IParentProperty itsProperty)
        {
            IParentSU parentSu = new ParentSU(frequency, itsProperty);
            _repository.SyntacticUnits.Add(parentSu);
            return parentSu;
        }
    }
}