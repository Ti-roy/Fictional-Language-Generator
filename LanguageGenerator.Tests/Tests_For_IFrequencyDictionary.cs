using System;
using System.Collections.Generic;
using LanguageGenerator.Core.FrequencyDictionary;
using NUnit.Framework;

namespace LanguageGenerator.Tests
{
    class Tests_For_IFrequencyDictionary
    {
        [Test]
        public void Does_Add_Throws_The_Exception_On_Negative_Frequency()
        {
            //Arrage
            IFrequencyDictionary<int> aDictionary = new FrequencyDictionary<int>();
            //Act Assert
            Assert.Throws<InvalidOperationException>(() => aDictionary.Add(1,-1), "Negative values are not allowed as frequency.");
            Assert.Throws<InvalidOperationException>(() => aDictionary.Add(new KeyValuePair<int, int>( 1, -1)), "Negative values are not allowed as frequency.");
        }
    }
}
