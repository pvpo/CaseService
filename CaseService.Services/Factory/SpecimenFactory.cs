using System;
using System.Collections.Generic;
using CaseService.Services.Domain;
using CaseService.Services.DTO;
using Microsoft.Azure.Documents;

namespace CaseService.Services.Factory {
    public class SpecimenFactory : SingletonBase<SpecimenFactory> {

        private SpecimenFactory () { }

        public Specimen create(CreateSpecimenDTO dto) {
            Specimen result = new Specimen();
            
            result.SpecimenId = dto.SpecimenId;
            result.BlockId = dto.BlockId;
            result.SlideId = dto.SlideId;
            result.ProtocolNumber = dto.ProtocolNumber;
            result.ProtocolName = dto.ProtocolName;
            result.ProtocolDescription = dto.ProtocolDescription;

            return result;
        }

        public Specimen create(Document doc) {
            Specimen result = new Specimen();

            result.SpecimenId = doc.GetPropertyValue<string>("SpecimenId");
            result.BlockId = doc.GetPropertyValue<long>("BlockId");
            result.SlideId = doc.GetPropertyValue<long>("SlideId");
            result.ProtocolNumber = doc.GetPropertyValue<long>("ProtocolNumber");
            result.ProtocolName = doc.GetPropertyValue<string>("ProtocolName");
            result.ProtocolDescription = doc.GetPropertyValue<string>("ProtocolDescription");
            result.Id = doc.GetPropertyValue<string>("id");

            return result;
        }

        public SpecimenDTO createDTO(Document doc) {
            SpecimenDTO result = new SpecimenDTO();

            result.SpecimenId = doc.GetPropertyValue<string>("SpecimenId");
            result.BlockId = doc.GetPropertyValue<long>("BlockId");
            result.SlideId = doc.GetPropertyValue<long>("SlideId");
            result.ProtocolNumber = doc.GetPropertyValue<long>("ProtocolNumber");
            result.ProtocolName = doc.GetPropertyValue<string>("ProtocolName");
            result.ProtocolDescription = doc.GetPropertyValue<string>("ProtocolDescription");
            result.Id = doc.GetPropertyValue<string>("id");

            return result;
        }

        public List<SpecimenDTO> create(List<Specimen> specimens) {
            List<SpecimenDTO> result = new List<SpecimenDTO>();

            foreach(Specimen sp in specimens) {
                result.Add(createDTO(sp));
            }

            return result;
        }

    }
}