using CaseService.Services.Domain;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseService.Services.Data {
    public abstract class BaseDomainRepository<T> where T : BaseDomainObject {

        public Document Save(T entity) {

            Document document =  DBInitializer.documentClient.CreateDocumentAsync(
                DBInitializer.collections[GetCollectionName()].DocumentsLink, entity).Result;

            return document;
        }


        public async Task<List<T>> ListAllAsync()
        {
        
            DocumentClient _client = DBInitializer.documentClient;
            DocumentCollection col = DBInitializer.collections[GetCollectionName()];

            // build the query
            var feedOptions = new FeedOptions() { MaxItemCount = -1 };
            var query = _client.CreateDocumentQuery<T>(col.DocumentsLink, "SELECT * FROM " + GetCollectionName(), feedOptions);
            var queryAll = query.AsDocumentQuery();
        
            // combine the results
            var results = new List<T>();
            while (queryAll.HasMoreResults) {
                results.AddRange(await queryAll.ExecuteNextAsync<T>());
            }
        
            return results;
        }

        protected abstract string GetCollectionName();
    }
}