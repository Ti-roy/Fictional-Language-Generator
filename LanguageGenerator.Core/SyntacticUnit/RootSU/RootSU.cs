using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core.SyntacticUnit.RootSU
{
    public class RootSU : IRootSU
    {
        public int Frequency { get; }
        public IProperty Property { get { return RootProperty; } }
        public IRootProperty RootProperty { get; }


        public string StringRepresentation { get; }



        public RootSU(string stringRepresentation, int frequency, IRootProperty rootProperty)
        {
            StringRepresentation = stringRepresentation;
            Frequency = frequency;
            RootProperty = rootProperty;
            RootProperty.RootSyntacticUnits.Add(this,frequency);
        }


        public bool Equals(IRootSU other)
        {
            if (other == null)
                return false;
            return Frequency == other.Frequency && Equals(Property, other.Property) && Equals(RootProperty, other.RootProperty) && string.Equals(StringRepresentation, other.StringRepresentation);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is IRootSU)) return false;
            return Equals((IRootSU) obj);
        }


        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Frequency;
                hashCode = (hashCode * 397) ^ (Property != null ? Property.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (RootProperty != null ? RootProperty.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (StringRepresentation != null ? StringRepresentation.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}