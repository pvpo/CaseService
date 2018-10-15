using Newtonsoft.Json;

namespace CaseService.Services.Domain
{
    public class Specimen : BaseDomainObject {
        public static readonly string collectionName = "Specimen";
        public Specimen() : base(collectionName) { }

        [JsonProperty(PropertyName = "SpecimenId")]
        public string SpecimenId { get; set; }
        public long BlockId { get; set; }
        public long SlideId { get; set; }
        public long ProtocolNumber { get; set; }
        public string ProtocolName { get; set; }
        public string ProtocolDescription { get; set; }

    }
}