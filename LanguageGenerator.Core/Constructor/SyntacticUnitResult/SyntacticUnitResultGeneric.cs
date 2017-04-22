using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.Constructor.SyntacticUnitResult
{
    public class SyntacticUnitResultGeneric : ISyntacticUnitResult 
    {
        public ISyntacticUnit ChoosenUnit { get;  }
        public IProperty Property { get;  }
        public IList<ISyntacticUnitResult> Children { get;  }


        public SyntacticUnitResultGeneric()
        {
        }


        public SyntacticUnitResultGeneric(ISyntacticUnit choosenUnit, IList<ISyntacticUnitResult> children) 
        {
            Children = children;
            ChoosenUnit = choosenUnit;
            Property = choosenUnit.Property;
        }
    }
}