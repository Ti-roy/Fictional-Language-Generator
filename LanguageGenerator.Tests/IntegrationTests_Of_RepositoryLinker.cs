using System.Linq;
using LanguageGenerator.Core.AbstractFactory;
using LanguageGenerator.Core.Repository.RepositoryLinker;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;
using NUnit.Framework;


namespace LanguageGenerator.Tests
{
    [TestFixture]
    internal class IntegrationTests_Of_RepositoryLinker
    {
        [Test]
        public void Does_IsRepositoryLinked_Returns_False_On_Not_Empty_ChildLinkInfo()
        {
            //Arrange
            ILanguageFactory languageFactory = new LanguageFactory();
            languageFactory.CreateParentProperty("testProperty");
            languageFactory.CreateParentSyntacticUnit("testProperty", 1).AddPossibleChild("testChildProperty", 1);
            IRepositoryLinker linker = new RepositoryLinker();
            //Act
            bool isRepositoryLinked = linker.IsRepositoryLinked(languageFactory.Repository);
            //Assert
            Assert.That(!isRepositoryLinked);
        }


        [Test]
        public void Does_IsRepositoryLinked_Returns_False_On_Not_Empty_ChildLinkInfo_And_OrderLinkInfo()
        {
            //Arrange
            ILanguageFactory languageFactory = new LanguageFactory();
            languageFactory.CreateParentProperty("testProperty").CanStartFrom("testPropertyToGoAfter");
            languageFactory.CreateParentSyntacticUnit("testProperty", 1).AddPossibleChild("testChildProperty", 1);
            IRepositoryLinker linker = new RepositoryLinker();
            //Act
            bool isRepositoryLinked = linker.IsRepositoryLinked(languageFactory.Repository);
            //Assert
            Assert.That(!isRepositoryLinked);
        }


        [Test]
        public void Does_IsRepositoryLinked_Returns_False_On_Not_Empty_MustContainInfo()
        {
            //Arrange
            ILanguageFactory languageFactory = new LanguageFactory();
            languageFactory.CreateRootProperty("childProperty");
            languageFactory.CreateParentProperty("testProperty").MustContainProperty("aProperty");
            IRepositoryLinker linker = new RepositoryLinker();
            //Act
            bool isRepositoryLinked = linker.IsRepositoryLinked(languageFactory.Repository);
            //Assert
            Assert.That(!isRepositoryLinked);
        }


        [Test]
        public void Does_IsRepositoryLinked_Returns_False_On_Not_Empty_OrderLinkInfo()
        {
            //Arrange
            ILanguageFactory languageFactory = new LanguageFactory();
            languageFactory.CreateParentProperty("testProperty").CanStartFrom("testPropertyToGoAfter");
            IRepositoryLinker linker = new RepositoryLinker();
            //Act
            bool isRepositoryLinked = linker.IsRepositoryLinked(languageFactory.Repository);
            //Assert
            Assert.That(!isRepositoryLinked);
        }


        [Test]
        public void Does_IsRepositoryLinked_Returns_True_On_Empty_ChildLinkInfo_And_OrderLinkInfo()
        {
            //Arrange
            ILanguageFactory languageFactory = new LanguageFactory();
            IRepositoryLinker linker = new RepositoryLinker();
            //Act
            bool isRepositoryLinked = linker.IsRepositoryLinked(languageFactory.Repository);
            //Assert
            Assert.That(isRepositoryLinked);
        }


        [Test]
        public void Does_LinkRepository_Links()
        {
            //Arrange
            string startPropertyName = "testStartProperty";
            string mainTestPropertyName = "testProperty";
            string childPropertyName = "testChildProperty";

            ILanguageFactory languageFactory = new LanguageFactory();
            IRepositoryLinker linker = new RepositoryLinker();

            IProperty startProperty = languageFactory.CreateParentProperty(startPropertyName);
            IParentProperty testProperty = languageFactory.CreateParentProperty(mainTestPropertyName).CanStartFrom(startPropertyName).MustContainProperty(childPropertyName);
            languageFactory.CreateRootProperty(childPropertyName);
            IParentSU parentSu = languageFactory.CreateParentSyntacticUnit(mainTestPropertyName, 1).AddPossibleChild(childPropertyName);
            //Act
            linker.LinkRepository(languageFactory.Repository);
            bool Does_PropertyCanGoAfter_Works = testProperty.FrequencyToStartFromProperty(startProperty)>0;
            bool Does_AddPossibleChild_Works = parentSu.PossibleChildren.Keys.Any(property => property.PropertyName == childPropertyName);
            bool Does_MustContainProperies_Works = testProperty.MustContainProperties.Any(property => property.PropertyName == childPropertyName);
            //Assert
            Assert.That(Does_PropertyCanGoAfter_Works && Does_AddPossibleChild_Works && Does_MustContainProperies_Works);
        }
    }
}