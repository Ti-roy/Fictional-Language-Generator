using System;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public static class ExtensionOfIProperty
    {
        public static IProperty PropertyCanGoAfter(this IProperty property, string propertyToGoAfter, int withFrequency = 1000)
        {
            if(property is IOrderInfoForLinker)
            {
                IOrderInfoForLinker order = (IOrderInfoForLinker)property;
                order.StartsWithFrequencyFromPropertyName.Add(propertyToGoAfter, withFrequency);
                return property;
            }
            else
            {
                throw new InvalidOperationException("Current implementation of IProperty doesnt implement IOrderInfoForLinker. You can use PropertyCanGoAfter overload with IProperty reference instead.");
            }
        }

        public static IProperty PropertyCanGoAfter(this IProperty property, IProperty propertyToGoAfter, int withFrequency = 1000)
        {
            property.StartsWithFrequencyFrom.Add(propertyToGoAfter, withFrequency);
            return property;
        }


        public static IProperty ChildOfSyntacticUnit(this IProperty property, IParentSU syntacticUnit, int withFrequency = 1000)
        {
            syntacticUnit.PossibleChildren.Add(property, withFrequency);
            return property;
        }
    }
}