using System;
using Microsoft.AspNetCore.Mvc;
using other_case_service.DTO;

namespace other_case_service.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CaseController : ControllerBase
    {

        [HttpPost]
        public void Post(CaseDTO value)
        {
            Console.WriteLine(value.Specimens.Count);
        }

    }
}