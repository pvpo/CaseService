using System;
using Newtonsoft.Json;

namespace CaseService.Services.Domain {
    public class Slide: BaseDomainObject {

        public static readonly string collectionName = "Slide";
        public Slide() : base(collectionName) { }

        [JsonProperty(PropertyName = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "SlideId")]
        public string SlideId { get; set; }

        [JsonProperty(PropertyName = "ProtocolName")]
        public string ProtocolName { get; set; }

        [JsonProperty(PropertyName = "ProtocolDescription")]
        public string ProtocolDescription { get; set; }

    }
}