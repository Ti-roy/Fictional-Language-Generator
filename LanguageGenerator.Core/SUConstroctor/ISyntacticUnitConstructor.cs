using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SUConstroctor.SyntacticUnitResultSchemeNamespace;
using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.SUConstroctor
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