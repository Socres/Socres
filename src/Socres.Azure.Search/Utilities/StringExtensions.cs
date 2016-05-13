using System.Globalization;

namespace Socres.Azure.Search.Utilities
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the sting to Camel Case.
        /// </summary>
        /// <remarks>
        /// Borrowd thsi code from the JSON.Net code
        /// See https://github.com/JamesNK/Newtonsoft.Json/blob/master/Src/Newtonsoft.Json/Utilities/StringUtils.cs
        /// Too bad the StringUtils class is internal.s
        /// </remarks>
        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
            {
                return s;
            }

            char[] chars = s.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i == 1 && !char.IsUpper(chars[i]))
                {
                    break;
                }

                bool hasNext = ( i + 1 < chars.Length );
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    break;
                }

#if !(DOTNET || PORTABLE)
                chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
#else
                chars[i] = char.ToLowerInvariant(chars[i]);
#endif
            }

            return new string(chars);
        }
    }
}
