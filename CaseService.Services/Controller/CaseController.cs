using System;
using Microsoft.AspNetCore.Mvc;
using CaseService.Services.DTO;
using CaseService.Services.Service;
using CaseService.Services.Domain;
using CaseService.Services.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

namespace CaseService.Services.Controller {

    [EnableCors("AllowSpecificOrigin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CaseController : ControllerBase {

        private readonly Services.Service.CaseService caseService;

        public CaseController(Services.Service.CaseService cs) {
            caseService = cs;
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpPost]
        public Case Post(CreateCaseDTO value) {

            if(!CaseType.isValidType(value.Type)) {
                throw new ArgumentException("Invalid Case type");
            }

            return caseService.createAndPersistAsync(value);
        }

        [HttpGet]
        public List<CaseDTO> Get() {
            return caseService.ListAllDTOAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public CaseDTO GetById(String id) => caseService.GetById(id);

        [HttpDelete]
        [Route("{id}")]
        public bool DeleteById(String id) {
            caseService.DeleteById(id);
            return true;
        }

        [HttpGet]
        [Route("count/{type}")]
        public int GetCountByType(String type) {

            if(CaseType.isValidType(type)) {
                return CaseRepository.Instance.GetCountByTypeAsync(type);
            } else {
                throw new ArgumentException("Case type is invalid.");
            }
        }

        [HttpGet]
        [Route("types")]
        public string[] GetCaseTypes() {
            return CaseType.types;
        }

        [HttpPost]
        [Route("close/{id}")]
        public Case CloseCase(string id) {
            return caseService.Close(id);
        }

        [HttpGet]
        [Route("stats/closed/daily")]
        public int[] GetDailyClosedStats() {
            return caseService.GetDailyClosedCountChartData();
        }

        [HttpGet]
        [Route("closed/daily")]
        public int GetDailyCLosed(){
            return caseService.GetTodaysClosedCases();
        }

    }
}