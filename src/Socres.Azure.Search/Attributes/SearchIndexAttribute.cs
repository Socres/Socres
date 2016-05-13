using System;
using Microsoft.Azure.Search.Models;

namespace Socres.Azure.Search.Attributes
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field)]
    public class SearchIndexAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the search <see cref="Field"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is possible to facet on this field.
        /// Default is false.
        /// </summary>
        public bool IsFacetable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field can be used in filter expressions.
        /// Default is false.
        /// </summary>
        public bool IsFilterable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field is the key of the index.
        /// Valid only for string fields. Every index must have exactly one key field.
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field can be returned in a search result.
        /// Default is true.
        /// </summary>
        public bool IsRetrievable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field is included in full-text searches. Valid only forstring or string collection fields. 
        /// Default is false.
        /// </summary>
        public bool IsSearchable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field can be used in orderby expressions. 
        /// Default is false.
        /// </summary>
        public bool IsSortable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field represents a geographic point.
        /// Default is false.
        /// </summary>
        public bool IsGeoPoint {get; set; }

        /// <summary>
        /// Gets or sets the name of the analyser that should be appointed to the field.
        /// </summary>
        public string Analyser { get; set; }
    }
}