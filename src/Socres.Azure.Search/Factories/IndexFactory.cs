using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Azure.Search.Models;
using Socres.Azure.Search.Attributes;

namespace Socres.Azure.Search.Factories
{
    public class IndexFactory : IIndexFactory
    {
        private readonly IFieldFactory _fieldFactory;

        public IndexFactory(IFieldFactory fieldFactory)
        {
            _fieldFactory = fieldFactory;
        }

        /// <summary>
        /// Creates an <see cref="Index"/> from a specified type.
        /// </summary>
        /// <typeparam name="T">The type used to create the <see cref="Index"/> from.</typeparam>
        /// <param name="name">The name of the <see cref="Index"/>.</param>
        /// <returns>The created <see cref="Index"/>.</returns>
        public Index CreateIndexFrom<T>(string name)
        {
            return CreateIndexFrom(typeof (T), name);
        }

        public Index CreateIndexFrom(Type type, string name)
        {
            var index = new Index(name, _fieldFactory.CreateFieldCollection(type).ToArray());

            foreach (var suggesterInfo in GetSuggesters(type))
            {
                index.Suggesters.Add(new Suggester(suggesterInfo.Key, SuggesterSearchMode.AnalyzingInfixMatching, suggesterInfo.Value));
            }

            return index;

        }

        public IReadOnlyDictionary<string, List<string>> GetSuggestersFromType<T>()
        {
            return GetSuggesters(typeof (T));
        }

        public IReadOnlyDictionary<string, List<string>> GetSuggesters(Type type)
        {
            var properties = from p in type.GetProperties()
                             let suggestionAttribute = p.GetCustomAttribute(typeof(SearchSuggestionAttribute)) as SearchSuggestionAttribute
                             where suggestionAttribute != null
                             select p;

            var suggesters = new Dictionary<string, List<string>>();

            foreach (var suggesionProperty in properties)
            {
                foreach (var suggestionAttribute in suggesionProperty.GetCustomAttributes<SearchSuggestionAttribute>())
                {
                    if (!suggesters.ContainsKey(suggestionAttribute.SuggesterName))
                    {
                        suggesters.Add(suggestionAttribute.SuggesterName, new List<string>());
                    }

                    suggesters[suggestionAttribute.SuggesterName].Add(suggesionProperty.Name);
                }
            }

            return suggesters;
        }
    }
}
