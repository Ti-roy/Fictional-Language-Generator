using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits
{
    public interface IBasicSyntacticUnitsFactory
    {
        IRootSU GetSyntacticUnitForStartOfConstraction();
        IRootSU GetSyntacticUnitForAny();
    }
}
