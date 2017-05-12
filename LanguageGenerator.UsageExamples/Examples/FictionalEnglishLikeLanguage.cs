using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageGenerator.Core.LanguageConstructor;
using LanguageGenerator.Core.SUConstroctor;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticUnit.ParentSU;


namespace LanguageGenerator.UsageExamples.Examples
{
    class FictionalEnglishLikeLanguage
    {
        private readonly LanguageConstructor languageConstructor = new LanguageConstructor();
        public FictionalEnglishLikeLanguage()
        {
            languageConstructor.CreateRootProperty("consonant").CanStartFrom("Start").CanStartFrom("whitespace").CanStartFrom("consonant", 15).CanStartFrom("vowel", 100);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("w", 2360);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("r", 5987);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("t", 9056);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("d", 4253);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("p", 1929);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("s", 6327);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("f", 2228);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("g", 2015);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("h", 6094);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("j", 153);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("x", 105);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("k", 772);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("l", 4025);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("z", 74);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("v", 978);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("b", 1492);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("n", 6749);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("m", 2406);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("c", 2782);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("ch", 3000);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("y", 1974);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("q", 95);


            languageConstructor.CreateRootProperty("vowel").CanStartFrom("Start", 20).CanStartFrom("whitespace").CanStartFrom("consonant", 100).CanStartFrom("vowel", 15);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("a", 8167);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("u", 2758);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("o", 7507);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("i", 6966);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("e", 12702);

            languageConstructor.CreateParentProperty("word").CanStartFrom("Start").CanStartFrom("whitespace");
            languageConstructor.CreateParentSyntacticUnit("word", 180)
                               .AddPossibleChild("consonant")
                               .AddPossibleChild("vowel")
                               .AddChildrenAmount(5, 100)
                               .AddChildrenAmount(6, 150)
                               .AddChildrenAmount(7, 130);
            languageConstructor.CreateParentSyntacticUnit("word", 100)
                               .AddPossibleChild("consonant")
                               .AddPossibleChild("vowel")
                               .AddChildrenAmount(3, 130)
                               .AddChildrenAmount(4, 130);

            languageConstructor.CreateRootProperty("whitespace").CanStartFrom("word");
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty(" ", 150);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty(", ", 40);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty(": ", 5);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("-", 10);

            languageConstructor.CreateParentProperty("sentence base").CanStartFrom("Start");
            languageConstructor.CreateParentSyntacticUnit("sentence base")
                               .AddPossibleChild("word")
                               .AddPossibleChild("whitespace")
                               .AddChildrenAmount(3, 100)
                               .AddChildrenAmount(5, 120)
                               .AddChildrenAmount(7, 130)
                               .AddChildrenAmount(9, 110);

            languageConstructor.CreateRootProperty("sentence ending").CanStartFrom("sentence base");
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty(".", 200);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("!", 10);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("?", 50);
            languageConstructor.CreateRootSyntacticUnitWithLastCreatedProperty("?!", 5);



            languageConstructor.CreateParentProperty("sentence").CanStartFrom("Start");
            languageConstructor.CreateParentSyntacticUnit("sentence").AddPossibleChild("sentence base").AddPossibleChild("sentence ending").AddChildrenAmount(2);
        }

        public void PrintSentencesOfEnglishLikeLanguage(int amount)
        {
            List<string> results = languageConstructor.GetStringEnumerableOfProprety("sentence", amount).ToList();
            for (int index = 0; index < results.Count; index++)
            {
                results[index] = char.ToUpper(results[index][0]) + results[index].Substring(1);
                Console.WriteLine(results[index]);
            }
            Console.ReadLine();
        }
    }
}
