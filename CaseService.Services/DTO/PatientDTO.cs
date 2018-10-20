using Newtonsoft.Json;

namespace CaseService.Services.DTO
{
    public class PatientDTO {

        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "AccessionID")]
        public string AccessionID { get; set; }

        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "PatientId")]
        public string PatientId { get; set; }
    }
}