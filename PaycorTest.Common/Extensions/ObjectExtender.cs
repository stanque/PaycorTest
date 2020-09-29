using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaycorTest.Common.Extensions
{
    public static class ObjectExtender
    {
        public static string ToQueryString(this object request, string prefix = null, int level = 0)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => string.IsNullOrEmpty(prefix) ? x.Name : $"{prefix}.{x.Name}", x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var collectionPropertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => string.IsNullOrEmpty(prefix) ? x.Key : $"{prefix}.{x.Key}")
                .ToList();

            var nestedClassProperties = properties.Where
                                        (
                                            x => x.Value.GetType().IsClass &&
                                            !(x.Value is string) &&
                                            !collectionPropertyNames.Any(y => y == x.Key)
                                        )
                                        .ToList();

            var propertyKeysToRemove = properties.Keys.Where(x => nestedClassProperties.Any(y => y.Key == x)).ToList();
            foreach (var propName in propertyKeysToRemove)
            {
                properties.Remove(propName);
            }

            StringBuilder nestedPropStringBuilder = new StringBuilder();

            //No need to go further that 1 nested level. 
            if (level == 0)
            {
                foreach (var nestedClassProperty in nestedClassProperties)
                {
                    nestedPropStringBuilder.Append(ToQueryString(nestedClassProperty.Value, nestedClassProperty.Key, ++level));
                }
            }

            // Concat all IEnumerable properties into a comma separated string
            List<string> propertyCollectionNamesToRemove = new List<string>();
            string cumulativeCollPart = string.Empty;
            foreach (var key in collectionPropertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    string newValue = null;
                    foreach (var member in enumerable)
                    {
                        newValue += $"{Uri.EscapeDataString(key)}={ Uri.EscapeDataString(member.ToString()) }&";
                    }

                    if (newValue != null)
                    {
                        newValue = newValue.Remove(newValue.LastIndexOf('&'));

                        cumulativeCollPart += $"{newValue}&";
                    }
                    
                    propertyCollectionNamesToRemove.Add(key);
                }
            }

            if (!string.IsNullOrWhiteSpace(cumulativeCollPart))
            {
                cumulativeCollPart = cumulativeCollPart.Remove(cumulativeCollPart.LastIndexOf('&'));
            }

            foreach (var toRemoveKey in propertyCollectionNamesToRemove)
            {
                properties.Remove(toRemoveKey);
            }

            var nestedClasses = nestedPropStringBuilder.ToString();

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join
                (
                    "&",
                    properties
                    .Select
                    (
                        x =>
                        string.Concat
                        (
                            Uri.EscapeDataString(x.Key), "=",
                            Uri.EscapeDataString
                            (
                                x.Value is DateTime ?
                                ((DateTime)x.Value).ToString("s") :
                                x.Value.ToString()
                            )
                        )
                    )
                )
                + (string.IsNullOrWhiteSpace(cumulativeCollPart) ? string.Empty : $"&{cumulativeCollPart}")
                + (string.IsNullOrWhiteSpace(nestedClasses) ? string.Empty : $"&{nestedClasses}");
        }
    }
}
