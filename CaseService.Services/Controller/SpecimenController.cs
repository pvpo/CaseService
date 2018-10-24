using System;
using Microsoft.AspNetCore.Mvc;
using CaseService.Services.DTO;
using CaseService.Services.Data.Repository;
using CaseService.Services.Domain;
using CaseService.Services.Service;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CaseService.Services.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SpecimenController : ControllerBase
    {
        private readonly SpecimenService specimenService;

        public SpecimenController(SpecimenService ss) {
            specimenService = ss;
        }

        [HttpPost]
        public SpecimenDTO Post(CreateSpecimenDTO value)
        {
            return specimenService.createAndPersistDTOAsync(value);
        }

        [HttpGet]
        public Task<List<SpecimenDTO>> GetAll() => specimenService.ListAllDTOAsync();

        [Route("{id}")]
        [HttpGet]
        public SpecimenDTO GetById(string id) => specimenService.GetById(id);

        [Route("tissue/types")]
        [HttpGet]
        public string[] GetTypes() {
            return TissueType.types;
        }

    }
}