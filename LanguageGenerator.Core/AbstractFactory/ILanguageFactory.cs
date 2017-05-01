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
        IRootSU CreateRootSyntacticUnit(string stringRepresentation, IRootProperty itsProperty, int frequency);
        IRootSU CreateRootSyntacticUnit(string stringRepresentation, string itsPropertyName, int frequency);
        IParentSU CreateParentSyntacticUnit(IParentProperty itsProperty, int frequency);
        IParentSU CreateParentSyntacticUnit(string itsPropertyName, int frequency);
    }
}