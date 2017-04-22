using System.Collections.Generic;


namespace LanguageGenerator.Core.Constructor.SyntacticUnitResult
{
    class SyntacticUnitResultScale : ISyntacticUnitResultScale
    {
        public IEnumerable<ISyntacticUnitResult> ResultScale { get; }


        public SyntacticUnitResultScale(IEnumerable<ISyntacticUnitResult> resultScale)
        {
            ResultScale = resultScale;
        }
    }
}