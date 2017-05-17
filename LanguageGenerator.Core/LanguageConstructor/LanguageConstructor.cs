using LanguageGenerator.Core.AbstractFactory;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SUConstroctor;
using LanguageGenerator.Core.SUConstroctor.SyntacticUnitResultSchemeNamespace;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.LanguageConstructor
{
    public class LanguageConstructor : ILanguageFactory, ISyntacticUnitConstructor
    {
        private readonly LanguageFactory _languageFactory;
        private int _lastHashCodeSnippet;
        private readonly SyntacticUnitConstructor _unitConstructor;


        public LanguageConstructor()
        {
            _languageFactory = new LanguageFactory();
            _unitConstructor = new SyntacticUnitConstructor(_languageFactory.Repository);
        }


        ISyntacticUnitRepository ILanguageFactory.Repository
        {
            get { return _languageFactory.Repository; }
        }


        public IRootProperty CreateRootProperty(string propertyName)
        {
            return _languageFactory.CreateRootProperty(propertyName);
        }


        public IParentProperty CreateParentProperty(string propertyName)
        {
            return _languageFactory.CreateParentProperty(propertyName);
        }


        public IRootSU CreateRootSyntacticUnit(string stringRepresentation, IRootProperty itsProperty, int frequency = 100)
        {
            return _languageFactory.CreateRootSyntacticUnit(stringRepresentation, itsProperty, frequency);
        }


        public IRootSU CreateRootSyntacticUnit(string stringRepresentation, string itsPropertyName, int frequency = 100)
        {
            return _languageFactory.CreateRootSyntacticUnit(stringRepresentation, itsPropertyName, frequency);
        }


        public IRootSU CreateRootSyntacticUnitWithLastCreatedProperty(string stringRepresentation, int frequency = 100)
        {
            return _languageFactory.CreateRootSyntacticUnitWithLastCreatedProperty(stringRepresentation, frequency);
        }


        public IParentSU CreateParentSyntacticUnit(IParentProperty itsProperty, int frequency = 100)
        {
            return _languageFactory.CreateParentSyntacticUnit(itsProperty, frequency);
        }


        public IParentSU CreateParentSyntacticUnit(string itsPropertyName, int frequency = 100)
        {
            return _languageFactory.CreateParentSyntacticUnit(itsPropertyName, frequency);
        }


        ISyntacticUnitRepository ISyntacticUnitConstructor.SyntacticUnitRepository
        {
            get { return _languageFactory.Repository; }
        }


        public string GetResultStringOfProperty(string propertyName)
        {
            LinkRepositoryIfItIsntLinked();
            return _unitConstructor.GetResultStringOfProperty(propertyName);
        }


        public string GetResultStringOfProperty(IProperty property)
        {
            LinkRepositoryIfItIsntLinked();
            return _unitConstructor.GetResultStringOfProperty(property);
        }


        public ISyntacticUnitResultScheme GetResultSchemeOfProperty(string propertyName)
        {
            LinkRepositoryIfItIsntLinked();
            return _unitConstructor.GetResultSchemeOfProperty(propertyName);
        }


        public ISyntacticUnitResultScheme GetResultSchemeOfProperty(IProperty property)
        {
            LinkRepositoryIfItIsntLinked();
            return _unitConstructor.GetResultSchemeOfProperty(property);
        }


        public void LinkRepository()
        {
            _unitConstructor.LinkRepository();
        }


        private void LinkRepositoryIfItIsntLinked()
        {
            int currentRepositoryHashCode = _languageFactory.Repository.GetHashCode();
            if (currentRepositoryHashCode != _lastHashCodeSnippet)
            {
                _lastHashCodeSnippet = currentRepositoryHashCode;
                LinkRepository();
            }
        }
    }
}