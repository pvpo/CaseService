using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CaseService.Services.Domain
{
    public class Specimen : BaseDomainObject {
        public static readonly string collectionName = "Specimen";
        public Specimen() : base(collectionName) { }

        [JsonProperty(PropertyName = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "SpecimenId")]
        public string SpecimenId { get; set; }

        [JsonProperty(PropertyName = "TissueType")]
        public string TissueType { get; set; }

        [JsonProperty(PropertyName = "BlockId")]
        public long BlockId { get; set; }

        [JsonProperty(PropertyName = "Slides")]
        public List<string> Slides { get; set; }

    }
}