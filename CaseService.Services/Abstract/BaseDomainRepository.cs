using CaseService.Services.Data;
using CaseService.Services.Domain;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseService.Services.Abstract.Data {
    public abstract class BaseDomainRepository<T, T1> : SingletonBase<T1> where T : BaseDomainObject where T1 : class {

        public Document Save(T entity) {
            DocumentClient client = DBInitializer.documentClient;
            DocumentCollection collection = DBInitializer.collections[GetCollectionName()];
            Document document =  client.CreateDocumentAsync(
               collection.DocumentsLink, entity).Result;

            return document;
        }


        public async Task<List<T>> ListAllAsync() {
            DocumentCollection collection = DBInitializer.collections[GetCollectionName()];
            DocumentClient client = DBInitializer.documentClient;
            // build the query
            var feedOptions = new FeedOptions() { MaxItemCount = -1 };

            var query = client.CreateDocumentQuery<T>(collection.DocumentsLink, "SELECT * FROM " + GetCollectionName(), feedOptions);
            var queryAll = query.AsDocumentQuery();
        
            // combine the results
            var results = new List<T>();
            while (queryAll.HasMoreResults) {
                results.AddRange(await queryAll.ExecuteNextAsync<T>());
            }
        
            return results;
        }
        public async Task<Document> findByIdAsync(string id) {
            DocumentClient client = DBInitializer.documentClient;
            var docLink = "dbs/" + Configuration.Persistence.dbName + "/colls/" + GetCollectionName() + "/docs/" + id;

            Document doc = await client.ReadDocumentAsync<T>(docLink);

            return doc;
        }

        public async Task<List<T>> ListAsync(List<string> ids) {
            DocumentClient client = DBInitializer.documentClient;
            DocumentCollection collection = DBInitializer.collections[GetCollectionName()];

            var feedOptions = new FeedOptions() { MaxItemCount = -1 };

            string queryStr = "SELECT * FROM " + GetCollectionName() + " WHERE " + GetCollectionName() + ".id IN (\"" + String.Join("\", \"", ids.ToArray()) + "\")";
            var query = client.CreateDocumentQuery<T>(collection.DocumentsLink, queryStr, feedOptions);
            var queryAll = query.AsDocumentQuery();
        
            // combine the results
            var results = new List<T>();
            while (queryAll.HasMoreResults) {
                results.AddRange(await queryAll.ExecuteNextAsync<T>());
            }
        
            return results;
        }

        public void DeleteById(string id) {
            DocumentClient client = DBInitializer.documentClient;
            var docLink = "dbs/" + Configuration.Persistence.dbName + "/colls/" + GetCollectionName() + "/docs/" + id;
            Console.WriteLine("Deleting from: " + GetCollectionName() + " ID: " + id);
            client.DeleteDocumentAsync(docLink);
        }

        public Document FullUpdate(T entity) {
            DocumentClient client = DBInitializer.documentClient;

            var document = client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(Configuration.Persistence.dbName, GetCollectionName(), entity.Id), entity).Result.Resource;

            return document;
        }

        public int GetCountAsync() {
            DocumentCollection collection = DBInitializer.collections[GetCollectionName()];
            DocumentClient client = DBInitializer.documentClient;
            Uri collectionURI = UriFactory.CreateDocumentCollectionUri(Configuration.Persistence.dbName, GetCollectionName());
            int count = client.CreateDocumentQuery<T>(collectionURI, new FeedOptions {
                EnableCrossPartitionQuery = true 
                // AsEnumerable used because a bug in older version of sdk, it does not take into account multiple pages.
            }).AsEnumerable().Count();

            return count;
        }

        protected abstract string GetCollectionName();
    }
}