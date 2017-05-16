using System;
using System.Collections.Generic;
using System.Linq;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.SyntacticProperty
{
    public static class ExtensionOfIProperty
    {
        [Obsolete]
        public static T CanStartFrom<T>(this T property, string propertyToGoAfter, int withFrequency = 100) where T : IProperty
        {
            property.StartsWithFrequencyFromPropertyName.Add(propertyToGoAfter, withFrequency);
            return property;
        }


        [Obsolete]
        public static T CanStartFrom<T>(this T property, IProperty propertyToGoAfter, int withFrequency = 100) where T : IProperty
        {
            property.StartsWithFrequencyFrom.Add(propertyToGoAfter, withFrequency);
            return property;
        }


        public static void CanStartFrom<T>(this T property, params KeyValuePair<IProperty, int>[] propertiesToStartFrom) where T : IProperty
        {
            foreach (KeyValuePair<IProperty, int> propertyToStartFrom in propertiesToStartFrom)
            {
                property.StartsWithFrequencyFrom.Add(propertyToStartFrom.Key, propertyToStartFrom.Value);
            }
        }


        public static void CanStartFrom<T>(this T property, params KeyValuePair<string, int>[] propertiesToStartFrom) where T : IProperty
        {
            foreach (KeyValuePair<string, int> propertyToStartFrom in propertiesToStartFrom)
            {
                property.StartsWithFrequencyFromPropertyName.Add(propertyToStartFrom.Key, propertyToStartFrom.Value);
            }
        }


        public static T ChildOfSyntacticUnit<T>(this T property, IParentSU syntacticUnit, int withFrequency = 100) where T : IProperty
        {
            syntacticUnit.PossibleChildren.Add(property, withFrequency);
            return property;
        }


        public static bool DoesPropertyCanStartFrom(this IProperty thePopertyThatStart, IProperty propertyToStartFrom)
        {
            return thePopertyThatStart.FrequencyToStartFromProperty(propertyToStartFrom) > 0;
        }


        public static bool DoesProppertyCanStartFromAnyOf(this IProperty thePopertyThatStart, IEnumerable<IProperty> propertiesToStartFrom)
        {
            return propertiesToStartFrom.Any(property => thePopertyThatStart.DoesPropertyCanStartFrom(property));
        }
    }
}