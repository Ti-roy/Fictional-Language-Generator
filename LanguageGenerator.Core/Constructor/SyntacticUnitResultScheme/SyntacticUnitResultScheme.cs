using System;
using System.Collections.Generic;
using System.Text;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


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


        public string TranformResultScaleToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ISyntacticUnitResult syntacticUnitResult in ResultScale)
            {
                stringBuilder.Append(((IRootSU)syntacticUnitResult.ChoosenUnit).StringRepresentation);
            }
            return stringBuilder.ToString();
        }
    }
}