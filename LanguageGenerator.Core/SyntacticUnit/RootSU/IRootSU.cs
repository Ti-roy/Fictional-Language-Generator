using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.SyntacticUnit.RootSU
{
    public interface IRootSU : ISyntacticUnit
    {
        IRootProperty RootProperty { get;  }
        string StringRepresentation { get;  }
    }
}