using System;
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
    public class LanguageConstructor : ILanguageFactory,ISyntacticUnitConstructor
    {
        LanguageFactory languageFactory;
        SUConstroctor.SyntacticUnitConstructor unitConstructor;
        int lastHashCodeSnippet = 0;

        public LanguageConstructor()
        {
            languageFactory = new LanguageFactory();
            unitConstructor = new SUConstroctor.SyntacticUnitConstructor(languageFactory.Repository);
        }
        ISyntacticUnitRepository ILanguageFactory.Repository { get { return languageFactory.Repository; } }
        public IRootProperty CreateRootProperty(string propertyName)
        {
            return languageFactory.CreateRootProperty(propertyName);
        }


        public IParentProperty CreateParentProperty(string propertyName)
        {
            return languageFactory.CreateParentProperty(propertyName);
        }


        public IRootSU CreateRootSyntacticUnit(string stringRepresentation, IRootProperty itsProperty, int frequency = 100)
        {
            return languageFactory.CreateRootSyntacticUnit(stringRepresentation, itsProperty, frequency);
        }


        public IRootSU CreateRootSyntacticUnit(string stringRepresentation, string itsPropertyName, int frequency = 100)
        {
            return languageFactory.CreateRootSyntacticUnit(stringRepresentation, itsPropertyName, frequency);
        }


        public IRootSU CreateRootSyntacticUnitWithLastCreatedProperty(string stringRepresentation, int frequency = 100)
        {
            return languageFactory.CreateRootSyntacticUnitWithLastCreatedProperty(stringRepresentation, frequency);
        }


        public IParentSU CreateParentSyntacticUnit(IParentProperty itsProperty, int frequency = 100)
        {
            return languageFactory.CreateParentSyntacticUnit(itsProperty, frequency);
        }


        public IParentSU CreateParentSyntacticUnit(string itsPropertyName, int frequency = 100)
        {
            return languageFactory.CreateParentSyntacticUnit(itsPropertyName, frequency);
        }


        ISyntacticUnitRepository ISyntacticUnitConstructor.SyntacticUnitRepository { get { return languageFactory.Repository; } }
        public string GetResultStringOfProperty(string propertyName)
        {
            LinkRepositoryIfItIsntLinked();
            return unitConstructor.GetResultStringOfProperty(propertyName);
        }


        public string GetResultStringOfProperty(IProperty property)
        {
            LinkRepositoryIfItIsntLinked();
            return unitConstructor.GetResultStringOfProperty(property);
        }


        public ISyntacticUnitResultScheme GetResultSchemeOfProperty(string propertyName)
        {
            LinkRepositoryIfItIsntLinked();
            return unitConstructor.GetResultSchemeOfProperty(propertyName);
        }


        public ISyntacticUnitResultScheme GetResultSchemeOfProperty(IProperty property)
        {
            LinkRepositoryIfItIsntLinked();
            return unitConstructor.GetResultSchemeOfProperty(property);
        }
        private void LinkRepositoryIfItIsntLinked()
        {
            int currentRepositoryHashCode = languageFactory.Repository.GetHashCode();
            if (currentRepositoryHashCode != lastHashCodeSnippet)
            {
                lastHashCodeSnippet = currentRepositoryHashCode;
                LinkRepository();
        }
        }

        public void LinkRepository()
        {
            unitConstructor.LinkRepository();
        }
    }
}
