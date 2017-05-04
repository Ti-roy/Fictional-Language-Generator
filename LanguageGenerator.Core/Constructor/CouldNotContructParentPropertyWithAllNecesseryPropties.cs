using System;


namespace LanguageGenerator.Core.Constructor
{
    public class CouldNotContructParentPropertyWithAllNecesseryPropties :Exception
    {
        public CouldNotContructParentPropertyWithAllNecesseryPropties()
        {
        }

        public CouldNotContructParentPropertyWithAllNecesseryPropties(string message)
            : base(message)
        {
        }

        public CouldNotContructParentPropertyWithAllNecesseryPropties(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}