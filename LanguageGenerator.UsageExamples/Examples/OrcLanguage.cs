using System;
using System.Collections.Generic;
using System.Linq;
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
    internal class OrcLanguage
    {
        private readonly LanguageConstructor languageConstructor = new LanguageConstructor();


        public OrcLanguage()
        {
            IRootProperty consonant = languageConstructor.CreateRootProperty("consonant");
            consonant.CanStartFrom(Pair.Of("Start", 100), Pair.Of("whitespace", 100), Pair.Of("consonant", 15), Pair.Of("vowel", 100));
            languageConstructor.CreateRootSyntacticUnitsOfProperty(
                consonant,
                Pair.Of("w", 150),
                Pair.Of("r", 180),
                Pair.Of("t", 120),
                Pair.Of("d", 150),
                Pair.Of("p", 110),
                Pair.Of("s", 80),
                Pair.Of("f", 20),
                Pair.Of("g", 80),
                Pair.Of("h", 80),
                Pair.Of("k", 130),
                Pair.Of("l", 50),
                Pair.Of("z", 130),
                Pair.Of("ch", 100),
                Pair.Of("v", 130),
                Pair.Of("b", 150),
                Pair.Of("n", 70),
                Pair.Of("m", 50),
                Pair.Of("c", 110));

            IRootProperty vowel = languageConstructor.CreateRootProperty("vowel");
            vowel.CanStartFrom(Pair.Of("Start", 20), Pair.Of("whitespace", 20), Pair.Of("consonant", 100), Pair.Of("vowel", 5));
            languageConstructor.CreateRootSyntacticUnitsOfProperty(
                vowel,
                Pair.Of("a", 100),
                Pair.Of("u", 100),
                Pair.Of("o", 100),
                Pair.Of("i", 40),
                Pair.Of("e", 100));

            IParentProperty orcishWord = languageConstructor.CreateParentProperty("orcish word");
            orcishWord.CanStartFrom(Pair.Of("Start", 100), Pair.Of("whitespace", 100));
            IParentSU longOrcishWordSU = languageConstructor.CreateParentSyntacticUnit(orcishWord);
            longOrcishWordSU.AddPossibleChildren(Pair.Of(consonant, 100), Pair.Of(vowel, 100));
            longOrcishWordSU.AddChildrenAmount(Pair.Of(5, 100), Pair.Of(6, 150), Pair.Of(7, 130));
            IParentSU shortOrcishWordSU = languageConstructor.CreateParentSyntacticUnit(orcishWord, 160);
            shortOrcishWordSU.AddPossibleChildren(Pair.Of(consonant, 100), Pair.Of(vowel, 100));
            shortOrcishWordSU.AddChildrenAmount(Pair.Of(3, 150), Pair.Of(4, 130));

            IRootProperty whitespace = languageConstructor.CreateRootProperty("whitespace");
            whitespace.CanStartFrom(orcishWord);
            languageConstructor.CreateRootSyntacticUnitsOfProperty(whitespace, Pair.Of(" ", 100), Pair.Of(", ", 30), Pair.Of(": ", 5));

            IParentProperty orcisgSentenceBase = languageConstructor.CreateParentProperty("orcish sentence base");
            orcisgSentenceBase.CanStartFrom("Start", 100);
            IParentSU orcishSentenceBaseSU = languageConstructor.CreateParentSyntacticUnit(orcisgSentenceBase);
            orcishSentenceBaseSU.AddPossibleChildren(Pair.Of(orcishWord, 100), Pair.Of(whitespace, 100));
            orcishSentenceBaseSU.AddChildrenAmount(Pair.Of(3, 120), Pair.Of(5, 110), Pair.Of(7, 100), Pair.Of(9, 100));

            IRootProperty sentenceEnding = languageConstructor.CreateRootProperty("sentence ending");
            sentenceEnding.CanStartFrom(orcisgSentenceBase);
            languageConstructor.CreateRootSyntacticUnitsOfProperty(
                sentenceEnding,
                Pair.Of(".", 100),
                Pair.Of("!", 50),
                Pair.Of("?", 50),
                Pair.Of("?!", 70),
                Pair.Of("!!!", 50));


            IParentProperty orcishSentence = languageConstructor.CreateParentProperty("orcish sentence");
            orcishSentence.CanStartFrom("Start", 100);
            IParentSU orcishSentenceSU = languageConstructor.CreateParentSyntacticUnit(orcishSentence);
            orcishSentenceSU.AddPossibleChildren(Pair.Of(orcisgSentenceBase, 100), Pair.Of(sentenceEnding, 100));
            orcishSentenceSU.AddChildrenAmount(2, 1);
        }


        public void PrintSentencesOfOrcishLanguage(int amount)
        {
            List<string> results = languageConstructor.GetStringEnumerableOfProperty("orcish sentence", amount).ToList();
            for (int index = 0; index < results.Count; index++)
            {
                results[index] = char.ToUpper(results[index][0]) + results[index].Substring(1);
                Console.WriteLine(results[index]);
            }
            Console.ReadLine();
        }
    }
}