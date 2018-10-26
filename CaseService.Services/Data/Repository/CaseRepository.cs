using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseService.Services.Abstract.Data;
using CaseService.Services.Domain;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;

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

            IQueryable<int> result = client.CreateDocumentQuery<int>(collectionURI,
                                        string.Format("SELECT VALUE COUNT(1) FROM c WHERE c.Status = 'Closed' AND (c.ClosedOn >= '{0}' AND c.ClosedOn <= '{1}')", start.ToString("o"), end.ToString("o")), new FeedOptions { EnableCrossPartitionQuery = true });
            
                
            return result.ToList().ElementAt(0);

        }
        protected override string GetCollectionName() {
            return Case.collectionName;
        }
    }
}