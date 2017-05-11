using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.LanguageConstructor;
using LanguageGenerator.Core.SUConstroctor;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;


namespace LanguageGenerator.UsageExamples
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
            languageConstructor.CreateRootProperty("consonant").CanStartFrom("Start").CanStartFrom("vowel", 100);
            languageConstructor.CreateRootProperty("vowel").CanStartFrom("consonant", 100).CanStartFrom("vowel", 10);
            languageConstructor.CreateRootSyntacticUnit("w", "consonant", 100);
            languageConstructor.CreateRootSyntacticUnit("r", "consonant", 100);
            languageConstructor.CreateRootSyntacticUnit("t", "consonant", 100);
            languageConstructor.CreateRootSyntacticUnit("j", "consonant", 500);
            languageConstructor.CreateRootSyntacticUnit("a", "vowel", 100);
            languageConstructor.CreateRootSyntacticUnit("u", "vowel", 100);
            languageConstructor.CreateRootSyntacticUnit("o", "vowel", 100);
            languageConstructor.CreateParentProperty("word").CanStartFrom("Start");
            languageConstructor.CreateParentSyntacticUnit("word").AddPossibleChild("consonant", 100).AddPossibleChild("vowel", 80).AddChildrenAmount(3, 100);
        }


        private void GenerateLanguageGraphByBetterChainedSyntax()
        {
            languageConstructor.CreateRootProperty("consonant").CanStartFrom("Start").CanStartFrom("vowel", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("w", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("r", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("t", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("j", 500);

            languageConstructor.CreateRootProperty("vowel").CanStartFrom("consonant", 100).CanStartFrom("vowel", 10);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("a", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("u", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("o", 100);

            languageConstructor.CreateParentProperty("word").CanStartFrom("Start");
            languageConstructor.CreateParentSyntacticUnit("word").AddPossibleChild("consonant", 100).AddPossibleChild("vowel", 80).AddChildrenAmount(3, 100);
        }



        public void PrintAmountWords(int amount)
        {
            IEnumerable<string> words = languageConstructor.GetStringListOfProprety("word", amount);
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
            Console.ReadLine();
        }
    }
}