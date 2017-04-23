using System;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits;


namespace LanguageGenerator.Core.AbstractFactory
{
    public interface IAbstractFactory
    {
        ISyntacticRepository CreateSyntacticRepository();
    }


    public class ConcreteFactory : IAbstractFactory
    {
        private IBasicSyntacticUnitsFactory _basicSyntacticUnitsFactory;
        Random _random;

        public ConcreteFactory()
        {
            _random = new Random();
            _basicSyntacticUnitsFactory = new BasicSyntacticUnitsFactory();
        }


        public ISyntacticRepository CreateSyntacticRepository()
        {
            return new SyntacticRepository(_random, _basicSyntacticUnitsFactory);
        }
    }
}