using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticProperty;


namespace LanguageGenerator.Core
{
    public static class Pair
    {
        //This class doesn`t use generic or boxing to make usage of it shorter, and typesafe.
        public static KeyValuePair<string, int> Of(string stringRepresentation, int frequency = 100)
        {
            return new KeyValuePair<string, int>(stringRepresentation, frequency);
        }


        public static KeyValuePair<int, int> Of(int childrenAmount, int frequency = 100)
        {
            return new KeyValuePair<int, int>(childrenAmount, frequency);
        }


        public static KeyValuePair<IProperty, int> Of(IProperty childrenAmount, int frequency = 100)
        {
            return new KeyValuePair<IProperty, int>(childrenAmount, frequency);
        }
    }
}