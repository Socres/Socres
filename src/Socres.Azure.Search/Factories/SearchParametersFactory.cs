using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Search.Models;

namespace Socres.Azure.Search.Factories
{
    public class SearchParametersFactory
    {
        private readonly IFieldFactory _fieldFactory;

        public SearchParametersFactory(IFieldFactory fieldFactory)
        {
            _fieldFactory = fieldFactory;
        }

        public SearchParameters CreateSearchParametersFromType<T>(string filter)
        {
            return CreateSearchParameters(typeof (T), filter);
        }

        public SearchParameters CreateSearchParameters(Type type, string filter)
        {
            var searchParameters = new SearchParameters
            {
                Facets = GetFacetableFieldNamesFromType(type).ToList(),
                Filter = filter
            };

            return searchParameters;
        }

        private IEnumerable<string> GetFacetableFieldNamesFromType(Type type)
        {
            return ( from field in _fieldFactory.CreateFieldCollection(type)
                     where field.IsFacetable
                     select field.Name );
        }
    }
}
