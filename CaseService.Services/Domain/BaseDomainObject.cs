using Microsoft.Azure.Documents;

namespace CaseService.Services.Domain {
    public class BaseDomainObject: Document {

        private static string cName;
        public BaseDomainObject(string collectionName) => cName = collectionName;

        public string GetCollectionName() => cName;

    }
}