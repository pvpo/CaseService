using CaseService.Services.Data;
using CaseService.Services.Domain;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseService.Services.Abstract.Data {
    public abstract class BaseDomainRepository<T, T1> : SingletonBase<T1> where T : BaseDomainObject where T1 : class {

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
        public async Task<Document> findByIdAsync(string id) {

            var docLink = "dbs/" + Configuration.Persistence.dbName + "/colls/" + GetCollectionName() + "/docs/" + id;

            Document doc = await DBInitializer.documentClient.ReadDocumentAsync<T>(docLink);

            return doc;
        }

        public async Task<List<T>> ListAsync(List<string> ids) {

            DocumentClient _client = DBInitializer.documentClient;
            DocumentCollection col = DBInitializer.collections[GetCollectionName()];

            var feedOptions = new FeedOptions() { MaxItemCount = -1 };

            string queryStr = "SELECT * FROM " + GetCollectionName() + " WHERE " + GetCollectionName() + ".id IN (\"" + String.Join("\", \"", ids.ToArray()) + "\")";
            var query = _client.CreateDocumentQuery<T>(col.DocumentsLink, queryStr, feedOptions);
            var queryAll = query.AsDocumentQuery();
        
            // combine the results
            var results = new List<T>();
            while (queryAll.HasMoreResults) {
                results.AddRange(await queryAll.ExecuteNextAsync<T>());
            }
        
            Console.WriteLine("!!!!!!!!!!!!!!");
            Console.WriteLine(JsonConvert.SerializeObject(results));

            return results;

        }

        protected abstract string GetCollectionName();
    }
}