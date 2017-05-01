using System;


namespace LanguageGenerator.Core.AbstractFactory
{
    public class PropertyNotExistsInRepositoryException : Exception
    {
        public PropertyNotExistsInRepositoryException()
        {
        }


        public PropertyNotExistsInRepositoryException(string message) : base(message)
        {
        }


        public PropertyNotExistsInRepositoryException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}