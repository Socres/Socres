using System;
using Newtonsoft.Json;

namespace SocresSamples.Azure.Search.Customizations
{
    public static class StringExtensions
    {
        /// <summary>
        /// Parse the JSON string and returns it as a typed object.
        /// </summary>
        /// <typeparam name="T">The type of the return object.</typeparam>
        /// <param name="json">The json string.</param>
        /// <param name="converters">Custom converters</param>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="System.ArgumentException">Thrown when the value is not in correct JSON format.</exception>
        public static T ToObject<T>(this string json, params JsonConverter[] converters)
        {
            try
            {
                var settings =
                    new JsonSerializerSettings()
                    {
                        Converters = converters
                    };

                return JsonConvert.DeserializeObject<T>(json, settings);
            }
            catch (JsonReaderException exception)
            {
                throw new ArgumentException(
                    string.Format(@"Value '{0}' is not a valid '{1}'.", json, typeof(T).FullName),
                    exception);
            }
        }
    }
}