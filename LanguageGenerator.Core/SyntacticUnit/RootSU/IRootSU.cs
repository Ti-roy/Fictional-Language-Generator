using System;
using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.SyntacticUnit.RootSU
{
    public interface IRootSU : ISyntacticUnit, IEquatable<IRootSU>
    {
        IRootProperty RootProperty { get; }
        string StringRepresentation { get; }
    }
}