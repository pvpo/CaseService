using CaseService.Services.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CaseService.Services.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SlideController : ControllerBase {

        [Route("protocols")]
        [HttpGet]
        public string[] GetProtocolNames() => Protocols.types;
    }
}