using CaseService.Services.Domain;
using CaseService.Services.DTO;
using Microsoft.Azure.Documents;

namespace CaseService.Services.Factory {
    public class SpecimenFactory {

        public Specimen create(SpecimenDTO dto) {
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

            return result;
        }

    }
}