using System;
using System.Collections.Generic;
using LanguageGenerator.Core.FrequencyDictionary;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.SyntacticProperty;
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
            IInformationAgent repo = new InformationAgent(Substitute.For<Random>());
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
            IInformationAgent repo = new InformationAgent(Substitute.For<Random>());
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
            IInformationAgent repo = new InformationAgent(Substitute.For<Random>());
            repo.Properties = new List<IProperty>();
            //Act Assert
            Assert.Throws<InvalidOperationException>(() => { repo.GetPropertyWithName("a name"); });
        }


        [Test]
        public void Does_DoesPropertyCanStartFrom_Returns_False_On_EmptyProperty_Data()
        {
            IInformationAgent repo = new InformationAgent(Substitute.For<Random>());
            IProperty propertyWithCanStartFromCollection = GetPropertyWith_CanStartFrom_Dictionary(new List<IProperty>());
            //Act 
            bool canStart = repo.DoesPropertyCanStartFrom(propertyWithCanStartFromCollection, Substitute.For<IProperty>());
            //Assert
            Assert.That(!canStart);
        }


        private IFrequencyDictionary<IProperty> CanStartFromDictionary(List<IProperty> listToStartFrom)
        {
            IFrequencyDictionary<IProperty> canStartFromDictionary = Substitute.For<IFrequencyDictionary<IProperty>>();
            canStartFromDictionary.Keys.Returns(listToStartFrom);
            return canStartFromDictionary;
        }


        private IProperty GetPropertyWith_CanStartFrom_Dictionary(List<IProperty> listToStartFrom)
        {
            IProperty propertyWithCanStartFromCollection = Substitute.For<IProperty>();
            IFrequencyDictionary<IProperty> canStartFromDictionary = CanStartFromDictionary(listToStartFrom);
            propertyWithCanStartFromCollection.StartsWithFrequencyFrom.Returns(canStartFromDictionary);
            return propertyWithCanStartFromCollection;
        }


        [Test]
        public void Does_DoesPropertyCanStartFrom_Works_If_Property_Includes_aProperty_in_CanStartFrom_Collection()
        {
            IInformationAgent repo = new InformationAgent(Substitute.For<Random>());
            IProperty startFromThisProperty = Substitute.For<IProperty>();
            IProperty propertyWithCanStartFromCollection = GetPropertyWith_CanStartFrom_Dictionary(
                new List<IProperty>() {startFromThisProperty, Substitute.For<IProperty>(), Substitute.For<IProperty>()});
            //Act 
            bool canStart = repo.DoesPropertyCanStartFrom(propertyWithCanStartFromCollection, startFromThisProperty);
            //Assert
            Assert.That(canStart);
        }


        [Test]
        public void Does_DoesPropertyCanStartFrom_Dont_Work_If_Property_Doesnt_Include_aProperty_in_CanStartFrom_Collection()
        {
            IInformationAgent repo = new InformationAgent(Substitute.For<Random>());
            IProperty startFromThisProperty = Substitute.For<IProperty>();
            IProperty propertyWithCanStartFromCollection =
                GetPropertyWith_CanStartFrom_Dictionary(new List<IProperty>() {Substitute.For<IProperty>(), Substitute.For<IProperty>()});
            //Act 
            bool canStart = repo.DoesPropertyCanStartFrom(propertyWithCanStartFromCollection, startFromThisProperty);
            //Assert
            Assert.That(!canStart);
        }
    }
}