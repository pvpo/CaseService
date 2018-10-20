using System;
using System.Collections.Generic;
using CaseService.Services.Domain;
using CaseService.Services.DTO;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace CaseService.Services.Factory {
    public class CaseFactory : SingletonBase<CaseFactory> {

        private readonly PatientFactory patientFactory = PatientFactory.Instance;
        private readonly RequestorFactory requestorFactory = RequestorFactory.Instance;
        private readonly SpecimenFactory specimenFactory = SpecimenFactory.Instance;

        public Case create(Document doc) {
            Case result = JsonConvert.DeserializeObject<Case>(JsonConvert.SerializeObject(doc));


            // result.Id = doc.GetPropertyValue<string>("id");
            // result.PatientId = doc.GetPropertyValue<string>("PatientId");
            // result.RequestorId = doc.GetPropertyValue<string>("RequestorId");
            // result.Specimens = doc.GetPropertyValue<List<string>>("Specimens");

            // Console.WriteLine(doc.GetPropertyValue<string>("id"));
            // Console.WriteLine(doc.GetPropertyValue<string>("RequestorId"));

            return result;
        }

    }
}