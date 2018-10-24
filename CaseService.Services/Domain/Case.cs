using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CaseService.Services.Domain {
    public class Case : BaseDomainObject {

        //By some reason when using just "Case" as collection name cosmosDB starts behaving unexpectedly
        public static readonly string collectionName = "Casess";
        public Case() : base(collectionName) { }

        [JsonProperty(PropertyName = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "CaseId")]
        public string CaseId {get; set;}

        [JsonProperty(PropertyName = "RequestorId")]
        public string RequestorId { get; set; }

        [JsonProperty(PropertyName = "PatientId")]
        public string PatientId { get; set; }

        [JsonProperty(PropertyName = "Specimens")]
        public List<string> Specimens { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "ClosedOn")]
        public DateTime ClosedOn { get; set; }
    }
}