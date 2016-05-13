using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web.Hosting;
using Socres.Azure.Search.Factories;
using SocresSamples.Azure.Search.Customizations;
using SocresSamples.Azure.Search.Managers;
using SocresSamples.Azure.Search.Models;

namespace SocresSamples.Azure.Search
{
    /// <summary>
    /// This class is responible for ensuring that the Search Index is created and filled with sample data.
    /// </summary>
    public class AzureSearchConfig
    {
        public static async Task EnsureSearchIndex()
        {
            var searchIndexName = ConfigurationManager.AppSettings["searchIndexName"];

            var searchIndexManager = new SearchManager<LandMark>(
                new IndexFactory(new FieldFactory()),
                ConfigurationManager.AppSettings["searchUrl"],
                ConfigurationManager.AppSettings["searchApiKey"]);

            if (await searchIndexManager.IndexExistsAsync(searchIndexName))
            {
                await searchIndexManager.DeleteIndexAsync(searchIndexName);
            }

            // Create the index
            await searchIndexManager.CreateOrUpdateIndexAsync(searchIndexName);

            // Fill index with sample data
            var landMarks = ReadSampleData();

            // Add wonders to the search index
            await searchIndexManager.UploadDocuments(searchIndexName, landMarks);
        }

        public static IEnumerable<LandMark> ReadSampleData()
        {
            var sampleDataFile = HostingEnvironment.MapPath("~/App_Data/sampleData.json");
            var json = File.ReadAllText(sampleDataFile);

            var worldWonders = json.ToObject<IEnumerable<LandMark>>(new GeographyPointConverter());
            return worldWonders;
        }
    }
}