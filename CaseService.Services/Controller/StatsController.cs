using System.Collections.Generic;
using CaseService.Services.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CaseService.Services.Controller {

    [EnableCors("AllowSpecificOrigin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StatsController {

        private readonly StatsService statsService;

        public StatsController(StatsService ss) {
            statsService = ss;
        }

        [HttpGet]
        [Route("cases/closed/daily")]
        public List<int[]> GetDailyClosedStats() {
            return statsService.GetDailyClosedCountChartData();
        }

        [HttpGet]
        [Route("db/stats")]
        public List<List<string>> getDBStats() {
            return statsService.getDBStats();
        }
    }
}