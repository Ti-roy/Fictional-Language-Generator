using System;
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
        IRootSU CreateRootSyntacticUnit(string stringRepresentation, int frequency, IRootProperty itsProperty);
        IRootSU CreateRootSyntacticUnit(string stringRepresentation, int frequency, string itsPropertyName);
        IParentSU CreateParentSyntacticUnit(int frequency, IParentProperty itsProperty);
        IParentSU CreateParentSyntacticUnit(int frequency, string itsPropertyName);
    }
}