using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LanguageGenerator.Core
{
    public static class RandomSingleton
    {
        static Random random;


        public static Random Random
        {
            get
            {
                if (random == null)
                    random = new Random();
                return random;
            }
        }
    }
}