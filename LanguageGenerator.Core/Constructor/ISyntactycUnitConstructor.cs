using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.Constructor
{
    public interface ISyntactycUnitConstructor
    {
        IInformationAgent InformationAgent { get; }
        string GetStringOfProperty(string propertyName);
        string GetStringOfProperty(IProperty property);
        ISyntacticUnitResultScheme GetResultScaleOfProperty(string propertyName);
        ISyntacticUnitResultScheme GetResultScaleOfProperty(IProperty property);
    }
}