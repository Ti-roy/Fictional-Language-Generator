using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LanguageGenerator.Core.SUConstroctor
{
    public static class ConstructorExtensions
    {
        public static int MaxAmountOfStringToExecuteInSingleThread = 10000;


        public static List<string> GetStringListOfProprety(this ISyntacticUnitConstructor constructor, string propertyName, int amountOfStrings)
        {
            if (amountOfStrings < MaxAmountOfStringToExecuteInSingleThread)
            {
                return SingleThreadLoop(constructor, propertyName, amountOfStrings);
            }
            return ParallelLoop(constructor, propertyName, amountOfStrings);
        }


        private static List<string> SingleThreadLoop(ISyntacticUnitConstructor constructor, string propertyName, int amountOfWords)
        {
            List<string> words = new List<string>();
            for (int i = 0; i < amountOfWords; i++)
            {
                string currentResult = constructor.GetResultStringOfProperty(propertyName);
                words.Add(currentResult);
            }
            return words;
        }


        private static List<string> ParallelLoop(ISyntacticUnitConstructor constructor, string propertyName, int amountOfWords)
        {
            ConcurrentBag<string> words = new ConcurrentBag<string>();
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
                            string currentResult = constructor.GetResultStringOfProperty(propertyName);
                            words.Add(currentResult);
                        }
                    });
            }

            Task.WaitAll(tasks);
            return words.ToList();
        }
    }
}