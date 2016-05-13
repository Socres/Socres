using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Socres.Azure.Search.Factories;

namespace SocresSamples.Azure.Search.Managers
{
    public class SearchManager<T> where T: class
    {
        private readonly IIndexFactory _indexFactory;
        private readonly SearchServiceClient _searchServiceClient;

        public SearchManager(IIndexFactory indexFactory, string serviceUrl, string apiKey)
        {
            _indexFactory = indexFactory;
            _searchServiceClient = new SearchServiceClient(new Uri(serviceUrl), new SearchCredentials(apiKey));
        }

        public async Task<bool> IndexExistsAsync(string indexName)
        {
            return await _searchServiceClient.Indexes.ExistsAsync(indexName);
        }

        public SearchIndexClient GetSearchClient(string indexName)
        {
            return _searchServiceClient.Indexes.GetClient(indexName);
        }

        public async Task CreateOrUpdateIndexAsync(string indexName)
        {
            var index = _indexFactory.CreateIndexFrom<T>(indexName);

            await _searchServiceClient.Indexes.CreateOrUpdateAsync(index);
        }

        public async Task UploadDocuments(string indexName, IEnumerable<T> documents)
        {
            var client = GetSearchClient(indexName);
            var batch = IndexBatch.Upload(documents);
            
            await client.Documents.IndexAsync(batch);
        }

        public async Task DeleteIndexAsync(string indexName)
        {
            await _searchServiceClient.Indexes.DeleteAsync(indexName);
        }

    }
}