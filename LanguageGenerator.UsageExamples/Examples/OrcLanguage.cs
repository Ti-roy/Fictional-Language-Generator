using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.LanguageConstructor;
using LanguageGenerator.Core.SUConstroctor;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;


namespace LanguageGenerator.UsageExamples.Examples
{
    internal class OrcLanguage
    {
        private readonly LanguageConstructor languageConstructor = new LanguageConstructor();


        public OrcLanguage()
        {
            languageConstructor.CreateRootProperty("consonant").CanStartFrom("Start").CanStartFrom("whitespace").CanStartFrom("consonant", 15).CanStartFrom("vowel", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("w", 150);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("r", 180);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("t", 120);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("d", 150);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("p", 110);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("s", 80);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("f", 20);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("g", 80);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("h", 80);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("k", 130);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("l", 50);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("z", 130);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("v", 130);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("b", 150);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("n", 70);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("m", 50);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("ch", 100);

            languageConstructor.CreateRootProperty("vowel").CanStartFrom("Start", 20).CanStartFrom("whitespace",20).CanStartFrom("consonant", 100).CanStartFrom("vowel", 5);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("a", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("u", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("o", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("i", 40);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("e", 100);

            languageConstructor.CreateParentProperty("orcish word").CanStartFrom("Start").CanStartFrom("whitespace");
            languageConstructor.CreateParentSyntacticUnit("orcish word")
                               .AddPossibleChild("consonant")
                               .AddPossibleChild("vowel")
                               .AddChildrenAmount(5, 100)
                               .AddChildrenAmount(6, 150)
                               .AddChildrenAmount(7, 130);
            languageConstructor.CreateParentSyntacticUnit("orcish word",160)
                               .AddPossibleChild("consonant")
                               .AddPossibleChild("vowel")
                               .AddChildrenAmount(3, 150)
                               .AddChildrenAmount(4, 130);

            languageConstructor.CreateRootProperty("whitespace").CanStartFrom("orcish word");
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty(" ");
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty(", ",30);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty(": ",5);

            languageConstructor.CreateParentProperty("orcish sentence base").CanStartFrom("Start");
            languageConstructor.CreateParentSyntacticUnit("orcish sentence base")
                               .AddPossibleChild("orcish word")
                               .AddPossibleChild("whitespace")
                               .AddChildrenAmount(3, 120)
                               .AddChildrenAmount(5, 110)
                               .AddChildrenAmount(7, 100)
                               .AddChildrenAmount(9, 100);

            languageConstructor.CreateRootProperty("sentence ending").CanStartFrom("orcish sentence base");
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty(".", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("!", 50);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("?", 50);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("?!", 70);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("!!!", 50);



            languageConstructor.CreateParentProperty("orcish sentence").CanStartFrom("Start");
            languageConstructor.CreateParentSyntacticUnit("orcish sentence").AddPossibleChild("orcish sentence base").AddPossibleChild("sentence ending").AddChildrenAmount(2);
        }

        public void PrintSentencesOfOrcishLanguage(int amount)
        {
            List<string> results = languageConstructor.GetStringEnumerableOfProprety("orcish sentence", amount).ToList();
            for (int index = 0; index < results.Count; index++)
            {
                results[index] = char.ToUpper(results[index][0]) + results[index].Substring(1);
                Console.WriteLine(results[index]);
            }
            Console.ReadLine();
        }
    }
}