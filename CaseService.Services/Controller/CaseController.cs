using System;
using Microsoft.AspNetCore.Mvc;
using CaseService.Services.DTO;
using CaseService.Services.Service;
using CaseService.Services.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CaseService.Services.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CaseController : ControllerBase
    {

        private readonly Services.Service.CaseService caseService;

        public CaseController(Services.Service.CaseService cs) {
            caseService = cs;
        }

        [HttpPost]
        public Case Post(CreateCaseDTO value)
        {
            return caseService.createAndPersistAsync(value);
        }

        [HttpGet]
        public List<CaseDTO> Get() {
            return caseService.ListAllDTOAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public CaseDTO GetById(String id) {
            return caseService.GetById(id);
        }


    }
}