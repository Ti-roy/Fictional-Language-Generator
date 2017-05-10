using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.SUConstroctor.SyntacticUnitResultNamespace
{
    public class SyntacticUnitResult : ISyntacticUnitResult
    {
        public ISyntacticUnit ChoosenUnit { get; }
        public IProperty Property { get; }
        public IList<ISyntacticUnitResult> Children { get; set; }
        public ISyntacticUnitResult ParentResult { get; }
        public ISyntacticUnitResult PreviosResult { get; }


        public IEnumerable<ISyntacticUnitResult> GetAllParentResults()
        {
            List<ISyntacticUnitResult> allParentsResults = new List<ISyntacticUnitResult>();
            ISyntacticUnitResult currentResult = this;
            while (currentResult.ParentResult !=null)
            {
                allParentsResults.Add(currentResult.ParentResult);
                currentResult = currentResult.ParentResult;
            }
            return allParentsResults;
        }


        public SyntacticUnitResult(
            ISyntacticUnit choosenUnit, IList<ISyntacticUnitResult> children, ISyntacticUnitResult parentResult, ISyntacticUnitResult previosResult)
        {
            Children = children;
            ParentResult = parentResult;
            ChoosenUnit = choosenUnit;
            Property = choosenUnit.Property;
            PreviosResult = previosResult;
        }


        public SyntacticUnitResult(ISyntacticUnit choosenUnit, ISyntacticUnitResult parentResult, ISyntacticUnitResult previosResult) : this(
            choosenUnit, new List<ISyntacticUnitResult>(), parentResult, previosResult)
        {
        }


        public SyntacticUnitResult(ISyntacticUnit choosenUnit) : this(choosenUnit, null, null, null)
        {
        }
        public SyntacticUnitResult(ISyntacticUnit choosenUnit, ISyntacticUnitResult parentResult) : this(choosenUnit,null, parentResult, null)
        {
        }
    }
}