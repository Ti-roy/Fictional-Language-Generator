using System;
using System.Collections.Generic;
using LanguageGenerator.Core.InformationAgent;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SyntacticProperty;
using NUnit.Framework;
using NSubstitute;


namespace LanguageGenerator.Tests
{
    [TestFixture]
    class Tests_Of_SyntacticRepository
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
            ISyntacticUnitRepository repo = new SyntacticUnitRepository();
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
            ISyntacticUnitRepository repo = new SyntacticUnitRepository();
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
            ISyntacticUnitRepository repo = new SyntacticUnitRepository();
            repo.Properties = new List<IProperty>();
            //Act Assert
            Assert.Throws<InvalidOperationException>(() => { repo.GetPropertyWithName("a name"); });
        }
    }
}