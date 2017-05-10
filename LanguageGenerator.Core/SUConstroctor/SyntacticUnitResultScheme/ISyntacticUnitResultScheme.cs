using System.Collections.Generic;
using LanguageGenerator.Core.SUConstroctor.SyntacticUnitResultNamespace;


namespace LanguageGenerator.Core.SUConstroctor.SyntacticUnitResultSchemeNamespace
{
    public interface ISyntacticUnitResultScheme
    {
        IList<ISyntacticUnitResult> ResultScale { get; }
        string TranformResultScaleToString();
    }
}