using System.Collections.Generic;


namespace LanguageGenerator.Core.Constructor.SyntacticUnitResult
{
    public interface ISyntacticUnitResultScale
    {
        IEnumerable<ISyntacticUnitResult> ResultScale { get; }
    }
}