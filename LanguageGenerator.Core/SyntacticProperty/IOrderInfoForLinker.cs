using LanguageGenerator.Core.FrequencyDictionary;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public interface IOrderInfoForLinker
    {
        IFrequencyDictionary<string> StartsWithFrequencyFromPropertyName { get; }
    }
}