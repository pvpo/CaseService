
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaseService.Services.Data.Repository;
using CaseService.Services.Domain;
using CaseService.Services.DTO;
using CaseService.Services.Factory;
using Microsoft.Azure.Documents;

namespace CaseService.Services.Service {
    public class RequestorService {
        private readonly RequestorRepository requestorRepository;
        private readonly RequestorFactory requestorFactory;

        public RequestorService() {
            requestorRepository = RequestorRepository.Instance;
            requestorFactory = RequestorFactory.Instance;
        }

        public async Task<List<Requestor>> ListAllAsync() {
            return await requestorRepository.ListAllAsync();
        }

        public async Task<List<RequestorDTO>> ListAllDTOAsync() {
            return requestorFactory.create(await requestorRepository.ListAllAsync());
        }

        public RequestorDTO GetById(string id) {
            return requestorFactory.createDTO(requestorRepository.findByIdAsync(id).Result);
        }
    }

}