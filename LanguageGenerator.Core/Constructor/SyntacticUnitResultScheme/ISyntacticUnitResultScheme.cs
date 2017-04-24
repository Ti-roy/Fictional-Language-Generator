using System.Collections.Generic;


namespace LanguageGenerator.Core.Constructor
{
    public interface ISyntacticUnitResultScheme
    {
        IList<ISyntacticUnitResult> ResultScale { get; }
    }
}