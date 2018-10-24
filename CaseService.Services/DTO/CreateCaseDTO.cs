using System.Collections.Generic;
using CaseService.Services.Domain;

namespace CaseService.Services.DTO
{
    public class CreateCaseDTO {
        public string Type { get; set; }
        public CreatePatientDTO Patient { get; set; }
        public CreateRequestorDTO Requestor { get; set; }
        public List<CreateSpecimenDTO> Specimens { get; set; }
    }
}