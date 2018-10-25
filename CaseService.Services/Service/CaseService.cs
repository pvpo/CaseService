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
                sp.CreatedOn = DateTime.Now;

                Document doc = specimenRepository.Save(sp);
                specimenIds.Add(specimenFactory.create(doc).Id);
            }

            Requestor r = requestorFactory.create(dto.Requestor);
            r.CreatedOn = DateTime.Now;
            Document reqDoc = requestorRepository.Save(r);
            Requestor requestor = requestorFactory.create(reqDoc);


            Patient p = patientFactory.create(dto.Patient);
            p.CreatedOn = DateTime.Now;

            p.PatientId = patientRepository.GetCountAsync() + 1;
            Patient patient = patientFactory.create(patientRepository.Save(p));

            result.Specimens = specimenIds;
            result.RequestorId = requestor.Id;
            result.PatientId = patient.Id;
            result.Type = dto.Type;
            result.CaseId = CaseType.GetCode(result.Type) + caseRepository.GetCountByTypeAsync(result.Type).ToString().PadLeft(5, '0');
            result.SetPropertyValue("CreatedOn", DateTime.Now);
            result.Status = CaseStatus.Open;

            caseRepository.Save(result);

            List<Specimen> specimens = specimenRepository.ListAsync(specimenIds).Result;

            int specimenCount = 1;
            foreach(Specimen sp in specimens) {
                sp.SetPropertyValue("SpecimenId", result.CaseId + "-" + specimenCount.ToString());
                specimenRepository.FullUpdate(sp);
                specimenCount++;
            }

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
            caseDTO.Type = c.Type;
            caseDTO.CaseId = c.CaseId;
            caseDTO.CreatedOn = c.CreatedOn;
            caseDTO.Status = c.Status;

            DateTime closedOn = c.ClosedOn;
            if(c.ClosedOn == null || c.ClosedOn.Equals(new DateTime())) {
                closedOn = DateTime.Now;
            }


            TimeSpan span = (closedOn - c.CreatedOn);

            caseDTO.OpenTime = String.Format("{0} days, {1} hours, {2} minutes, {3} seconds", 
                span.Days, span.Hours, span.Minutes, span.Seconds);


            try {
                if(c.RequestorId != null && c.RequestorId != "") {
                    RequestorDTO r = requestorFactory.createDTO(requestorRepository.findByIdAsync(c.RequestorId).Result);
                    caseDTO.Requestor = requestorFactory.createDTO(requestorRepository.findByIdAsync(c.RequestorId).Result);
                }
            } catch(DocumentClientException e) {
                Console.WriteLine(e.Message);
            } catch (AggregateException e) {   
                Console.WriteLine(e.Message);
            }

            try {
                if(c.PatientId != null && c.PatientId != "") {
                    caseDTO.Patient = patientFactory.createDTO(patientRepository.findByIdAsync(c.PatientId).Result);
                }
            } catch(DocumentClientException e) {
                Console.WriteLine(e.Message);
            } catch (AggregateException e) {   
                Console.WriteLine(e.Message);
            }


            try {
                if(c.Specimens != null) {
                    caseDTO.Specimens = specimenFactory.create(specimenRepository.ListAsync(c.Specimens).Result);
                }
            } catch(DocumentClientException e) {
                Console.WriteLine(e.Message);
            } catch (AggregateException e) {   
                Console.WriteLine(e.Message);
            }

            return caseDTO;
        }

        public CaseDTO GetById(string id) {
            return GetCaseDTO(caseFactory.create(caseRepository.findByIdAsync(id).Result));
        }

        public void DeleteById(string id) {
            Case c = caseFactory.create(caseRepository.findByIdAsync(id).Result);

            requestorRepository.DeleteById(c.RequestorId);
            patientRepository.DeleteById(c.PatientId);

            foreach(string sID in c.Specimens) {
                specimenRepository.DeleteById(sID);
            }

            caseRepository.DeleteById(id);
        }

        public Case Close(string id) {
            Case c = caseFactory.create(caseRepository.findByIdAsync(id).Result);
            c.Status = CaseStatus.Closed;
            return caseFactory.create(caseRepository.FullUpdate(c));
        }

        public int GetTodaysClosedCases() {
                    DateTime now = DateTime.Now;

                DateTime start = new DateTime(
                    now.Year,
                    now.Month,
                    now.Day,
                    0,
                    0,
                    0,
                    0,
                    now.Kind
                );

                DateTime end = new DateTime(
                    now.Year, 
                    now.Month,
                    now.Day,
                    23,
                    0,
                    0,
                    0,
                    now.Kind
                );


            return caseRepository.GetClosedCountBetweenDates(start, end);
        }

        public int[] GetDailyClosedCountChartData() {
            int[] result = new int[24];

            DateTime now = DateTime.Now;

            for(int i = 0; i <= 22; i++) {
                DateTime start = new DateTime(
                    now.Year,
                    now.Month,
                    now.Day,
                    i,
                    0,
                    0,
                    0,
                    now.Kind
                );

                DateTime end = new DateTime(
                    now.Year, 
                    now.Month,
                    now.Day,
                    i + 1,
                    0,
                    0,
                    0,
                    now.Kind
                );

                result[i] = caseRepository.GetClosedCountBetweenDates(start, end);
            }

            return result;
        }

    }
}