using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.SyntacticProperty.ParentProperty
{
    public interface IParentProperty : IProperty
    {
        IList<IParentSU> ParentSyntacticUnits { get; }
        IList<IProperty> MustContainProperties { get; }
    }
}