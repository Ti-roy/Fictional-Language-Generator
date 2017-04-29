using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageGenerator.Core.Constructor;
using LanguageGenerator.Core.SyntacticUnit;
using NSubstitute;
using NUnit.Framework;


namespace LanguageGenerator.Tests
{
    [TestFixture]
    class Tests_Of_SyntacticUnitsResult
    {
        [Test]
        public void Does_GetAllParentResults_Returns_Correct_Data()
        {
            //Arrange
            ISyntacticUnitResult resultParentTop = new SyntacticUnitResult(Substitute.For<ISyntacticUnit>());
            ISyntacticUnitResult resultParentMiddle = new SyntacticUnitResult(Substitute.For<ISyntacticUnit>(),resultParentTop);
            ISyntacticUnitResult resultChild = new SyntacticUnitResult(Substitute.For<ISyntacticUnit>(),resultParentMiddle);
            //Act
            ISyntacticUnitResult[] parents = resultChild.GetAllParentResults().ToArray();
            //Assert
            Assert.That(parents.Contains(resultParentTop)&&parents.Contains(resultParentMiddle) && parents.Length == 2); 
        }
    }
}
