using System.Collections.Generic;
using CaseService.Services.Domain;
using CaseService.Services.DTO;
using Newtonsoft.Json;

namespace CaseService.Services.Factory {
    public class SlideFactory: SingletonBase<SlideFactory> {
        public Slide create(CreateSlideDTO dto) {
            Slide result = JsonConvert.DeserializeObject<Slide>(JsonConvert.SerializeObject(dto));
            return result;
        }

        public List<SlideDTO> create(List<Slide> slides) {
            List<SlideDTO> result = JsonConvert.DeserializeObject<List<SlideDTO>>(JsonConvert.SerializeObject(slides));
            return result;
        }
    }
}