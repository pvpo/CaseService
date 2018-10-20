using CaseService.Services.Abstract.Data;
using CaseService.Services.Domain;

namespace CaseService.Services.Data.Repository {
    public class PatientRepository : BaseDomainRepository<Patient, PatientRepository>
    {
        protected override string GetCollectionName()
        {
            return Patient.collectionName;
        }
    }
}