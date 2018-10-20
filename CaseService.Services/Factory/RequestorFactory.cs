using System;
using System.Collections.Generic;
using CaseService.Services.Domain;
using CaseService.Services.DTO;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace CaseService.Services.Factory {
    public class RequestorFactory : SingletonBase<RequestorFactory> {

        public Requestor create(CreateRequestorDTO dto) {
            Requestor result = JsonConvert.DeserializeObject<Requestor>(JsonConvert.SerializeObject(dto));

            return result;
        }

        public Requestor create(Document doc) {
            Requestor result = JsonConvert.DeserializeObject<Requestor>(JsonConvert.SerializeObject(doc));

            return result;
        }

        public RequestorDTO createDTO(Requestor requestor) {
            RequestorDTO result = JsonConvert.DeserializeObject<RequestorDTO>(JsonConvert.SerializeObject(requestor));

            return result;
        }

        public RequestorDTO createDTO(Document doc) {
            RequestorDTO result = JsonConvert.DeserializeObject<RequestorDTO>(JsonConvert.SerializeObject(doc));

            return result;
        }

        public List<RequestorDTO> create(List<Requestor> requestor) {
            List<RequestorDTO> result = new List<RequestorDTO>();

            foreach(Requestor sp in requestor) {
                result.Add(createDTO(sp));
            }

            return result;
        }
    }
}