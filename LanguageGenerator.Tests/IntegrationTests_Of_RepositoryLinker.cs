using System.Linq;
using LanguageGenerator.Core.AbstractFactory;
using LanguageGenerator.Core.Repository.RepositoryLinker;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit;
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
            languageFactory.CreateParentProperty("testProperty").PropertyCanGoAfter("testPropertyToGoAfter");
            languageFactory.CreateParentSyntacticUnit("testProperty", 1).AddPossibleChild("testChildProperty", 1);
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
            languageFactory.CreateParentProperty("testProperty").PropertyCanGoAfter("testPropertyToGoAfter");
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
            ILanguageFactory languageFactory = new LanguageFactory();
            IRepositoryLinker linker = new RepositoryLinker();
            IProperty startProperty = languageFactory.CreateParentProperty("testStartProperty");
            IProperty testProperty = languageFactory.CreateParentProperty("testProperty").PropertyCanGoAfter("testStartProperty");
            languageFactory.CreateRootProperty("testChildProperty");
            IParentSU parentSu = languageFactory.CreateParentSyntacticUnit("testProperty", 1).AddPossibleChild("testChildProperty");
            //Act
            linker.LinkRepository(languageFactory.Repository);
            bool Does_PropertyCanGoAfter_Works = testProperty.CanStartFrom(startProperty);
            bool Does_AddPossibleChild_Works = parentSu.PossibleChildren.Keys.Any(property => property.PropertyName == "testChildProperty");
            //Assert
            Assert.That(Does_PropertyCanGoAfter_Works && Does_AddPossibleChild_Works);
        }
    }
}