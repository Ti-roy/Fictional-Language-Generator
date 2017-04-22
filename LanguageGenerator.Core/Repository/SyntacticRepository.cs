using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits;
using LanguageGenerator.Core.SyntacticUnit.RootSU;


namespace LanguageGenerator.Core.Repository
{
    public class SyntacticRepository : ISyntacticRepository
    {
        public IList<IProperty> Properties { get; set; }
        public IList<ISyntacticUnit> SyntacticUnits { get; set; }
        private readonly Random _random;
        private IBasicSyntacticUnitsFactory _basicSyntacticUnitsFactory;


        public SyntacticRepository(
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


        public SyntacticRepository(Random random, IBasicSyntacticUnitsFactory basicSyntacticUnitsFactory) : this(
            random,
            basicSyntacticUnitsFactory,
            new List<IProperty>(),
            new List<ISyntacticUnit>())
        {
        }


        public ISyntacticUnit GetRandomSyntacticUnitsOfProperty(IProperty property)
        {
            IRootProperty rootProperty = property as IRootProperty;
            if (rootProperty != null)
            {
                return ReturnSyntacticPropertyBasedOnProbabilytieScale(rootProperty.RootSyntacticUnits);
            }
            IParentProperty parentProperty = property as IParentProperty;
            if (parentProperty != null)
            {
                return ReturnSyntacticPropertyBasedOnProbabilytieScale(parentProperty.ParentSyntacticUnits);
            }
            throw new InvalidOperationException("Trying to get syntactic property of uknown property type.");
        }


        public bool DoesPropertyCanStartFrom(IProperty propertyThatStarts, IProperty propertyToStartFrom)
        {
            return propertyThatStarts.StartsWithFrequencyFrom.Keys.Any(
                aProperty => aProperty == _basicSyntacticUnitsFactory.GetSyntacticUnitForAny() ||
                             aProperty == propertyToStartFrom);
        }


        private ISyntacticUnit ReturnSyntacticPropertyBasedOnProbabilytieScale(IList<IParentSU> syntacticUnits)
        {
            return GetRandomSyntacticUnitBasedOnProbability(syntacticUnits.ToList<ISyntacticUnit>());
        }


        private ISyntacticUnit ReturnSyntacticPropertyBasedOnProbabilytieScale(IList<IRootSU> syntacticUnits)
        {
            return GetRandomSyntacticUnitBasedOnProbability(syntacticUnits.ToList<ISyntacticUnit>());
        }


        private ISyntacticUnit GetRandomSyntacticUnitBasedOnProbability(IList<ISyntacticUnit> syntacticUnits)
        {
            int totalFrequencyOfAllSyntacticUnits = 0;
            totalFrequencyOfAllSyntacticUnits = CalculateTotalFrequencyOfAllSyntacticUnits(syntacticUnits, totalFrequencyOfAllSyntacticUnits);
            int randomNumberInRangeOfTotal = _random.Next(1, totalFrequencyOfAllSyntacticUnits);
            return GetSyntacticUnitWithNumberInTotalRange(syntacticUnits, randomNumberInRangeOfTotal);
        }


        private static ISyntacticUnit GetSyntacticUnitWithNumberInTotalRange(IList<ISyntacticUnit> syntacticUnits, int randomNumberInRangeOfTotal)
        {
            int index = -1;
            for (; randomNumberInRangeOfTotal > 0;)
            {
                index++;
                randomNumberInRangeOfTotal -= syntacticUnits[index].Frequency;
            }
            return syntacticUnits[index];
        }


        private static int CalculateTotalFrequencyOfAllSyntacticUnits(IList<ISyntacticUnit> syntacticUnits, int total)
        {
            foreach (ISyntacticUnit syntacticUnit in syntacticUnits)
            {
                total += syntacticUnit.Frequency;
            }
            if (total < 1)
            {
                throw new ArgumentOutOfRangeException("Total frequency of all elements less then 1.");
            }
            return total;
        }


        public IProperty GetPropertyWithName(string propertyName)
        {
            return Properties.Single(prop => prop.PropertyName == propertyName);
        }
    }
}