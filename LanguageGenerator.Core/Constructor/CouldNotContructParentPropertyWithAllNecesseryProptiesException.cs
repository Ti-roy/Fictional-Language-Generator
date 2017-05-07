using System;


namespace LanguageGenerator.Core.Constructor
{
    public class CouldNotContructParentPropertyWithAllNecesseryProptiesException :Exception
    {
        public CouldNotContructParentPropertyWithAllNecesseryProptiesException()
        {
        }

        public CouldNotContructParentPropertyWithAllNecesseryProptiesException(string message)
            : base(message)
        {
        }

        public CouldNotContructParentPropertyWithAllNecesseryProptiesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}