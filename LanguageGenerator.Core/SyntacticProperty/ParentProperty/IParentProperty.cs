using System.Collections.Generic;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.SyntacticProperty.ParentProperty
{
    public interface IParentProperty : IProperty
    {
        IFrequencyDictionary<IParentSU> ParentSyntacticUnits { get; }
        IList<IProperty> MustContainProperties { get; }
    }
}