using System;
using LanguageGenerator.Core.InformationAgent;
using LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits;


namespace LanguageGenerator.Core.AbstractFactory
{
    public interface IAbstractFactory
    {
        IInformationAgent CreateSyntacticRepository();
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


        public IInformationAgent CreateSyntacticRepository()
        {
            return new InformationAgent.InformationAgent(_random, _basicSyntacticUnitsFactory);
        }
    }
}