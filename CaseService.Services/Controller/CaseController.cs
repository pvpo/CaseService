using System;
using Microsoft.AspNetCore.Mvc;
using CaseService.Services.DTO;

namespace CaseService.Services.Controller
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