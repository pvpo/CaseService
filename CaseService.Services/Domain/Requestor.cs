using System;
using Newtonsoft.Json;

namespace CaseService.Services.Domain {
    public class Requestor : BaseDomainObject {

        public static readonly string collectionName = "Requestor";
        public Requestor() : base(collectionName) {  }

        [JsonProperty(PropertyName = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "Mobile")]
        public string Mobile { get; set; }
    }
}