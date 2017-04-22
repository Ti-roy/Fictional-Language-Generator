using System;
using System.Collections.Generic;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.BasicSyntacticUnits;
using NUnit.Framework;
using NSubstitute;


namespace LanguageGenerator.Tests
{
    [TestFixture]
    class Tests_SyntacticRepository
    {
        IProperty GetSubstituteFor_IPropery_WichReturnsPropertyName(string propertyName)
        {
            IProperty prop1 = Substitute.For<IProperty>();
            prop1.PropertyName.Returns(propertyName);
            return prop1;
        }


        [Test]
        public void Does_GetPropertyWithName_Works_With_One_Property_Data()
        {
            //Arrange
            ISyntacticRepository repo = new SyntacticRepository(Substitute.For<IBasicSyntacticUnitsFactory>(), Substitute.For<Random>());
            repo.Properties = new List<IProperty>();
            string testPropertyName = "TestProperty";
            //Act
            repo.Properties.Add(GetSubstituteFor_IPropery_WichReturnsPropertyName(testPropertyName));
            //Assert
            Assert.That(repo.GetPropertyWithName(testPropertyName).PropertyName == testPropertyName);
        }


        [Test]
        public void Does_GetPropertyWithName_Works_With_Few_Properties_Data()
        {
            //Arrange
            ISyntacticRepository repo = new SyntacticRepository(Substitute.For<IBasicSyntacticUnitsFactory>(), Substitute.For<Random>());
            repo.Properties = new List<IProperty>();
            string testPropertyName1 = "TestProperty1";
            string testPropertyName2 = "TestProperty2";
            //Act
            repo.Properties.Add(GetSubstituteFor_IPropery_WichReturnsPropertyName(testPropertyName1));
            repo.Properties.Add(GetSubstituteFor_IPropery_WichReturnsPropertyName(testPropertyName2));
            //Assert
            Assert.That(repo.GetPropertyWithName(testPropertyName1).PropertyName == testPropertyName1);
        }


        [Test]
        public void Does_GetPropertyWithName_Throws_Exception_When_Nothing_Found()
        {
            //Arrange
            ISyntacticRepository repo = new SyntacticRepository(Substitute.For<IBasicSyntacticUnitsFactory>(), Substitute.For<Random>());
            repo.Properties = new List<IProperty>();
            //Act Assert
            Assert.Throws<InvalidOperationException>(() => { repo.GetPropertyWithName("a name"); });
        }


        [Test]
        public void Does_DoesPropertyCanStartFrom_Returns_False_On_EptyProperty_Data()
        {
            throw new NotImplementedException();
        }
    }
}