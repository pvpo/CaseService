using System.Collections.Generic;
using System.Threading.Tasks;
using CaseService.Services.Data;
using CaseService.Services.Data.Repository;
using CaseService.Services.Domain;
using CaseService.Services.DTO;
using CaseService.Services.Factory;
using Microsoft.Azure.Documents;

namespace CaseService.Services.Service {
    public class PatientService {

        private readonly PatientRepository patientRepository;
        private readonly PatientFactory patientFactory;

        public PatientService() {
            patientRepository = PatientRepository.Instance;
            patientFactory = PatientFactory.Instance;
        }

        public Task<List<Patient>> ListAllAsync() {
            return patientRepository.ListAllAsync();
        }

    }
}