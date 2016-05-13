using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Azure.Search.Models;
using Microsoft.Spatial;
using Socres.Azure.Search.Attributes;
using Socres.Azure.Search.Utilities;

namespace Socres.Azure.Search.Factories
{
    public class FieldFactory : IFieldFactory
    {
        public IEnumerable<Field> CreateFieldCollection(Type type)
        {
            var useCamelCasing = type.CustomAttributes.Any(a => a.AttributeType.Name == "SerializePropertyNamesAsCamelCaseAttribute");

            return ( from p in type.GetProperties()
                     let field = CreateFieldFromProperty(p, useCamelCasing)
                     where field != null
                     select field );
        }

        private Field CreateFieldFromProperty(PropertyInfo propertyInfo, bool useCamelCasing)
        {
            var searchIntexAttribute =
                propertyInfo.GetCustomAttribute(typeof(SearchIndexAttribute)) as SearchIndexAttribute;

            if (searchIntexAttribute == null)
            {
                return null;
            }

            var fieldName = searchIntexAttribute.Name ?? propertyInfo.Name;

            if (useCamelCasing && !string.IsNullOrEmpty(fieldName))
            {
                fieldName = fieldName.ToCamelCase();
            }
            
            var field = new Field(fieldName, ToDataType(propertyInfo.PropertyType))
            {
                IsKey = searchIntexAttribute.IsKey,
                IsRetrievable = searchIntexAttribute.IsRetrievable,
                IsSearchable = searchIntexAttribute.IsSearchable,
                IsSortable = searchIntexAttribute.IsSortable,
                IsFilterable = searchIntexAttribute.IsFilterable,
                IsFacetable = searchIntexAttribute.IsFacetable
            };
            return field;
        }

        private DataType ToDataType(Type type)
        {
            // see https://msdn.microsoft.com/en-us/library/azure/dn798938.aspx

            if (type == typeof(string)) return DataType.String;
            if (type == typeof(bool)) return DataType.Boolean;
            if (type == typeof(int)) return DataType.Int32;
            if (type == typeof(long)) return DataType.Int64;
            if (type == typeof(double)) return DataType.Double;
            if (type == typeof(DateTimeOffset)) return DataType.DateTimeOffset;
            if (type == typeof(GeographyPoint)) return DataType.GeographyPoint;
            if (typeof(IEnumerable<string>).IsAssignableFrom(type)) return DataType.Collection(DataType.String);

            throw new ArgumentException(string.Format("Unsupported type: '{0}'", type), "type");
        }

    }
}
