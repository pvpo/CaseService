using System;
using Microsoft.AspNetCore.Mvc;
using other_case_service.DTO;

namespace other_case_service.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SpecimenController : ControllerBase
    {

        [HttpPost]
        public void Post(SpecimenDTO value)
        {
            Console.WriteLine(value.ProtocolName);
        }

    }
}