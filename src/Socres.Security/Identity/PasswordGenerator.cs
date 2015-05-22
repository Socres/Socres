namespace Socres.Security.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Socres.Security.Properties;

    // Thanks to: http://www.siepman.nl/blog/post/2014/05/31/Random-password-generator-with-numbers-and-special-characters.aspx
    public class PasswordGenerator : IPasswordGenerator
    {
        private readonly string _allLowerCaseChars;
        private readonly string _allUpperCaseChars;
        private readonly string _allNumericChars;
        private readonly string _allSpecialChars;
        private readonly Random _random;

        public PasswordGenerator()
        {
            _random = new Random();
            _allLowerCaseChars = GetCharRange('a', 'z');
            _allUpperCaseChars = GetCharRange('A', 'Z');
            _allNumericChars = GetCharRange('0', '9');
            _allSpecialChars = "!@#%*()$?+-=";
        }

        /// <summary>
        /// Generates this password.
        /// </summary>
        /// <param name="minimumLengthPassword">The minimum length password.</param>
        /// <param name="maximumLengthPassword">The maximum length password.</param>
        /// <param name="minimumLowerCaseChars">The minimum lower case chars.</param>
        /// <param name="minimumUpperCaseChars">The minimum upper case chars.</param>
        /// <param name="minimumNumericChars">The minimum numeric chars.</param>
        /// <param name="minimumSpecialChars">The minimum special chars.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// minimumLengthPassword
        /// or
        /// minimumLengthPassword
        /// or
        /// minimumLowerCaseChars
        /// or
        /// minimumUpperCaseChars
        /// or
        /// minimumNumericChars
        /// or
        /// minimumSpecialChars
        /// or
        /// maximumLengthPassword
        /// </exception>
        public string Generate(int minimumLengthPassword = 8,
            int maximumLengthPassword = 15,
            int minimumLowerCaseChars = 1,
            int minimumUpperCaseChars = 1,
            int minimumNumericChars = 1,
            int minimumSpecialChars = 1)
        {
            if (minimumLengthPassword < 1)
            {
                throw new ArgumentException(Resources.PasswordGenerator_Minimumlength_Error,
                    "minimumLengthPassword");
            }

            if (minimumLengthPassword > maximumLengthPassword)
            {
                throw new ArgumentException(Resources.PasswordGenerator_MinimumLength_Bigger_Maximum_Error,
                    "minimumLengthPassword");
            }

            if (minimumLowerCaseChars < 0)
            {
                throw new ArgumentException(Resources.PasswordGenerator_MinimumLowerCase_Error,
                    "minimumLowerCaseChars");
            }

            if (minimumUpperCaseChars < 0)
            {
                throw new ArgumentException(Resources.PasswordGenerator_MinimumUpperCase_Error,
                    "minimumUpperCaseChars");
            }

            if (minimumNumericChars < 0)
            {
                throw new ArgumentException(Resources.PasswordGenerator_MinimumNumeric_Error,
                    "minimumNumericChars");
            }

            if (minimumSpecialChars < 0)
            {
                throw new ArgumentException(Resources.PasswordGenerator_MinimumSpecial_Error,
                    "minimumSpecialChars");
            }

            var minimumNumberOfChars = minimumLowerCaseChars + minimumUpperCaseChars +
                                    minimumNumericChars + minimumSpecialChars;

            if (minimumLengthPassword < minimumNumberOfChars)
            {
                throw new ArgumentException(
                    Resources.PasswordGenerator_MinimumLength_SmallerThanSum_Error,
                    "maximumLengthPassword");
            }

            var allAvailableChars =
                OnlyIfOneCharIsRequired(minimumNumberOfChars, minimumLowerCaseChars, _allLowerCaseChars) +
                OnlyIfOneCharIsRequired(minimumNumberOfChars, minimumUpperCaseChars, _allUpperCaseChars) +
                OnlyIfOneCharIsRequired(minimumNumberOfChars, minimumNumericChars, _allNumericChars) +
                OnlyIfOneCharIsRequired(minimumNumberOfChars, minimumSpecialChars, _allSpecialChars);

            var lengthOfPassword = _random.Next(minimumLengthPassword, maximumLengthPassword);

            // Get the required number of characters of each catagory and 
            // add random charactes of all catagories
            var minimumChars = GetRandomString(_allLowerCaseChars, minimumLowerCaseChars) +
                            GetRandomString(_allUpperCaseChars, minimumUpperCaseChars) +
                            GetRandomString(_allNumericChars, minimumNumericChars) +
                            GetRandomString(_allSpecialChars, minimumSpecialChars);
            var rest = GetRandomString(allAvailableChars, lengthOfPassword - minimumChars.Length);
            var unshuffeledResult = minimumChars + rest;

            // Shuffle the result so the order of the characters are unpredictable
            var result = ShuffleTextSecure(unshuffeledResult);
            return result;
        }

        private string OnlyIfOneCharIsRequired(int minimumNumberOfChars, int minimum, string allChars)
        {
            return minimum > 0 || minimumNumberOfChars == 0 ? allChars : string.Empty;
        }

        private string GetRandomString(string possibleChars, int lenght)
        {
            var result = string.Empty;
            for (var position = 0; position < lenght; position++)
            {
                var index = _random.Next(possibleChars.Length);
                result += possibleChars[index];
            }
            return result;
        }

        private static string GetCharRange(char minimum, char maximum, string exclusiveChars = "")
        {
            var result = string.Empty;
            for (var value = minimum; value <= maximum; value++)
            {
                result += value;
            }
            if (!string.IsNullOrEmpty(exclusiveChars))
            {
                var inclusiveChars = result.Except(exclusiveChars).ToArray();
                result = new string(inclusiveChars);
            }
            return result;
        }

        private IEnumerable<T> ShuffleSecure<T>(IEnumerable<T> source)
        {
            var sourceArray = source.ToArray();
            for (var counter = 0; counter < sourceArray.Length; counter++)
            {
                var randomIndex = _random.Next(counter, sourceArray.Length);
                yield return sourceArray[randomIndex];

                sourceArray[randomIndex] = sourceArray[counter];
            }
        }

        private string ShuffleTextSecure(string source)
        {
            var shuffeldChars = ShuffleSecure(source).ToArray();
            return new string(shuffeldChars);
        }
    }
}
