using System.Collections.Generic;


namespace LanguageGenerator.Core.Constructor
{
    class SyntacticUnitResultScheme : ISyntacticUnitResultScheme
    {
        public IList<ISyntacticUnitResult> ResultScale { get; }


        public SyntacticUnitResultScheme(IList<ISyntacticUnitResult> resultScale)
        {
            ResultScale = resultScale;
        }


        public SyntacticUnitResultScheme() : this(new List<ISyntacticUnitResult>())
        {
        }
    }
}