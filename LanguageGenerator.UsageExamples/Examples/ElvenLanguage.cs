using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.LanguageConstructor;
using LanguageGenerator.Core.SUConstroctor;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;


namespace LanguageGenerator.UsageExamples.Examples
{
    class ElvenLanguage
    {
        private readonly LanguageConstructor languageConstructor = new LanguageConstructor();

        public ElvenLanguage()
        {
            languageConstructor.CreateRootProperty("consonant").CanStartFrom("Start").CanStartFrom("whitespace").CanStartFrom("consonant", 5).CanStartFrom("vowel", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("w", 80);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("r", 60);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("t", 120);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("d", 70);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("p", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("s", 150);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("f", 120);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("g", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("h", 110);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("k", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("l", 170);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("z", 80);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("v", 130);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("b", 80);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("n", 150);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("m", 160);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("ch", 80);

            languageConstructor.CreateRootProperty("vowel").CanStartFrom("Start", 20).CanStartFrom("whitespace").CanStartFrom("consonant", 100).CanStartFrom("vowel", 15);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("a", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("u", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("o", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("i", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("e", 100);

            languageConstructor.CreateParentProperty("elven word").CanStartFrom("Start").CanStartFrom("whitespace");
            languageConstructor.CreateParentSyntacticUnit("elven word",180)
                               .AddPossibleChild("consonant")
                               .AddPossibleChild("vowel")
                               .AddChildrenAmount(5, 100)
                               .AddChildrenAmount(6, 150)
                               .AddChildrenAmount(7, 130);
            languageConstructor.CreateParentSyntacticUnit("elven word", 100)
                               .AddPossibleChild("consonant")
                               .AddPossibleChild("vowel")
                               .AddChildrenAmount(3, 130)
                               .AddChildrenAmount(4, 130);

            languageConstructor.CreateRootProperty("whitespace").CanStartFrom("elven word");
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty(" ",150);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty(", ", 70);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty(": ", 10);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("-", 40);

            languageConstructor.CreateParentProperty("elven sentence base").CanStartFrom("Start");
            languageConstructor.CreateParentSyntacticUnit("elven sentence base")
                               .AddPossibleChild("elven word")
                               .AddPossibleChild("whitespace")
                               .AddChildrenAmount(3, 100)
                               .AddChildrenAmount(5, 120)
                               .AddChildrenAmount(7, 130)
                               .AddChildrenAmount(9, 110);

            languageConstructor.CreateRootProperty("sentence ending").CanStartFrom("elven sentence base");
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty(".", 200);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("!", 40);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("?", 80);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("?!", 20);



            languageConstructor.CreateParentProperty("elven sentence").CanStartFrom("Start");
            languageConstructor.CreateParentSyntacticUnit("elven sentence").AddPossibleChild("elven sentence base").AddPossibleChild("sentence ending").AddChildrenAmount(2);
        }

        public void PrintSentencesOfElvenLanguage(int amount)
        {
            List<string> results = languageConstructor.GetStringEnumerableOfProprety("elven sentence", amount).ToList();
            for (int index = 0; index < results.Count; index++)
            {
                results[index] = char.ToUpper(results[index][0]) + results[index].Substring(1);
                Console.WriteLine(results[index]);
            }
            Console.ReadLine();
        }
    }
}
