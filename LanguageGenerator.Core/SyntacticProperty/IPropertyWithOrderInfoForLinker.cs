using LanguageGenerator.Core.FrequencyDictionary;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public interface IPropertyWithOrderInfoForLinker
    {
        IFrequencyDictionary<string> StartsWithFrequencyFromPropertyName { get; }
    }
}