﻿using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;


namespace LanguageGenerator.Core.SyntacticUnit
{
    public interface IParentSU : ISyntacticUnit
    {
        IFrequencyDictionary<IProperty> PossibleChildren { get; }
        IFrequencyDictionary<int> ChildrenAmount { get; }
    }
}