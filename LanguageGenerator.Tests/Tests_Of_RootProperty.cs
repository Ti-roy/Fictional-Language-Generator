using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.RootProperty;
using NUnit.Framework;


namespace LanguageGenerator.Tests
{
    class Tests_Of_RootProperty : Tests_Of_Comperison_Of_IProperty
    {
        [Test]
        public void Is_Same_Elements_Equal()
        {
            //Arrange
            IProperty property1 = new RootProperty("Property1");
            IProperty property2 = new RootProperty("Property1");
            //Act
            bool propertiesEqual = IsTwoIPropertyImplementationsEqual(property1, property2);
            //Assert
            Assert.That(propertiesEqual);
        }


        [Test]
        public void Is_Different_Elements_Not_Equal()
        {
            //Arrange
            IProperty property1 = new RootProperty("Property1");
            IProperty property2 = new RootProperty("Property2");
            //Act
            bool propertiesEqual = IsTwoIPropertyImplementationsEqual(property1, property2);
            //Assert
            Assert.That(!propertiesEqual);
        }
    }
}