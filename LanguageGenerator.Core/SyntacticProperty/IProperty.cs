using LanguageGenerator.Core.FrequencyDictionary;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public interface IProperty
    {
        string PropertyName { get; set; }
        IFrequencyDictionary<IProperty> StartsWithFrequencyFrom { get; set; }
    }
}