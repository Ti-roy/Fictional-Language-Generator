using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.SUConstroctor.SyntacticUnitResultNamespace
{
    public interface ISyntacticUnitResult
    {
        ISyntacticUnit ChoosenUnit { get;  }
        IProperty Property { get;  }
        IList<ISyntacticUnitResult> Children { get; set; }
        ISyntacticUnitResult ParentResult { get; }
        ISyntacticUnitResult PreviosResult { get; }

        IEnumerable<ISyntacticUnitResult> GetAllParentResults();
    }
}