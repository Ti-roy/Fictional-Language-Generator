using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.Repository
{
    public class SyntacticUnitRepository : ISyntacticUnitRepository
    {
        public SyntacticUnitRepository(IList<IProperty> properties, IList<ISyntacticUnit> syntacticUnits)
        {
            Properties = properties;
            SyntacticUnits = syntacticUnits;
            AddDefaultProperties();
        }


        public SyntacticUnitRepository() : this(new List<IProperty>(), new List<ISyntacticUnit>())
        {
        }


        public IList<IProperty> Properties { get; set; }
        public IList<ISyntacticUnit> SyntacticUnits { get; set; }


        public IProperty GetPropertyWithName(string propertyName)
        {
            return Properties.Single(prop => prop.PropertyName == propertyName);
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
    }
}