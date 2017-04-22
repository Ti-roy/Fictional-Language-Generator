using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.SyntacticUnit
{
    public interface ISyntacticUnit
    {
        int Frequency { get; }
        IProperty Property { get; }
    }
}