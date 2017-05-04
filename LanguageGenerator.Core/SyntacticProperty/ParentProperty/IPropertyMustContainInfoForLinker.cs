using System.Collections.Generic;


namespace LanguageGenerator.Core.SyntacticProperty.ParentProperty
{
    public interface IPropertyMustContainInfoForLinker 
    {
        IList<string> MustContainPropertiesWithNames { get; }
    }
}