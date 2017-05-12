using System;
using LanguageGenerator.UsageExamples.Examples;


namespace LanguageGenerator.UsageExamples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new OrcLanguage().PrintSentencesOfOrcishLanguage(20);
            Console.WriteLine();
            new ElvenLanguage().PrintSentencesOfElvenLanguage(20);
            Console.WriteLine();
            new FictionalEnglishLikeLanguage().PrintSentencesOfEnglishLikeLanguage(20);
            Console.ReadLine();
        }
    }
}