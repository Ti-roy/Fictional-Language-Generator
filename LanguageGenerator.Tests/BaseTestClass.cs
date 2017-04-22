using System;
using DryIoc;
using NSubstitute;
using Arg = DryIoc.Arg;


namespace LanguageGenerator.Tests
{
    class BaseTestClass
    {
        public Container GetAutoMockingContainer()
        {
            return new Container(rules => rules.WithUnknownServiceResolvers(request =>
            {
                var serviceType = request.ServiceType;
                if (!serviceType.IsAbstract)
                    return null; // Mock interface or abstract class only.

                return new ReflectionFactory(made: Made.Of(
                    () => Substitute.For(Arg.Index<Type[]>(0), Arg.Index<object[]>(1)),
                    _ => new[] { serviceType }, _ => (object[])null));
            }));
        }
    }
}
