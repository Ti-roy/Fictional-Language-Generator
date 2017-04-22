using LanguageGenerator.Core.SyntacticUnit;
using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public interface IRootProperty : IProperty
    {
        IList<IRootSU> RootSyntacticUnits { get; set; }
    }
}