using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.Repository
{
    public class SyntacticUnitRepository : ISyntacticUnitRepository
    {
        public SyntacticUnitRepository(ISet<IProperty> properties, ISet<ISyntacticUnit> syntacticUnits)
        {
            Properties = properties;
            SyntacticUnits = syntacticUnits;
            AddDefaultProperties();
        }


        public SyntacticUnitRepository() : this(new HashSet<IProperty>(), new HashSet<ISyntacticUnit>())
        {
        }


        public ISet<IProperty> Properties { get; set; }
        public ISet<ISyntacticUnit> SyntacticUnits { get; set; }


        public IProperty GetPropertyWithName(string propertyName)
        {
            return Properties.First(prop => prop.PropertyName == propertyName);
        }


        public IParentProperty GetParentPropertyWithName(string propertyName)
        {
            return (IParentProperty) GetPropertyWithName(propertyName);
        }


        public IRootProperty GetRootPropertyWithName(string propertyName)
        {
            return (IRootProperty) GetPropertyWithName(propertyName);
        }


        private void AddDefaultProperties()
        {
            Properties.Add(BasicSyntacticUnitsSingleton.AnyProperty);
            Properties.Add(BasicSyntacticUnitsSingleton.StartOfConstructionProperty);
        }


        public override int GetHashCode()
        {
            unchecked 
            {         
                int hash = 27;
                hash = (13 * hash) + Properties.GetHashCode();
                hash = (13 * hash) + SyntacticUnits.GetHashCode();
                return hash;
            }
        }
    }
}