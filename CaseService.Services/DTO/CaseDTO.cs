using System.Collections.Generic;
using Newtonsoft.Json;

namespace CaseService.Services.DTO
{
    public class CaseDTO {

        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Patient")]
        public PatientDTO Patient { get; set; }

        [JsonProperty(PropertyName = "Requestor")]
        public RequestorDTO Requestor { get; set; }

        [JsonProperty(PropertyName = "Specimens")]
        public List<SpecimenDTO> Specimens { get; set; }

    }
}