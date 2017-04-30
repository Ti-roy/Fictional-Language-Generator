using LanguageGenerator.Core.FrequencyDictionary;


namespace LanguageGenerator.Core.SyntacticUnit.ParentSU
{
    public interface IChildInfoForLinker
    {
        IFrequencyDictionary<string> PossibleChildrenByPropertyNames { get; }
    }
}