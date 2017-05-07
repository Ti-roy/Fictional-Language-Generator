using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.Constructor
{
    public interface ISyntacticUnitConstructor
    {
        ISyntacticUnitRepository SyntacticUnitRepository { get; }
        string GetResultStringOfProperty(string propertyName);
        string GetResultStringOfProperty(IProperty property);
        ISyntacticUnitResultScheme GetResultSchemeOfProperty(string propertyName);
        ISyntacticUnitResultScheme GetResultSchemeOfProperty(IProperty property);
        void LinkRepository();
    }
}