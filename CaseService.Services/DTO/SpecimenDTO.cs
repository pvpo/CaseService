using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CaseService.Services.DTO
{
    public class SpecimenDTO {

        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "SpecimenId")]
        public string SpecimenId { get; set; }

        [JsonProperty(PropertyName = "BlockId")]
        public long BlockId { get; set; }

        [JsonProperty(PropertyName = "TissueType")]
        public string TissueType { get; set; }

        [JsonProperty(PropertyName = "Slides")]
        public List<SlideDTO> Slides { get; set; }
    }
}