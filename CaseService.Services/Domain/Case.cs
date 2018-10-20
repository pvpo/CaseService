using System.Collections.Generic;
using Newtonsoft.Json;

namespace CaseService.Services.Domain {
    public class Case : BaseDomainObject {

        //By some reason when using just "Case" as collection name cosmosDB starts behaving unexpectedly
        public static readonly string collectionName = "Casess";
        public Case() : base(collectionName) { }


        [JsonProperty(PropertyName = "RequestorId")]
        public string RequestorId { get; set; }

        [JsonProperty(PropertyName = "PatientId")]
        public string PatientId { get; set; }

        [JsonProperty(PropertyName = "Specimens")]
        public List<string> Specimens { get; set; }
    }
}