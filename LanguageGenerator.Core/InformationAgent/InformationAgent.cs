using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits;


namespace LanguageGenerator.Core.InformationAgent
{
    public class InformationAgent : IInformationAgent
    {
        public IList<IProperty> Properties { get; set; }
        public IList<ISyntacticUnit> SyntacticUnits { get; set; }
        private readonly Random _random;


        public InformationAgent(IList<IProperty> properties, IList<ISyntacticUnit> syntacticUnits)
        {
            _random = RandomSingleton.Random;
            Properties = properties;
            SyntacticUnits = syntacticUnits;
        }


        public InformationAgent() : this(new List<IProperty>(), new List<ISyntacticUnit>())
        {
        }

        //TODO: if this will not be used in code - delete
        public IProperty GetPropertyWithName(string propertyName)
        {
            return Properties.Single(prop => prop.PropertyName == propertyName);
        }
    }
}