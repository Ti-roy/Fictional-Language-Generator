using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.InformationAgent
{
    public class SyntacticUnitRepository : ISyntacticUnitRepository
    {
        public IList<IProperty> Properties { get; set; }
        public IList<ISyntacticUnit> SyntacticUnits { get; set; }


        public SyntacticUnitRepository(IList<IProperty> properties, IList<ISyntacticUnit> syntacticUnits)
        {
            Properties = properties;
            SyntacticUnits = syntacticUnits;
            AddDefaultProperties();
        }


        private void AddDefaultProperties()
        {
            Properties.Add(BasicSyntacticUnitsSingleton.AnyProperty);
            Properties.Add(BasicSyntacticUnitsSingleton.StartOfConstructionProperty);
        }


        public SyntacticUnitRepository() : this(new List<IProperty>(), new List<ISyntacticUnit>())
        {
        }
        

        public IProperty GetPropertyWithName(string propertyName)
        {
            return Properties.Single(prop => prop.PropertyName == propertyName);
        }
    }
}