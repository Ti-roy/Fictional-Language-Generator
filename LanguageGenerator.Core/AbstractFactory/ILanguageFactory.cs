using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.AbstractFactory
{
    public interface ILanguageFactory
    {
        ISyntacticUnitRepository Repository { get; }
        IRootProperty CreateRootProperty(string propertyName);
        IParentProperty CreateParentProperty(string propertyName);
        IRootSU CreateRootSyntacticUnit(string stringRepresentation, IRootProperty itsProperty, int frequency = 100);
        IRootSU CreateRootSyntacticUnit(string stringRepresentation, string itsPropertyName, int frequency = 100);
        IRootSU CreateRootSyntacticUnitWithLastCreatedProperty(string stringRepresentation, int frequency = 100);
        IParentSU CreateParentSyntacticUnit(IParentProperty itsProperty, int frequency = 100);
        IParentSU CreateParentSyntacticUnit(string itsPropertyName, int frequency = 100);

    }
}