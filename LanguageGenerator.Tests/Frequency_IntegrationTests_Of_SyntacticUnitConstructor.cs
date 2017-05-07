using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.AbstractFactory;
using LanguageGenerator.Core.Constructor;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;
using NUnit.Framework;


namespace LanguageGenerator.Tests
{
    [TestFixture]
    internal class Frequency_IntegrationTests_Of_SyntacticUnitConstructor
    {
        public Frequency_IntegrationTests_Of_SyntacticUnitConstructor()
        {
            _consonants = new List<char> {'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z'};
            _vowels = new List<char> {'a', 'e', 'i', 'o', 'u'};
        }


        private readonly List<char> _consonants;
        private readonly List<char> _vowels;


        private ILanguageFactory CreateRepositoryWithConsonantsAndVowels()
        {
            ILanguageFactory languageFactory = new LanguageFactory();
            List<char> consonants = _consonants;
            List<char> vowels = _vowels;
            languageFactory.CreateRootProperty("consonant");
            foreach (char c in consonants)
            {
                languageFactory.CreateRootSyntacticUnit(c.ToString(), "consonant");
            }
            languageFactory.CreateRootProperty("vowel");
            foreach (char c in vowels)
            {
                languageFactory.CreateRootSyntacticUnit(c.ToString(), "vowel");
            }

            return languageFactory;
        }


        private bool IsValueInRange(int valueThatMustBeInRange, int totalAmount, int frequencyForValue, params int[] otherFrequencies)
        {
            int possibleFrequencyError = totalAmount / 100;
            int indealAmountOfValue = totalAmount / (frequencyForValue + otherFrequencies.Sum()) * frequencyForValue;
            int minAmountOfValue = indealAmountOfValue - possibleFrequencyError;
            int maxAmountOfValue = indealAmountOfValue + possibleFrequencyError;
            return valueThatMustBeInRange < maxAmountOfValue && valueThatMustBeInRange > minAmountOfValue;
        }


        [Test]
        [Retry(5)]
        public void Frequency_In_Order_On_Second_Element_Test()
        {
            //Arrange
            ILanguageFactory languageFactory = CreateRepositoryWithConsonantsAndVowels();
            SyntacticUnitConstructor constructor = new SyntacticUnitConstructor(languageFactory.Repository);
            List<string> words = new List<string>();

            int amountOfSecondLetterAsConsonant = 0;

            int totalAmountOfWords = 1000;
            int frequencyForPropertyToGoAfterSameProperty = 10;
            int frequencyForPropertyToGoAfterAnotherProperty = 90;

            languageFactory.Repository.GetRootPropertyWithName("consonant")
                           .CanStartFrom("Start")
                           .CanStartFrom("consonant", frequencyForPropertyToGoAfterSameProperty)
                           .CanStartFrom("vowel", frequencyForPropertyToGoAfterAnotherProperty);
            languageFactory.Repository.GetRootPropertyWithName("vowel")
                           .CanStartFrom("consonant", frequencyForPropertyToGoAfterAnotherProperty)
                           .CanStartFrom("vowel", frequencyForPropertyToGoAfterSameProperty);
            languageFactory.CreateParentProperty("TwoLetters").CanStartFrom("Start");
            languageFactory.CreateParentSyntacticUnit("TwoLetters").AddChildrenAmount(2, 1).AddPossibleChild("consonant").AddPossibleChild("vowel");
            constructor.LinkRepository();
            //Act
            for (int i = 0; i < totalAmountOfWords; i++)
            {
                string currentResult = constructor.GetResultStringOfProperty("TwoLetters");
                words.Add(currentResult);
                if (_consonants.Contains(currentResult[1]))
                {
                    amountOfSecondLetterAsConsonant++;
                }
            }
            //Assert
            Assert.That(
                IsValueInRange(
                    amountOfSecondLetterAsConsonant, totalAmountOfWords, frequencyForPropertyToGoAfterSameProperty,
                    frequencyForPropertyToGoAfterAnotherProperty));
        }


        [Test]
        [Retry(5)]
        public void Frequency_In_Order_On_Third_Element_Test()
        {
            ILanguageFactory languageFactory = CreateRepositoryWithConsonantsAndVowels();
            SyntacticUnitConstructor constructor = new SyntacticUnitConstructor(languageFactory.Repository);
            List<string> words = new List<string>();

            int amountOfThirdLetterAsConsonant = 0;

            int totalAmountOfWords = 10000;
            int frequencyForPropertyToGoAfterSameProperty = 10;
            int frequencyForPropertyToGoAfterAnotherProperty = 90;

            languageFactory.Repository.GetRootPropertyWithName("consonant")
                           .CanStartFrom("Start")
                           .CanStartFrom("consonant", frequencyForPropertyToGoAfterSameProperty)
                           .CanStartFrom("vowel", frequencyForPropertyToGoAfterAnotherProperty);
            languageFactory.Repository.GetRootPropertyWithName("vowel")
                           .CanStartFrom("consonant", frequencyForPropertyToGoAfterAnotherProperty)
                           .CanStartFrom("vowel", frequencyForPropertyToGoAfterSameProperty);
            languageFactory.CreateParentProperty("ThreeLetters").CanStartFrom("Start");
            languageFactory.CreateParentSyntacticUnit("ThreeLetters").AddChildrenAmount(3, 1).AddPossibleChild("consonant").AddPossibleChild("vowel");
            constructor.LinkRepository();
            //Act
            for (int i = 0; i < totalAmountOfWords; i++)
            {
                string currentResult = constructor.GetResultStringOfProperty("ThreeLetters");
                words.Add(currentResult);
                if (_consonants.Contains(currentResult[2]))
                {
                    amountOfThirdLetterAsConsonant++;
                }
            }
            //Assert
            //This frequencies calculated as markov chains
            int frequencyForThirdLetterToBeConsonant = 82;
            int frequencyForThirdLetterToBeVowel = 18;
            Assert.That(
                IsValueInRange(
                    amountOfThirdLetterAsConsonant, totalAmountOfWords, frequencyForThirdLetterToBeConsonant, frequencyForThirdLetterToBeVowel));
        }


        [Test]
        [Retry(5)]
        public void Frequency_Test_For_Children_Amount()
        {
            //Arrange
            ILanguageFactory languageFactory = CreateRepositoryWithConsonantsAndVowels();
            SyntacticUnitConstructor constructor = new SyntacticUnitConstructor(languageFactory.Repository);
            List<string> words = new List<string>();
            int amountOfWordsWithFiveLetters = 0;
            int totalAmountOfWords = 10000;
            int frequencyForThreeLetters = 100;
            int frequencyForFourLetters = 80;
            int frequencyForFiveLetters = 60;


            languageFactory.Repository.GetRootPropertyWithName("consonant").CanStartFrom("Start").CanStartFrom("consonant").CanStartFrom("vowel");
            languageFactory.Repository.GetRootPropertyWithName("vowel").CanStartFrom("consonant").CanStartFrom("vowel");
            languageFactory.CreateParentProperty("ThreeLetters").CanStartFrom("Start");
            languageFactory.CreateParentSyntacticUnit("ThreeLetters")
                           .AddChildrenAmount(3, frequencyForThreeLetters)
                           .AddChildrenAmount(4, frequencyForFourLetters)
                           .AddChildrenAmount(5, frequencyForFiveLetters)
                           .AddPossibleChild("consonant")
                           .AddPossibleChild("vowel");
            constructor.LinkRepository();


            //Act
            for (int i = 0; i < totalAmountOfWords; i++)
            {
                string currentResult = constructor.GetResultStringOfProperty("ThreeLetters");
                words.Add(currentResult);
                if (currentResult.Length == 5)
                {
                    amountOfWordsWithFiveLetters++;
                }
            }
            //Assert
            Assert.That(
                IsValueInRange(
                    amountOfWordsWithFiveLetters, totalAmountOfWords, frequencyForFiveLetters, frequencyForFourLetters, frequencyForThreeLetters));
        }


        [Test]
        [Retry(5)]
        public void Test_With_Different_Child_Frequency()
        {
            //Arrange
            ILanguageFactory languageFactory = CreateRepositoryWithConsonantsAndVowels();
            SyntacticUnitConstructor constructor = new SyntacticUnitConstructor(languageFactory.Repository);
            List<string> words = new List<string>();
            int totalAmountOfWords = 10000;
            int amountOfConsonants = 0;
            int amountOfVowels = 0;
            int amountOfLettersInWord = 3;

            int consonantFrequency = 100;
            int vowelFrequency = 50;

            languageFactory.Repository.GetRootPropertyWithName("consonant").CanStartFrom("Start").CanStartFrom("consonant").CanStartFrom("vowel");
            languageFactory.Repository.GetRootPropertyWithName("vowel").CanStartFrom("Start").CanStartFrom("consonant").CanStartFrom("vowel");
            languageFactory.CreateParentProperty("ThreeLetters").CanStartFrom("Start");
            languageFactory.CreateParentSyntacticUnit("ThreeLetters")
                           .AddChildrenAmount(amountOfLettersInWord, 1)
                           .AddPossibleChild("consonant", consonantFrequency)
                           .AddPossibleChild("vowel", vowelFrequency);
            constructor.LinkRepository();
            //Act
            for (int i = 0; i < totalAmountOfWords; i++)
            {
                string currentResult = constructor.GetResultStringOfProperty("ThreeLetters");
                words.Add(currentResult);
                foreach (char c in currentResult)
                {
                    if (_vowels.Contains(c))
                    {
                        amountOfVowels++;
                    }
                    else
                    {
                        amountOfConsonants++;
                    }
                }
            }
            //Assert
            Assert.That(IsValueInRange(amountOfVowels, totalAmountOfWords * amountOfLettersInWord, vowelFrequency, consonantFrequency));
        }


        [Test]
        [Retry(5)]
        public void Test_With_Same_Child_Frequency()
        {
            //Arrange
            ILanguageFactory languageFactory = CreateRepositoryWithConsonantsAndVowels();
            SyntacticUnitConstructor constructor = new SyntacticUnitConstructor(languageFactory.Repository);
            List<string> words = new List<string>();
            int totalAmountOfWords = 10000;
            int amountOfVowels = 0;
            int amountOfLettersInWord = 3;

            languageFactory.Repository.GetRootPropertyWithName("consonant").CanStartFrom("Start").CanStartFrom("consonant").CanStartFrom("vowel");
            languageFactory.Repository.GetRootPropertyWithName("vowel").CanStartFrom("Start").CanStartFrom("consonant").CanStartFrom("vowel");
            languageFactory.CreateParentProperty("ThreeLetters").CanStartFrom("Start");
            languageFactory.CreateParentSyntacticUnit("ThreeLetters")
                           .AddChildrenAmount(amountOfLettersInWord, 1)
                           .AddPossibleChild("consonant")
                           .AddPossibleChild("vowel");
            constructor.LinkRepository();
            //Act
            for (int i = 0; i < totalAmountOfWords; i++)
            {
                string currentResult = constructor.GetResultStringOfProperty("ThreeLetters");
                words.Add(currentResult);
                foreach (char c in currentResult)
                {
                    if (_vowels.Contains(c))
                    {
                        amountOfVowels++;
                    }
                }
            }
            //Assert
            int defaultFrequency = 100;
            Assert.That(IsValueInRange(amountOfVowels, totalAmountOfWords * amountOfLettersInWord, defaultFrequency, defaultFrequency));
        }
    }
}