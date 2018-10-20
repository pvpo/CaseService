using CaseService.Services.Abstract.Data;
using CaseService.Services.Domain;

namespace CaseService.Services.Data.Repository {
    public class RequestorRepository : BaseDomainRepository<Requestor, RequestorRepository>
    {
        protected override string GetCollectionName()
        {
            return Requestor.collectionName;
        }
    }
}