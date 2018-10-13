using System.Collections.Generic;

namespace other_case_service.DTO
{
    public class CaseDTO {

        public PatientDTO Patient { get; set; }
        public RequestorDTO Requestor { get; set; }
        public List<SpecimenDTO> Specimens { get; set; }

    }
}