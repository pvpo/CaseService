using System;
using Newtonsoft.Json;

namespace CaseService.Services.Domain {
    public class Patient : BaseDomainObject {

        public static readonly string collectionName = "Patient";

        public Patient() : base(collectionName) {  }

        [JsonProperty(PropertyName = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

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