namespace LanguageGenerator.Core.SyntacticUnit
{
    public static class ExtensionsIParentSU
    {
        public static IParentSU WithChildrenAmount(this IParentSU parentSU,int amount,int frequency)
        {
            parentSU.ChildrenAmount.Add(amount,frequency);
            return parentSU;
        }
    }
}