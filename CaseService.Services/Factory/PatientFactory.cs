using System;
using CaseService.Services.Domain;
using CaseService.Services.DTO;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace CaseService.Services.Factory {
    public class PatientFactory : SingletonBase<PatientFactory> {

        public Patient create(CreatePatientDTO dto) {
            Patient result = JsonConvert.DeserializeObject<Patient>(JsonConvert.SerializeObject(dto));

            return result;
        }

        public Patient create(Document doc) {
            Patient result = JsonConvert.DeserializeObject<Patient>(JsonConvert.SerializeObject(doc));

            return result;
        }

        public PatientDTO createDTO(Document doc) {
            PatientDTO result = JsonConvert.DeserializeObject<PatientDTO>(JsonConvert.SerializeObject(doc));

            return result;
        }

        public PatientDTO createDTO(Patient patient) {
            PatientDTO result = JsonConvert.DeserializeObject<PatientDTO>(JsonConvert.SerializeObject(patient));
            return result;
        }
    }
}