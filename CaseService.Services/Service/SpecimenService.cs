using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            specimenRepository = SpecimenRepository.Instance;
            specimenFactory = SpecimenFactory.Instance;
        }

        public Specimen createAndPersistAsync(CreateSpecimenDTO dto) {
            Specimen result = specimenFactory.create(dto);
            Document document = specimenRepository.Save(result);
            return specimenFactory.create(document);
        }

        public SpecimenDTO createAndPersistDTOAsync(CreateSpecimenDTO dto) {
            Specimen result = specimenFactory.create(dto);
            Document document = specimenRepository.Save(result);
            return specimenFactory.createDTO(document);
        }

        public async Task<List<Specimen>> ListAllAsync() {
            return await specimenRepository.ListAllAsync();
        }

        public async Task<List<SpecimenDTO>> ListAllDTOAsync() {
            return specimenFactory.create(await specimenRepository.ListAllAsync());
        }

        public SpecimenDTO GetById(string id) {
            return specimenFactory.createDTO(specimenRepository.findByIdAsync(id).Result);
        }
    }

}