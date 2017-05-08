using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace LanguageGenerator.Core.Constructor
{
    public static class ConstructorExtensions
    {
        public static int MaxAmountOfStringToExecuteInSingleThread = 10000;
        public static List<string> GetStringListOfProprety(this ISyntacticUnitConstructor constructor, int amountOfStrings)
        {
            if (amountOfStrings < MaxAmountOfStringToExecuteInSingleThread)
            {
                return SingleThreadLoop(constructor, amountOfStrings);
            }
            return ParallelLoop(constructor, amountOfStrings);
        }


        private static List<string> SingleThreadLoop(ISyntacticUnitConstructor constructor, int amountOfWords)
        {
            List<string> words = new List<string>();
            for (int i = 0; i < amountOfWords; i++)
            {
                string currentResult = constructor.GetResultStringOfProperty("ThreeLetters");
                words.Add(currentResult);
            }
            return words;
        }


        private static List<string> ParallelLoop(ISyntacticUnitConstructor constructor, int amountOfWords)
        {
            List<string> words = new List<string>();
            int degreeOfParallelism = Environment.ProcessorCount;
            Task[] tasks = new Task[degreeOfParallelism];
            int amountOfWordsOnOneThread = amountOfWords / degreeOfParallelism;
            for (int taskNumber = 0; taskNumber < degreeOfParallelism; taskNumber++)
            {
                tasks[taskNumber] = Task.Factory.StartNew(
                    () =>
                    {
                        for (int i = 0; i < amountOfWordsOnOneThread; i++)
                        {
                            string currentResult = constructor.GetResultStringOfProperty("ThreeLetters");
                            words.Add(currentResult);
                        }
                    });
            }

            Task.WaitAll(tasks);
            return words;
        }
    }
}