using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.Constructor
{
    public class SyntacticUnitResult : ISyntacticUnitResult
    {
        public ISyntacticUnit ChoosenUnit { get; }
        public IProperty Property { get; }
        public IList<ISyntacticUnitResult> Children { get; set; }
        public ISyntacticUnitResult ParentResult { get; }


        public SyntacticUnitResult(ISyntacticUnitResult parentResult)
        {
            ParentResult = parentResult;
        }


        public SyntacticUnitResult(ISyntacticUnit choosenUnit, IList<ISyntacticUnitResult> children, ISyntacticUnitResult parentResult)
        {
            Children = children;
            ParentResult = parentResult;
            ChoosenUnit = choosenUnit;
            Property = choosenUnit.Property;
        }


        public SyntacticUnitResult(ISyntacticUnit choosenUnit, ISyntacticUnitResult parentResult) : this(
            choosenUnit,
            new List<ISyntacticUnitResult>(),
            parentResult)
        {
        }


        public SyntacticUnitResult(ISyntacticUnit choosenUnit) : this(choosenUnit, new List<ISyntacticUnitResult>(), null)
        {
        }
    }
}