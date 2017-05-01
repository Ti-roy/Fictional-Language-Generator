using System;
using LanguageGenerator.Core.AbstractFactory;
using LanguageGenerator.Core.Constructor;
using LanguageGenerator.Core.InformationAgent;
using LanguageGenerator.Core.Repository;
using LanguageGenerator.Core.Repository.RepositoryLinker;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticProperty.RootProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;
using LanguageGenerator.Core.SyntacticUnit.RootSU;
using NUnit.Framework;


namespace LanguageGenerator.Tests
{
    [TestFixture]
    internal class IntegrationTests_Of_SyntacticUnitConstructor
    {
        [Test]
        public void Does_GetResultSchemeOfProperty_Throws_Exception_When_Asked_Propety_Cant_Start_From_StartOfConstruction()
        {
            //Arrange
            ISyntacticUnitRepository repository = new SyntacticUnitRepository();
            string testRootPropertyName = "testRootProperty";
            IRootProperty rootProperty = new RootProperty(testRootPropertyName);
            repository.Properties.Add(rootProperty);
            repository.SyntacticUnits.Add(new RootSU("a", 1, rootProperty));
            ISyntacticUnitConstructor suConstructor = new SyntacticUnitConstructor(repository);
            //Act Assert
            Assert.Throws<InvalidOperationException>(() => { suConstructor.GetStringOfProperty(testRootPropertyName); });
        }


        [Test]
        public void Does_GetResultSchemeOfProperty_Works_With_One_Parent_Property()
        {
            //Arrange
            ISyntacticUnitRepository repository = new SyntacticUnitRepository();
            string testRootPropertyName = "testRootProperty";

            IRootProperty rootProperty = new RootProperty(testRootPropertyName);
            repository.Properties.Add(rootProperty);
            rootProperty.StartsWithFrequencyFrom.Add(BasicSyntacticUnitsSingleton.StartOfConstructionProperty, 1);
            repository.SyntacticUnits.Add(new RootSU("a", 1, rootProperty));

            IParentProperty parentProperty = new ParentProperty("testParentProperty");
            parentProperty.StartsWithFrequencyFrom.Add(BasicSyntacticUnitsSingleton.StartOfConstructionProperty, 1);
            repository.Properties.Add(parentProperty);

            IParentSU parentSu = new ParentSU(1, parentProperty);
            parentSu.ChildrenAmount.Add(1, 1);
            parentSu.PossibleChildren.Add(rootProperty, 1);
            repository.SyntacticUnits.Add(parentSu);
            ISyntacticUnitConstructor suConstructor = new SyntacticUnitConstructor(repository);
            //Act 
            string result = suConstructor.GetStringOfProperty(parentProperty);
            //Assert
            Assert.That(result == "a");
        }


        [Test]
        public void Does_GetResultSchemeOfProperty_Works_With_One_Root_Property()
        {
            //Arrange
            ISyntacticUnitRepository repository = new SyntacticUnitRepository();
            string testRootPropertyName = "testRootProperty";
            IRootProperty rootProperty = new RootProperty(testRootPropertyName);
            rootProperty.StartsWithFrequencyFrom.Add(BasicSyntacticUnitsSingleton.StartOfConstructionProperty, 1);
            repository.Properties.Add(rootProperty);
            repository.SyntacticUnits.Add(new RootSU("a", 1, rootProperty));
            ISyntacticUnitConstructor suConstructor = new SyntacticUnitConstructor(repository);
            //Act 
            string result = suConstructor.GetStringOfProperty(testRootPropertyName);
            //Assert
            Assert.That(result == "a");
        }


        //    2_0
        //     | \ 
        //    1_0 1_1
        //     |   |
        //0_0 0_1 0_2


        [Test]
        public void Simple_Parent_Property_Order_Test()
        {
            //Arrange

            ILanguageFactory languageFactory = new LanguageFactory();

            languageFactory.CreateRootProperty("0_1").PropertyCanGoAfter("StartOfConstruction", 1);
            languageFactory.CreateRootSyntacticUnit("a", "0_1", 1);

            languageFactory.CreateRootProperty("0_2").PropertyCanGoAfter("0_1", 1);
            languageFactory.CreateRootSyntacticUnit("b", "0_2", 1);


            languageFactory.CreateParentProperty("1_0").PropertyCanGoAfter("StartOfConstruction", 1);
            languageFactory.CreateParentSyntacticUnit("1_0", 1).AddChildrenAmount(1, 1).AddPossibleChild("0_1", 1);


            languageFactory.CreateParentProperty("1_1").PropertyCanGoAfter("1_0", 1);
            languageFactory.CreateParentSyntacticUnit("1_1", 1).AddChildrenAmount(1, 1).AddPossibleChild("0_2");


            languageFactory.CreateParentProperty("2_0").PropertyCanGoAfter("StartOfConstruction", 1);
            languageFactory.CreateParentSyntacticUnit("2_0", 1).AddChildrenAmount(2, 1).AddPossibleChild("1_1", 1).AddPossibleChild("1_0", 1);

            ISyntacticUnitConstructor suConstructor = new SyntacticUnitConstructor(languageFactory.Repository , new RepositoryLinker());
            //Act 
            string result = suConstructor.GetStringOfProperty("2_0");
            //Assert
            Assert.That(result == "ab");
        }


        //       2_0
        //     /      \ 
        //    1_0     1_1
        //     | \     | \
        //0_0 0_1 0_2 0_3 0_4
        [Test]
        public void Simple_Parent_Property_Order_Test_With_Multiply_Children()
        {
            //Arrange
            ISyntacticUnitRepository repository = new SyntacticUnitRepository();

            IRootProperty rootProperty1 = new RootProperty("0_1");
            repository.Properties.Add(rootProperty1);
            rootProperty1.StartsWithFrequencyFrom.Add(BasicSyntacticUnitsSingleton.StartOfConstructionProperty, 1);
            repository.SyntacticUnits.Add(new RootSU("a", 1, rootProperty1));

            IRootProperty rootProperty2 = new RootProperty("0_2");
            repository.Properties.Add(rootProperty2);
            rootProperty2.StartsWithFrequencyFrom.Add(rootProperty1, 1);
            repository.SyntacticUnits.Add(new RootSU("b", 1, rootProperty2));

            IRootProperty rootProperty3 = new RootProperty("0_3");
            repository.Properties.Add(rootProperty3);
            rootProperty3.StartsWithFrequencyFrom.Add(rootProperty2, 1);
            repository.SyntacticUnits.Add(new RootSU("c", 1, rootProperty3));

            IRootProperty rootProperty4 = new RootProperty("0_4");
            repository.Properties.Add(rootProperty4);
            rootProperty4.StartsWithFrequencyFrom.Add(rootProperty3, 1);
            repository.SyntacticUnits.Add(new RootSU("d", 1, rootProperty4));

            IParentProperty parentProperty1 = new ParentProperty("1_0");
            parentProperty1.StartsWithFrequencyFrom.Add(BasicSyntacticUnitsSingleton.StartOfConstructionProperty, 1);
            repository.Properties.Add(parentProperty1);

            IParentProperty parentProperty2 = new ParentProperty("1_1");
            parentProperty2.StartsWithFrequencyFrom.Add(parentProperty1, 1);
            repository.Properties.Add(parentProperty2);

            IParentSU parentSu1 = new ParentSU(1, parentProperty1);
            parentSu1.ChildrenAmount.Add(2, 1);
            parentSu1.PossibleChildren.Add(rootProperty1, 1);
            parentSu1.PossibleChildren.Add(rootProperty2, 1);
            repository.SyntacticUnits.Add(parentSu1);

            IParentSU parentSu2 = new ParentSU(1, parentProperty2);
            parentSu2.ChildrenAmount.Add(2, 1);
            parentSu2.PossibleChildren.Add(rootProperty3, 1);
            parentSu2.PossibleChildren.Add(rootProperty4, 1);
            repository.SyntacticUnits.Add(parentSu2);

            IParentProperty parentProperty3 = new ParentProperty("2_0");
            parentProperty3.StartsWithFrequencyFrom.Add(BasicSyntacticUnitsSingleton.StartOfConstructionProperty, 1);
            repository.Properties.Add(parentProperty3);

            IParentSU parentSu3 = new ParentSU(1, parentProperty3);
            parentSu3.ChildrenAmount.Add(2, 1);
            parentSu3.PossibleChildren.Add(parentProperty1, 1);
            parentSu3.PossibleChildren.Add(parentProperty2, 1);
            repository.SyntacticUnits.Add(parentSu3);

            ISyntacticUnitConstructor suConstructor = new SyntacticUnitConstructor(repository);
            //Act 
            string result = suConstructor.GetStringOfProperty(parentProperty3);
            //Assert
            Assert.That(result == "abcd");
        }


        [Test]
        public void Simple_Root_Property_Order_Test()
        {
            //Arrange
            ISyntacticUnitRepository repository = new SyntacticUnitRepository();

            IRootProperty rootProperty1 = new RootProperty("testRootProperty1");
            repository.Properties.Add(rootProperty1);
            rootProperty1.StartsWithFrequencyFrom.Add(BasicSyntacticUnitsSingleton.StartOfConstructionProperty, 1);
            repository.SyntacticUnits.Add(new RootSU("a", 1, rootProperty1));

            IRootProperty rootProperty2 = new RootProperty("testRootProperty2");
            repository.Properties.Add(rootProperty2);
            rootProperty2.StartsWithFrequencyFrom.Add(rootProperty1, 1);
            repository.SyntacticUnits.Add(new RootSU("b", 1, rootProperty2));

            IParentProperty parentProperty = new ParentProperty("testParentProperty");
            parentProperty.StartsWithFrequencyFrom.Add(BasicSyntacticUnitsSingleton.StartOfConstructionProperty, 1);
            repository.Properties.Add(parentProperty);

            IParentSU parentSu = new ParentSU(1, parentProperty);
            parentSu.ChildrenAmount.Add(2, 1);
            parentSu.PossibleChildren.Add(rootProperty1, 1);
            parentSu.PossibleChildren.Add(rootProperty2, 1);
            repository.SyntacticUnits.Add(parentSu);
            ISyntacticUnitConstructor suConstructor = new SyntacticUnitConstructor(repository);
            //Act 
            string result = suConstructor.GetStringOfProperty(parentProperty);
            //Assert
            Assert.That(result == "ab");
        }
    }
}