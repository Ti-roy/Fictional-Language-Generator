using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits;


namespace LanguageGenerator.Core.InformationAgent
{
    public class InformationAgent : IInformationAgent
    {
        public IList<IProperty> Properties { get; set; }
        public IList<ISyntacticUnit> SyntacticUnits { get; set; }
        private readonly Random _random;
        private IBasicSyntacticUnitsFactory _basicSyntacticUnitsFactory;


        public InformationAgent(
            Random random,
            IBasicSyntacticUnitsFactory basicSyntacticUnitsFactory,
            IList<IProperty> properties,
            IList<ISyntacticUnit> syntacticUnits)
        {
            _basicSyntacticUnitsFactory = basicSyntacticUnitsFactory;
            _random = random;
            Properties = properties;
            SyntacticUnits = syntacticUnits;
        }


        public InformationAgent(Random random, IBasicSyntacticUnitsFactory basicSyntacticUnitsFactory) : this(
            random,
            basicSyntacticUnitsFactory,
            new List<IProperty>(),
            new List<ISyntacticUnit>())
        {
        }


        public InformationAgent(Random random) : this(random, new BasicSyntacticUnitsFactory(), new List<IProperty>(), new List<ISyntacticUnit>())
        {
        }


        //TODO: GetRandomSyntacticUnitsOfProperty consider implement IFrequecny dictionoary for IProperty and destroy this method
        public ISyntacticUnit GetRandomSyntacticUnitsOfProperty(IProperty property)
        {
            IRootProperty rootProperty = property as IRootProperty;
            if (rootProperty != null)
            {
                return rootProperty.RootSyntacticUnits.GetRandomElementBasedOnFrequency(_random);
            }
            IParentProperty parentProperty = property as IParentProperty;
            if (parentProperty != null)
            {
                return parentProperty.ParentSyntacticUnits.GetRandomElementBasedOnFrequency(_random);
            }
            throw new InvalidOperationException("Trying to get syntactic property of uknown property type.");
        }


        public bool DoesPropertyCanStartFrom(IProperty propertyThatStarts, IProperty propertyToStartFrom)
        {
            IProperty anyProperty = _basicSyntacticUnitsFactory.GetSyntacticUnitForAny().Property;
            return (propertyThatStarts.StartsWithFrequencyFrom.ContainsKey(propertyToStartFrom)&&
                propertyThatStarts.StartsWithFrequencyFrom[propertyToStartFrom] > 0) ||
                   (propertyThatStarts.StartsWithFrequencyFrom.ContainsKey(anyProperty) && propertyThatStarts.StartsWithFrequencyFrom[anyProperty] > 0);
            //foreach (KeyValuePair<IProperty, int> keyValuePair in propertyThatStarts.StartsWithFrequencyFrom)
            //{
            //    if (keyValuePair.Value > 0 && (keyValuePair.Key.Equals(_basicSyntacticUnitsFactory.GetSyntacticUnitForAny().Property) || keyValuePair.Key.Equals(propertyToStartFrom)))
            //        return true;
            //}
            //return false;
            //return propertyThatStarts.StartsWithFrequencyFrom.Any(
            //    aProperty => aProperty.Value > 0 &&
            //                 (aProperty.Key.Equals(_basicSyntacticUnitsFactory.GetSyntacticUnitForAny().Property) ||
            //                  aProperty.Key.Equals(propertyToStartFrom)));
        }


        public IProperty GetPropertyWithName(string propertyName)
        {
            return Properties.Single(prop => prop.PropertyName == propertyName);
        }
    }
}