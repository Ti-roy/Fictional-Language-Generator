using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LanguageGenerator.Core.AbstractFactory;
using LanguageGenerator.Core.Constructor;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;
using NUnit.Framework;


namespace LanguageGenerator.Tests
{
    [TestFixture]
    public class Load_Tests
    {
        public Load_Tests()
        {
            _consonants = new List<char> {'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z'};
            _vowels = new List<char> {'a', 'e', 'i', 'o', 'u'};
        }


        private readonly List<char> _consonants;
        private readonly List<char> _vowels;

        private int amountOfWords = 10000;

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


        private SyntacticUnitConstructor CreateConstructorWithDefaultScheme()
        {
            ILanguageFactory languageFactory = CreateRepositoryWithConsonantsAndVowels();
            SyntacticUnitConstructor constructor = new SyntacticUnitConstructor(languageFactory.Repository);
            SetUpDefaultScheme(languageFactory, constructor);
            return constructor;
        }


        private static void SetUpDefaultScheme(ILanguageFactory languageFactory, SyntacticUnitConstructor constructor)
        {
            languageFactory.Repository.GetRootPropertyWithName("consonant").CanStartFrom("Start").CanStartFrom("consonant").CanStartFrom("vowel");
            languageFactory.Repository.GetRootPropertyWithName("vowel").CanStartFrom("Start").CanStartFrom("consonant").CanStartFrom("vowel");
            languageFactory.CreateParentProperty("ThreeLetters").CanStartFrom("Start");
            languageFactory.CreateParentSyntacticUnit("ThreeLetters").AddChildrenAmount(3, 1).AddPossibleChild("consonant").AddPossibleChild("vowel");
            constructor.LinkRepository();
        }


        [Test]
        public void Parralell_Attempt()
        {
            SyntacticUnitConstructor constructor = CreateConstructorWithDefaultScheme();
            List<string> words = new List<string>();
            int totalAmountOfWords = amountOfWords;
            //Act
            Stopwatch sw = new Stopwatch();

            sw.Start();

            int degreeOfParallelism = Environment.ProcessorCount;
            Task[] tasks = new Task[degreeOfParallelism];
            int amountOfWordsOnOneThread = totalAmountOfWords / degreeOfParallelism;
            for (int taskNumber = 0; taskNumber < degreeOfParallelism; taskNumber++)
            {
                tasks[taskNumber] = Task.Factory.StartNew(
                    () =>
                    {
                        for (int i = 0; i < amountOfWordsOnOneThread;
                             i++)
                        {
                            string currentResult = constructor.GetResultStringOfProperty("ThreeLetters");
                            words.Add(currentResult);
                        }
                    });
            }

            Task.WaitAll(tasks);

            sw.Stop();

            Console.WriteLine("Miliseconds: " + sw.ElapsedMilliseconds);
            Console.WriteLine("List length: " + words.Count);
            //Assert
        }


        [Test]
        public void Single_Thread()
        {
            SyntacticUnitConstructor constructor = CreateConstructorWithDefaultScheme();
            List<string> words = new List<string>();
            int totalAmountOfWords = amountOfWords;
            //Act
            Stopwatch sw = new Stopwatch();

            sw.Start();


            for (int i = 0; i < totalAmountOfWords; i++)
            {
                string currentResult = constructor.GetResultStringOfProperty("ThreeLetters");
                words.Add(currentResult);
            }


            sw.Stop();

            Console.WriteLine("Miliseconds: " + sw.ElapsedMilliseconds);
            Console.WriteLine("List length: " + words.Count);
        }
    }
}