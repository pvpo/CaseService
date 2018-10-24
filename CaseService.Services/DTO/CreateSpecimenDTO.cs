using Newtonsoft.Json;

namespace CaseService.Services.DTO
{
    public class CreateSpecimenDTO {

        [JsonProperty(PropertyName = "BlockId")]
        public long BlockId { get; set; }

        [JsonProperty(PropertyName = "ProtocolNumber")]
        public long ProtocolNumber { get; set; }

        [JsonProperty(PropertyName = "ProtocolName")]
        public string ProtocolName { get; set; }

        [JsonProperty(PropertyName = "ProtocolDescription")]
        public string ProtocolDescription { get; set; }

        [JsonProperty(PropertyName = "TissueType")]
        public string TissueType { get; set; }
    }
}