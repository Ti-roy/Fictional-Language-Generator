using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.Repository
{
    public interface IInformationAgent
    {
        IList<IProperty> Properties { get; set; }
        IList<ISyntacticUnit> SyntacticUnits { get; set; }

        IEnumerable<ISyntacticUnit> GetSetOfChildren(IParentSU parentSU);
        ISyntacticUnit GetRandomSyntacticUnitsOfProperty(IProperty property);
        IProperty GetPropertyWithName(string propertyName);
        bool DoesPropertyCanStartFrom(IProperty propertyThatStarts, IProperty propertyToStartFrom);
    }
}