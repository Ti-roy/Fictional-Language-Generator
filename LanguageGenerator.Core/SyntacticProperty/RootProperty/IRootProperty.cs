using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public interface IRootProperty : IProperty
    {
        IFrequencyDictionary<IRootSU> RootSyntacticUnits { get; set; }
    }
}