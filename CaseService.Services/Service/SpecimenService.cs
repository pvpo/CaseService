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
            specimenRepository = new SpecimenRepository();
            specimenFactory = new SpecimenFactory();
        }

        public Specimen createAndPersistAsync(SpecimenDTO dto) {
            Specimen result = specimenFactory.create(dto);

            Document doc = specimenRepository.Save(result);
            return specimenFactory.create(doc);
        }

        public async Task<List<Specimen>> ListAllAsync() {
            return await specimenRepository.ListAllAsync();
        }
    }

}