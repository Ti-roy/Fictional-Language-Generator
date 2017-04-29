using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.Repository
{
    public interface ISyntacticUnitRepository
    {
        IList<IProperty> Properties { get; set; }
        IList<ISyntacticUnit> SyntacticUnits { get; set; }

        IProperty GetPropertyWithName(string propertyName);
    }
}