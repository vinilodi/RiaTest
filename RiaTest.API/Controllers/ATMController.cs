using Microsoft.AspNetCore.Mvc;
using RiaTest.Application.DTOs;
using RiaTest.Application.IAppServices;
using System.Collections.Generic;

namespace RiaTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATMController : ControllerBase
    {
        private readonly IATMAppService _atmAppService;

        public ATMController(IATMAppService atmAppService)
        {
            _atmAppService = atmAppService;
        }

        [HttpGet("calculate-payout/{value}")]
        public GenericResultDTO<List<string>> CalculatePayout(int value)
        {
            return _atmAppService.CalculatePayout(value);
        }
    }
}
