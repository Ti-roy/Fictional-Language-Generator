using LanguageGenerator.Core.InformationAgent;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.Constructor
{
    public interface ISyntacticUnitConstructor
    {
        ISyntacticUnitRepository SyntacticUnitRepository { get; }
        string GetStringOfProperty(string propertyName);
        string GetStringOfProperty(IProperty property);
        ISyntacticUnitResultScheme GetResultSchemeOfProperty(string propertyName);
        ISyntacticUnitResultScheme GetResultSchemeOfProperty(IProperty property);
    }
}