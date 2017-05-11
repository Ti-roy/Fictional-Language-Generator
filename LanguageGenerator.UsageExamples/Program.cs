using System;
using LanguageGenerator.UsageExamples.Exapmles;


namespace LanguageGenerator.UsageExamples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new OrcLanguage().PrintSentencesOfOrcishLanguage(20);
            Console.ReadLine();
        }
    }
}