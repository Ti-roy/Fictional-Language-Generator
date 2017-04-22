using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;


namespace LanguageGenerator.Core.SyntacticUnit.ParentSU
{
    //TODO: destroy generics, remove unused setters
    public class ParentSU : IParentSU
    {
        public int Frequency { get; }
        public IProperty Property { get; }


        public IFrequencyDictionary<IProperty> PossibleChildren { get; }
        public IFrequencyDictionary<int> ChildrenAmount { get; }

        
        public ParentSU(
            int frequency,
            IParentProperty parentProperty,
            IFrequencyDictionary<IProperty> possibleChildren,
            IFrequencyDictionary<int> childrenAmount) 
        {
            Frequency = frequency;
            Property = parentProperty;
            PossibleChildren = possibleChildren;
            ChildrenAmount = childrenAmount;
        }


        public ParentSU(int frequency, IParentProperty parentProperty) : this(
            frequency,
            parentProperty,
            new FrequencyDictionary<IProperty>(),
            new FrequencyDictionary<int>())
        {
        }
    }
}