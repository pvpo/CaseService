using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaseService.Services.Data;
using CaseService.Services.Data.Repository;
using CaseService.Services.Domain;
using CaseService.Services.DTO;
using CaseService.Services.Factory;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace CaseService.Services.Service {
    public class CaseService {

        private readonly SpecimenRepository specimenRepository;
        private readonly SpecimenFactory specimenFactory;
        private readonly RequestorRepository requestorRepository;
        private readonly RequestorFactory requestorFactory;
        private readonly PatientRepository patientRepository;
        private readonly PatientFactory patientFactory;
        private readonly CaseRepository caseRepository;
        private readonly CaseFactory caseFactory;

        public CaseService() {
            specimenRepository = SpecimenRepository.Instance;
            specimenFactory = SpecimenFactory.Instance;
            requestorRepository = RequestorRepository.Instance;
            requestorFactory = RequestorFactory.Instance;
            patientRepository = PatientRepository.Instance;
            patientFactory = PatientFactory.Instance;
            caseRepository = CaseRepository.Instance;
            caseFactory = CaseFactory.Instance;
        }


        public Case createAndPersistAsync(CreateCaseDTO dto) {
            Case result = new Case();

            List<string> specimenIds = new List<string>();

            foreach(CreateSpecimenDTO spDTO in dto.Specimens) {
                Specimen sp = specimenFactory.create(spDTO);
                Document doc = specimenRepository.Save(sp);
                specimenIds.Add(specimenFactory.create(doc).Id);
            }

            Document reqDoc = requestorRepository.Save(requestorFactory.create(dto.Requestor));
            Requestor requestor = requestorFactory.create(reqDoc);
            Patient patient = patientFactory.create(patientRepository.Save(patientFactory.create(dto.Patient)));

            result.Specimens = specimenIds;
            result.RequestorId = requestor.Id;
            result.PatientId = patient.Id;

            caseRepository.Save(result);

            return result;
        }

        public Task<List<Case>> ListAllAsync() {
            return caseRepository.ListAllAsync();
        }

        public List<CaseDTO> ListAllDTOAsync() {
            Task<List<Case>> cases = caseRepository.ListAllAsync();

            cases.Wait();
            List<CaseDTO> result = new List<CaseDTO>();

            foreach(Case c in cases.Result) {

                result.Add(GetCaseDTO(c));
            }

            return result;
        }

        private CaseDTO GetCaseDTO(Case c) {
            CaseDTO caseDTO = new CaseDTO();

            caseDTO.Id = c.Id;

            if(c.RequestorId != null && c.RequestorId != "") {
                RequestorDTO r = requestorFactory.createDTO(requestorRepository.findByIdAsync(c.RequestorId).Result);
                caseDTO.Requestor = requestorFactory.createDTO(requestorRepository.findByIdAsync(c.RequestorId).Result);
            }

            if(c.PatientId != null && c.PatientId != "") {
                caseDTO.Patient = patientFactory.createDTO(patientRepository.findByIdAsync(c.PatientId).Result);
            }

            if(c.Specimens != null) {
                caseDTO.Specimens = specimenFactory.create(specimenRepository.ListAsync(c.Specimens).Result);
            }

            return caseDTO;
        }

        public CaseDTO GetById(string id) {
            return GetCaseDTO(caseFactory.create(caseRepository.findByIdAsync(id).Result));
        }

    }
}