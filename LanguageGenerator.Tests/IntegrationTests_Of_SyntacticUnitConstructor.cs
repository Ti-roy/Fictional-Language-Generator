using System;
using LanguageGenerator.Core.AbstractFactory;
using LanguageGenerator.Core.Constructor;
using LanguageGenerator.Core.InformationAgent;
using LanguageGenerator.Core.Repository;
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
        private void CreateParentPropertySyntacticUnitParent(
            ILanguageFactory languageFactory, string propertyName, string propertyCanGoAfter, params string[] possibleChildren)
        {
            languageFactory.CreateParentProperty(propertyName).PropertyCanGoAfter(propertyCanGoAfter);
            IParentSU parentSU = languageFactory.CreateParentSyntacticUnit(propertyName, 1);
            foreach (string possibleChild in possibleChildren)
            {
                parentSU.AddPossibleChild(possibleChild, 1);
            }
            parentSU.AddChildrenAmount(possibleChildren.Length);
        }


        private void CreateRootPropertySyntacticUnitPair(
            ILanguageFactory languageFactory, string propertyName, string propertyCanGoAfter, string rootRepresentation)
        {
            languageFactory.CreateRootProperty(propertyName).PropertyCanGoAfter(propertyCanGoAfter, 1);
            languageFactory.CreateRootSyntacticUnit(rootRepresentation, propertyName, 1);
        }


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


        [Test]
        public void Does_MustContain_Works_With_Nessecery_Value_At_The_End()
        {
            //Arrange
            ILanguageFactory languageFactory = new LanguageFactory();
            //Act
            CreateRootPropertySyntacticUnitPair(languageFactory, "child1", "Start", "a");
            CreateRootPropertySyntacticUnitPair(languageFactory, "child2", "child1", "b");
            CreateRootPropertySyntacticUnitPair(languageFactory, "child3", "child2", "c");
            CreateRootPropertySyntacticUnitPair(languageFactory, "child4", "child2", "d");

            languageFactory.CreateParentProperty("TopParent").PropertyCanGoAfter("Start").MustContainProperty("child3");
            languageFactory.CreateParentSyntacticUnit("TopParent")
                           .AddChildrenAmount(3, 1)
                           .AddPossibleChild("child1")
                           .AddPossibleChild("child2")
                           .AddPossibleChild("child3");
            
            string result = new SyntacticUnitConstructor(languageFactory.Repository).GetStringOfProperty("TopParent");
            //Assert
            Assert.That(result[2] == 'c');
        }


        [Test]
        public void Does_MustContain_Works_With_Nessecery_Value_At_The_Start()
        {
            //Arrange
            ILanguageFactory languageFactory = new LanguageFactory();
            //Act
            CreateRootPropertySyntacticUnitPair(languageFactory, "child1", "Start", "a");
            CreateRootPropertySyntacticUnitPair(languageFactory, "child2", "Any", "b");
            CreateRootPropertySyntacticUnitPair(languageFactory, "child3", "Any", "c");
            CreateRootPropertySyntacticUnitPair(languageFactory, "child4", "Any", "d");

            languageFactory.CreateParentProperty("TopParent").PropertyCanGoAfter("Start").MustContainProperty("child1");
            languageFactory.CreateParentSyntacticUnit("TopParent")
                           .AddChildrenAmount(4, 1)
                           .AddPossibleChild("child1")
                           .AddPossibleChild("child2")
                           .AddPossibleChild("child3");

            string result = new SyntacticUnitConstructor(languageFactory.Repository).GetStringOfProperty("TopParent");
            //Assert
            Assert.That(result[0] == 'a');
        }
        [Test]
        public void Does_MustContain_Works_With_Only_Nessecery_Value()
        {
            //Arrange
            ILanguageFactory languageFactory = new LanguageFactory();
            //Act
            CreateRootPropertySyntacticUnitPair(languageFactory, "child1", "Any", "a");
            CreateRootPropertySyntacticUnitPair(languageFactory, "child2", "Any", "b");
            CreateRootPropertySyntacticUnitPair(languageFactory, "child3", "Any", "c");
            CreateRootPropertySyntacticUnitPair(languageFactory, "child4", "Any", "d");

            languageFactory.CreateParentProperty("TopParent").PropertyCanGoAfter("Start").MustContainProperty("child1");
            languageFactory.CreateParentSyntacticUnit("TopParent")
                           .AddChildrenAmount(2, 1)
                           .AddPossibleChild("child1")
                           .AddPossibleChild("child2")
                           .AddPossibleChild("child3");

            string result = new SyntacticUnitConstructor(languageFactory.Repository).GetStringOfProperty("TopParent");
            //Assert
            Assert.That(result[0] == 'a');
        }


        //                  3_0
        //              /       \
        //           2_0        2_1 ---
        //         /      \       \    \
        //        1_0     1_1     0_5  1_2
        //        | \     | \           |
        //  0_0  0_1 0_2 0_3 0_4       0_6
        [Test]
        public void Does_Root_SU_On_Different_Levels_Constracted_And_Start_From_Different_Levels()
        {
            //Arrange
            ILanguageFactory languageFactory = new LanguageFactory();

            CreateRootPropertySyntacticUnitPair(languageFactory, "0_1", "Start", "a");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_2", "0_1", "b");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_3", "1_0", "c");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_4", "0_3", "d");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_5", "2_0", "f");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_6", "0_5", "k");

            CreateParentPropertySyntacticUnitParent(languageFactory, "1_0", "Start", "0_1", "0_2");
            CreateParentPropertySyntacticUnitParent(languageFactory, "1_1", "1_0", "0_3", "0_4");
            CreateParentPropertySyntacticUnitParent(languageFactory, "1_2", "0_5", "0_6");
            CreateParentPropertySyntacticUnitParent(languageFactory, "2_0", "Start", "1_0", "1_1");
            CreateParentPropertySyntacticUnitParent(languageFactory, "2_1", "2_0", "0_5", "1_2");
            CreateParentPropertySyntacticUnitParent(languageFactory, "3_0", "Start", "2_0", "2_1");

            //Act
            string result = new SyntacticUnitConstructor(languageFactory.Repository).GetStringOfProperty("3_0");

            //Assert
            Assert.That(result == "abcdfk");
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

            CreateRootPropertySyntacticUnitPair(languageFactory, "0_1", "Start", "a");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_2", "0_1", "b");

            CreateParentPropertySyntacticUnitParent(languageFactory, "1_0", "Start", "0_1");
            CreateParentPropertySyntacticUnitParent(languageFactory, "1_1", "1_0", "0_2");
            CreateParentPropertySyntacticUnitParent(languageFactory, "2_0", "Start", "1_0", "1_1");

            ISyntacticUnitConstructor suConstructor = new SyntacticUnitConstructor(languageFactory.Repository);
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


        //                  4_0
        //                /    | 
        //              0_1   3_0
        //                    |   \
        //                   0_2  2_0
        //                        |  \
        //                       0_3 1_0
        //                            | \
        //         0_0               0_4 0_5   
        [Test]
        public void Test_Binary_Tree_Structure_With_Left_Child_As_Root()
        {
            //Arrange
            ILanguageFactory languageFactory = new LanguageFactory();

            CreateRootPropertySyntacticUnitPair(languageFactory, "0_1", "Start", "a");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_2", "0_1", "b");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_3", "0_2", "c");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_4", "0_3", "d");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_5", "0_4", "f");

            CreateParentPropertySyntacticUnitParent(languageFactory, "1_0", "0_3", "0_4", "0_5");
            CreateParentPropertySyntacticUnitParent(languageFactory, "2_0", "0_2", "1_0", "0_3");
            CreateParentPropertySyntacticUnitParent(languageFactory, "3_0", "0_1", "2_0", "0_2");
            CreateParentPropertySyntacticUnitParent(languageFactory, "4_0", "Start", "3_0", "0_1");
            //Act
            string result = new SyntacticUnitConstructor(languageFactory.Repository).GetStringOfProperty("4_0");
            //Assert
            Assert.That(result == "abcdf");
        }


        //                  4_0
        //                /    | 
        //              3_0   0_5
        //            /   |
        //          2_0  0_4
        //         /  |
        //       1_0 0_3
        //       / |
        // 0_0 0_1 0_2   
        [Test]
        public void Test_Binary_Tree_Structure_With_Right_Child_As_Root()
        {
            //Arrange
            ILanguageFactory languageFactory = new LanguageFactory();

            CreateRootPropertySyntacticUnitPair(languageFactory, "0_1", "Start", "a");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_2", "0_1", "b");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_3", "1_0", "c");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_4", "2_0", "d");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_5", "3_0", "f");

            CreateParentPropertySyntacticUnitParent(languageFactory, "1_0", "Start", "0_1", "0_2");
            CreateParentPropertySyntacticUnitParent(languageFactory, "2_0", "Start", "1_0", "0_3");
            CreateParentPropertySyntacticUnitParent(languageFactory, "3_0", "Start", "2_0", "0_4");
            CreateParentPropertySyntacticUnitParent(languageFactory, "4_0", "Start", "3_0", "0_5");
            //Act
            string result = new SyntacticUnitConstructor(languageFactory.Repository).GetStringOfProperty("4_0");
            //Assert
            Assert.That(result == "abcdf");
        }


        //                        4_0
        //                /        |      \
        //              3_0       3_1     3_2
        //            /   |     /    \     |  \
        //          2_0  0_4  2_1    2_2  0_9 2_3
        //         /  |       / \    /  \      |   \
        //       1_0 0_3    0_5 0_6 0_7 0_8   0_10 1_1 
        //       / |                                |  \
        // 0_0 0_1 0_2                            0_11 0_12


        [Test]
        public void Test_Complex_Tree()
        {
            //Arrange
            ILanguageFactory languageFactory = new LanguageFactory();

            CreateRootPropertySyntacticUnitPair(languageFactory, "0_1", "Start", "q");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_2", "0_1", "w");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_3", "1_0", "e");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_4", "2_0", "r");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_5", "3_0", "t");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_6", "0_5", "y");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_7", "0_6", "u");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_8", "0_7", "i");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_9", "0_8", "o");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_10", "0_9", "p");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_11", "0_10", "a");
            CreateRootPropertySyntacticUnitPair(languageFactory, "0_12", "0_11", "s");


            CreateParentPropertySyntacticUnitParent(languageFactory, "1_0", "Start", "0_1", "0_2");
            CreateParentPropertySyntacticUnitParent(languageFactory, "1_1", "0_10", "0_11", "0_12");

            CreateParentPropertySyntacticUnitParent(languageFactory, "2_0", "Start", "1_0", "0_3");
            CreateParentPropertySyntacticUnitParent(languageFactory, "2_1", "3_0", "0_5", "0_6");
            CreateParentPropertySyntacticUnitParent(languageFactory, "2_2", "2_1", "0_7", "0_8");
            CreateParentPropertySyntacticUnitParent(languageFactory, "2_3", "0_9", "0_10", "1_1");

            CreateParentPropertySyntacticUnitParent(languageFactory, "3_0", "Start", "2_0", "0_4");
            CreateParentPropertySyntacticUnitParent(languageFactory, "3_1", "3_0", "2_1", "2_2");
            CreateParentPropertySyntacticUnitParent(languageFactory, "3_2", "3_1", "0_9", "2_3");


            CreateParentPropertySyntacticUnitParent(languageFactory, "4_0", "Start", "3_0", "3_1", "3_2");
            //Act
            string result = new SyntacticUnitConstructor(languageFactory.Repository).GetStringOfProperty("4_0");
            //Assert
            Assert.That(result == "qwertyuiopas");
        }
    }
}