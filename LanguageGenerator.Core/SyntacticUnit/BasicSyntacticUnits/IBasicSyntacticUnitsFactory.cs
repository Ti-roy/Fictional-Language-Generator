using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits
{
    public interface IBasicSyntacticUnitsFactory
    {
        IRootSU GetSyntacticUnitForStartOfConstraction(int frequency = 1);
        IRootSU GetSyntacticUnitForAny(int frequency = 1);
    }
}
