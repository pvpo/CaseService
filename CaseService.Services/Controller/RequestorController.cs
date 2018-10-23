using System.Collections.Generic;
using System.Threading.Tasks;
using CaseService.Services.DTO;
using CaseService.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace CaseService.Services.Controller {


    [Route("api/v1/[controller]")]
    [ApiController]
    public class RequestorController {

        private readonly RequestorService requestorService;

        public RequestorController(RequestorService rs) {
            requestorService = rs;
        }

        [Route("{id}")]
        [HttpGet]
        public RequestorDTO GetById(string id) => requestorService.GetById(id);

        [HttpGet]
        public Task<List<RequestorDTO>> Get() => requestorService.ListAllDTOAsync();

    }
}