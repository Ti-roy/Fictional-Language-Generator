using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.Constructor.SyntacticUnitResult
{
    public interface ISyntacticUnitResult
    {
        ISyntacticUnit ChoosenUnit { get;  }
        IProperty Property { get;  }
        IList<ISyntacticUnitResult> Children { get;  }
    }
}