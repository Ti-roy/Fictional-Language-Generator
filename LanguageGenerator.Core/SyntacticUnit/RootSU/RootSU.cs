using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.SyntacticUnit.RootSU
{
    public class RootSU : IRootSU
    {
        public int Frequency { get; }
        public IProperty Property { get; }
        public IRootProperty RootProperty { get; }


        public string StringRepresentation { get; }



        public RootSU(string stringRepresentation, int frequency, IRootProperty rootProperty)
        {
            StringRepresentation = stringRepresentation;
            Frequency = frequency;
            RootProperty = rootProperty;
            RootProperty.RootSyntacticUnits.Add(this);
        }
    }
}