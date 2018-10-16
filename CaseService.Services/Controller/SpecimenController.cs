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
        public Specimen Post(SpecimenDTO value)
        {
            return specimenService.createAndPersistAsync(value);
        }

        [HttpGet]
        public Task<List<Specimen>> Get() {
            return specimenService.ListAllAsync();
        }

    }
}