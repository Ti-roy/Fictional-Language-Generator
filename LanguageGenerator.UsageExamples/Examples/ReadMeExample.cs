using System;
using System.Collections.Generic;
using LanguageGenerator.Core;
using LanguageGenerator.Core.AbstractFactory;
using LanguageGenerator.Core.LanguageConstructor;
using LanguageGenerator.Core.SUConstroctor;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;


namespace LanguageGenerator.UsageExamples.Examples
{
    internal class ReadMeExample
    {
        private readonly LanguageConstructor languageConstructor = new LanguageConstructor();


        public ReadMeExample()
        {
            GenerateLanguageGraphByExactExampleCode();
        }


        private void GenerateLanguageGraphByExactExampleCode()
        {
            IRootProperty consonant = languageConstructor.CreateRootProperty("consonant");
            consonant.CanStartFrom(Pair.Of("Start", 100), Pair.Of("vowel", 100));
            IRootProperty vowel = languageConstructor.CreateRootProperty("vowel");
            vowel.CanStartFrom(Pair.Of("consonant", 100), Pair.Of("vowel", 10));
            languageConstructor.CreateRootSyntacticUnitsOfProperty(consonant, Pair.Of("w", 100), Pair.Of("r", 100), Pair.Of("t", 100));
            languageConstructor.CreateRootSyntacticUnitsOfProperty(consonant, Pair.Of("j", 500));
            languageConstructor.CreateRootSyntacticUnitsOfProperty(vowel, Pair.Of("a", 100), Pair.Of("o", 100), Pair.Of("u", 100));
            IParentProperty word = languageConstructor.CreateParentProperty("word");
            word.CanStartFrom("Start", 100);
            IParentSU wordSu = languageConstructor.CreateParentSyntacticUnit("word");
            wordSu.AddPossibleChild(Pair.Of("consonant", 100), Pair.Of("vowel", 80));
            wordSu.AddChildrenAmount(3, 100);
        }


        public void PrintAmountWords(int amount)
        {
            IEnumerable<string> words = languageConstructor.GetStringEnumerableOfProperty("word", amount);
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
            Console.ReadLine();
        }
    }
}