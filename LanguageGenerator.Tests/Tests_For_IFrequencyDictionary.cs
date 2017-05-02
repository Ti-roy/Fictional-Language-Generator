using System;
using System.Collections.Generic;
using LanguageGenerator.Core.FrequencyDictionary;
using NUnit.Framework;


namespace LanguageGenerator.Tests
{
    class Tests_Of_IFrequencyDictionary
    {
        [Test]
        public void Does_Add_Throws_The_Exception_On_Negative_Frequency()
        {
            //Arrange
            IFrequencyDictionary<int> aDictionary = new FrequencyDictionary<int>();
            //Act Assert
            Assert.Throws<InvalidOperationException>(() => aDictionary.Add(1, -1), "Negative values are not allowed as frequency.");
            Assert.Throws<InvalidOperationException>(
                () => aDictionary.Add(new KeyValuePair<int, int>(1, -1)),
                "Negative values are not allowed as frequency.");
        }


        [Test]
        public void Does_Add_Throws_The_Exception_On_Same_Key()
        {
            //Arrange
            IFrequencyDictionary<int> aDictionary = new FrequencyDictionary<int>();
            //Act
            aDictionary.Add(1, 1);
            //Act Assert
            Assert.Throws<ArgumentException>(() => aDictionary.Add(1, 1));
            Assert.Throws<ArgumentException>(() => aDictionary.Add(new KeyValuePair<int, int>(1, 1)));
        }


        [Test]
        public void Does_GetRandomElementBasedOnFrequency_Works_With_Few_Elements()
        {
            //Arrange
            IFrequencyDictionary<int> aDictionary = new FrequencyDictionary<int>();
            //Act
            aDictionary.Add(1, 0);
            aDictionary.Add(2, 1);
            aDictionary.Add(3, 199);
            aDictionary.Add(4, 100);
            int aKey = aDictionary.GetRandomElementBasedOnFrequency();
            //Assert
            Assert.That(aKey == 2 || aKey == 3 || aKey == 4);
        }


        [Test]
        public void Does_GetRandomElementBasedOnFrequency_Works_With_One_Element()
        {
            //Arrange
            IFrequencyDictionary<int> aDictionary = new FrequencyDictionary<int>();
            //Act
            aDictionary.Add(1, 1);
            int aKey = aDictionary.GetRandomElementBasedOnFrequency();
            //Assert
            Assert.That(aKey == 1);
        }


        [Test]
        public void Does_GetRandomElementBasedOnFrequency_Throws_Exception_When_Total_Frequency_Less_Than_One()
        {
            //Arrange
            IFrequencyDictionary<int> aDictionary = new FrequencyDictionary<int>();
            //Act
            aDictionary.Add(1, 0);
            aDictionary.Add(2, 0);
            aDictionary.Add(3, 0);
            //Assert
            Assert.Throws<InvalidOperationException>(() => { aDictionary.GetRandomElementBasedOnFrequency(); });
        }


        [Test]
        public void Does_GetRandomElementBasedOnFrequency_Throws_Exception_When_Dictionary_Empty()
        {
            //Arrange
            IFrequencyDictionary<int> aDictionary = new FrequencyDictionary<int>();

            //ActAssert
            Assert.Throws<InvalidOperationException>(() => { aDictionary.GetRandomElementBasedOnFrequency(); });
        }
    }
}