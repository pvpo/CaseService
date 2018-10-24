using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CaseService.Services.DTO
{
    public class CaseDTO {

        [JsonProperty(PropertyName = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "CaseId")]
        public string CaseId { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "Patient")]
        public PatientDTO Patient { get; set; }

        [JsonProperty(PropertyName = "Requestor")]
        public RequestorDTO Requestor { get; set; }

        [JsonProperty(PropertyName = "Specimens")]
        public List<SpecimenDTO> Specimens { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "ClosedOn")]
        public DateTime ClosedOn { get; set; }

        [JsonProperty(PropertyName = "OpenTime")]
        public string OpenTime { get; set; }

    }
}