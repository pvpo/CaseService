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
    public class PatientController : ControllerBase
    {

        private readonly PatientService patientService;

        public PatientController(PatientService ps) {
            patientService = ps;
        }

        [HttpGet]
        public Task<List<Patient>> GetAll() => patientService.ListAllAsync();


    }
}