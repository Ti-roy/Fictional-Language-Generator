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