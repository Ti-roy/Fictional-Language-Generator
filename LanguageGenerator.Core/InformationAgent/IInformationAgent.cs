using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.InformationAgent
{
    public interface IInformationAgent
    {
        IList<IProperty> Properties { get; set; }
        IList<ISyntacticUnit> SyntacticUnits { get; set; }

        IProperty GetPropertyWithName(string propertyName);
    }
}