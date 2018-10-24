using System;
using Newtonsoft.Json;

namespace CaseService.Services.DTO
{
    public class PatientDTO {

        [JsonProperty(PropertyName = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "PatientId")]
        public int PatientId { get; set; }

        [JsonProperty(PropertyName = "Gender")]
        public string Gender { get; set; }
    }
}