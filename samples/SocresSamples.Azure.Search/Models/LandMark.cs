using Microsoft.Azure.Search.Models;
using Microsoft.Spatial;
using Socres.Azure.Search.Attributes;

namespace SocresSamples.Azure.Search.Models
{
    [SerializePropertyNamesAsCamelCase]
public class LandMark
{
    [SearchIndex(IsKey = true, IsRetrievable = true)]
    public string Id { get; set; }

    [SearchIndex(IsRetrievable = true, IsSearchable = true)]
    public string Name { get; set; }

    [SearchIndex(IsRetrievable = true, IsFacetable = true)]
    public string Continent { get; set; }

    [SearchIndex(IsRetrievable = true, IsSearchable = true)]
    public string Country { get; set; }

    [SearchIndex(IsRetrievable = true)]
    public string ImageUrl { get; set; }

    [SearchIndex(IsRetrievable = true, IsSearchable = true)]
    public GeographyPoint Location { get; set; }
}
}