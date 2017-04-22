using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.Repository
{
    public interface ISyntacticRepository
    {
        IList<IProperty> Properties { get; set; }
        IList<ISyntacticUnit> SyntacticUnits { get; set; }

        ISyntacticUnit GetRandomSyntacticUnitsOfProperty(IProperty property);
        IProperty GetPropertyWithName(string propertyName);
        bool DoesPropertyCanStartFrom(IProperty propertyThatStarts, IProperty propertyToStartFrom);
    }
}