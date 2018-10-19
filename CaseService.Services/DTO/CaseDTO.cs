using System.Collections.Generic;

namespace CaseService.Services.DTO
{
    public class CaseDTO {

        public string Id { get; set; }
        public PatientDTO Patient { get; set; }
        public RequestorDTO Requestor { get; set; }
        public List<SpecimenDTO> Specimens { get; set; }

    }
}