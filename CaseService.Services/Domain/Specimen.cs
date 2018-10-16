using Newtonsoft.Json;

namespace CaseService.Services.Domain
{
    public class Specimen : BaseDomainObject {
        public static readonly string collectionName = "Specimen";
        public Specimen() : base(collectionName) { }

        [JsonProperty(PropertyName = "SpecimenId")]
        public string SpecimenId { get; set; }

        [JsonProperty(PropertyName = "BlockId")]
        public long BlockId { get; set; }

        [JsonProperty(PropertyName = "SlideId")]
        public long SlideId { get; set; }

        [JsonProperty(PropertyName = "ProtocolNumber")]
        public long ProtocolNumber { get; set; }

        [JsonProperty(PropertyName = "ProtocolName")]
        public string ProtocolName { get; set; }

        [JsonProperty(PropertyName = "ProtocolDescription")]
        public string ProtocolDescription { get; set; }

    }
}