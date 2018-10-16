using System.Collections.Generic;
using System.Threading.Tasks;
using CaseService.Services.Domain;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace CaseService.Services.Data.Repository {
    public class SpecimenRepository : BaseDomainRepository<Specimen>
    {
        protected override string GetCollectionName()
        {
           return Specimen.collectionName;
        }
    }


}