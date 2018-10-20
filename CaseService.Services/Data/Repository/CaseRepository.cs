using CaseService.Services.Abstract.Data;
using CaseService.Services.Domain;

namespace CaseService.Services.Data {
    public class CaseRepository : BaseDomainRepository<Case, CaseRepository>
    {
        protected override string GetCollectionName()
        {
            return Case.collectionName;
        }
    }
}