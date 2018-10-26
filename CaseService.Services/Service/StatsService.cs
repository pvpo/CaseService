using System;
using System.Collections.Generic;
using CaseService.Services.Data;
using CaseService.Services.Data.Repository;

namespace CaseService.Services.Service {

    public class StatsService {

        private readonly CaseRepository caseRepository;
        private readonly SpecimenRepository specimenRepository;
        private readonly SlideRepository slideRepository;
        private readonly RequestorRepository requestorRepository;
        private readonly PatientRepository patientRepository;

        public StatsService () {
            caseRepository = CaseRepository.Instance;
            specimenRepository = SpecimenRepository.Instance;
            slideRepository = SlideRepository.Instance;
            requestorRepository = RequestorRepository.Instance;
            patientRepository = PatientRepository.Instance;
        } 

        public List<int[]> GetDailyClosedCountChartData() {
            List<int[]> result = new List<int[]>();

            DateTime now = DateTime.Now;

            for(int i = 0; i <= 22; i++) {
                int[] res = new int[2];
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


                res[0] = (Int32)(start.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                res[1] = caseRepository.GetClosedCountBetweenDates(start, end);
                result.Add(res);
            }

            return result;
        }

        public List<List<string>> getDBStats() {

            List<string> cases = new List<string>();
            cases.Add("Caes");
            cases.Add(caseRepository.GetCountAsync().ToString());

            List<string> patients = new List<string>();
            patients.Add("Patients");
            patients.Add(patientRepository.GetCountAsync().ToString());
            List<string> specimens = new List<string>();
            specimens.Add("Specimens");
            specimens.Add(specimenRepository.GetCountAsync().ToString());
            List<string> slides = new List<string>();
            slides.Add("Slides");
            slides.Add(slideRepository.GetCountAsync().ToString());
            List<string> requestors = new List<string>();
            requestors.Add("Requestors");
            requestors.Add(requestorRepository.GetCountAsync().ToString());

            List<List<string>> result = new List<List<string>>();

            result.Add(cases);
            result.Add(patients);
            result.Add(specimens);
            result.Add(slides);
            result.Add(requestors);

            return result;
        }
    }
}