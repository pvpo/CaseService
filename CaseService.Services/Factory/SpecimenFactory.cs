using System;
using System.Collections.Generic;
using CaseService.Services.Data;
using CaseService.Services.Domain;
using CaseService.Services.DTO;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace CaseService.Services.Factory {
    public class SpecimenFactory : SingletonBase<SpecimenFactory> {

        private SpecimenFactory () { }

        public Specimen create(CreateSpecimenDTO dto) {

            Specimen result = JsonConvert.DeserializeObject<Specimen>(JsonConvert.SerializeObject(dto));
            return result;
        }

        public Specimen create(Document doc) {
            Specimen result = JsonConvert.DeserializeObject<Specimen>(JsonConvert.SerializeObject(doc));

            return result;
        }

        public SpecimenDTO createDTO(Document doc) {
            SpecimenDTO result = JsonConvert.DeserializeObject<SpecimenDTO>(JsonConvert.SerializeObject(doc));

            return result;
        }

        public List<SpecimenDTO> create(List<Specimen> specimens) {
            List<SpecimenDTO> result = new List<SpecimenDTO>();

            foreach(Specimen sp in specimens) {

                List<Slide> slides = null;
                if(sp.Slides != null) {
                    List<string> slideIds = sp.Slides;
                    slides = SlideRepository.Instance.ListAsync(slideIds).Result;
                }

                sp.Slides = null;
                sp.SetPropertyValue("Slides", null);

                SpecimenDTO dto = createDTO(sp);

                if(slides != null) {
                    dto.Slides = SlideFactory.Instance.create(slides);
                }

                result.Add(dto);
            }

            return result;
        }

    }
}