using CaseService.Services.Domain;
using Microsoft.Azure.Documents;
using System.Threading.Tasks;

namespace CaseService.Services.Data {
    public class BaseDomainRepository<T> where T : BaseDomainObject {

        public Document Save(T entity) {

            Document document =  DBInitializer.documentClient.CreateDocumentAsync(
                DBInitializer.collections[entity.GetCollectionName()].DocumentsLink, entity).Result;

            return document;
        }
    }
}