using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CaseService.Services.Data {

    public class DBInitializer
    {
        public static DocumentClient documentClient { get; private set; }
        public static Database db { get; private set; }
        public static ConcurrentDictionary<string, DocumentCollection> collections = new ConcurrentDictionary<string, DocumentCollection>();

        public static void Init()
        {
            
            documentClient = new DocumentClient(new Uri(Configuration.Persistence.endpointURI),
            Configuration.Persistence.authKey);
            db = CreateDatabaseAsync().Result;
            Console.WriteLine(db.ToString());

        }

        private static async Task<Database> CreateDatabaseAsync()
        {
            Database database = documentClient.CreateDatabaseQuery().Where(c => c.Id == Configuration.Persistence.dbName).AsEnumerable().FirstOrDefault();
            if (database == null)
            {
                database = await documentClient.CreateDatabaseAsync(new Database()
                {
                    Id = Configuration.Persistence.dbName
                });
            }
            return database;
        }

        public static void RegisterDocumentCollection(String collectionName) {
            DocumentCollection collection = CreateDocumentCollection(collectionName).Result;
            collections.TryAdd(collectionName, collection);
        }

        private static async Task<DocumentCollection> CreateDocumentCollection(String collectionName) {
            if(documentClient == null) {
                Task.Delay(2000).Wait();
            }
            Console.WriteLine("Creating collection with name: " + collectionName);
            DocumentCollection documentCollection = documentClient.CreateDocumentCollectionQuery(db.CollectionsLink).Where(c => c.Id == collectionName).AsEnumerable().FirstOrDefault();

            if (documentCollection == null) {
                documentCollection = await documentClient.CreateDocumentCollectionAsync(db.SelfLink, new DocumentCollection() {
                    Id = collectionName
                });
            }
            
            return documentCollection;
        }

        private static async Task<DocumentCollection> CreateDocumentCollection(String collectionName, IndexingPolicy indexingPolicy) {
            if(documentClient == null) {
                Task.Delay(2000).Wait();
            }
            Console.WriteLine("Creating collection with name: " + collectionName);
            
            DocumentCollection documentCollection = documentClient.CreateDocumentCollectionQuery(db.CollectionsLink).Where(c => c.Id == collectionName).AsEnumerable().FirstOrDefault();

            if (documentCollection == null) {

                documentCollection = new DocumentCollection { Id = collectionName };
                documentCollection.IndexingPolicy = indexingPolicy;

                documentCollection = await documentClient.CreateDocumentCollectionAsync(db.SelfLink, documentCollection);
            }
            
            return documentCollection;
        }
    }

}