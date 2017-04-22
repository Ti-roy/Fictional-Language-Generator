using LanguageGenerator.Core.Constructor.SyntacticUnitResult;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.Constructor
{
    public interface ISyntactycUnitConstructor
    {
        ISyntacticRepository Repository { get; set; }
        string GetStringOfProperty(string propertyName);
        string GetStringOfProperty(IProperty property);
        ISyntacticUnitResultScale GetResultScaleOfProperty(string propertyName);
        ISyntacticUnitResultScale GetResultScaleOfProperty(IProperty property);
    }
}