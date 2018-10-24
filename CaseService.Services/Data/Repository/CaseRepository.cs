using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseService.Services.Abstract.Data;
using CaseService.Services.Domain;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace CaseService.Services.Data {
    public class CaseRepository : BaseDomainRepository<Case, CaseRepository> {
        public int GetCountByTypeAsync(string type) {
            DocumentCollection collection = DBInitializer.collections[GetCollectionName()];
            DocumentClient client = DBInitializer.documentClient;
            Uri collectionURI = UriFactory.CreateDocumentCollectionUri(Configuration.Persistence.dbName, GetCollectionName());
            int count = client.CreateDocumentQuery<Case>(collectionURI, new FeedOptions {
                EnableCrossPartitionQuery = true 
                // AsEnumerable used because a bug in older version of sdk, it does not take into account multiple pages.
            }).Where(f => f.Type == type).AsEnumerable().Count();

            return count;
        }

        public int GetClosedCountBetweenDates(DateTime start, DateTime end) {
            DocumentCollection collection = DBInitializer.collections[GetCollectionName()];
            DocumentClient client = DBInitializer.documentClient;
            Uri collectionURI = UriFactory.CreateDocumentCollectionUri(Configuration.Persistence.dbName, GetCollectionName());
            int count = client.CreateDocumentQuery<Case>(collectionURI, new FeedOptions {
                EnableCrossPartitionQuery = true 
                // AsEnumerable used because a bug in older version of sdk, it does not take into account multiple pages.
            }).Where(f => f.ClosedOn >= start && f.ClosedOn <= end).AsEnumerable().Count();

            return count;
        }
        protected override string GetCollectionName() {
            return Case.collectionName;
        }
    }
}