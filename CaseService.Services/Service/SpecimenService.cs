using CaseService.Services.Data.Repository;
using CaseService.Services.Domain;
using CaseService.Services.DTO;
using CaseService.Services.Factory;
using Microsoft.Azure.Documents;

namespace CaseService.Services.Service {
    public class SpecimenService {
        private readonly SpecimenRepository specimenRepository;
        private readonly SpecimenFactory specimenFactory;

        public SpecimenService() {
            specimenRepository = new SpecimenRepository();
            specimenFactory = new SpecimenFactory();
        }

        public Specimen createAndPersist(SpecimenDTO dto) {
            Specimen result = specimenFactory.create(dto);
            Document doc = specimenRepository.Save(result);
            return result;
        }
    }
}