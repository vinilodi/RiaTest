using Microsoft.AspNetCore.Mvc;
using RiaTest.Application.Simulators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiaTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATMSimulatorController : ControllerBase
    {
        [HttpGet]
        public async Task<List<string>> SimulateATM()
        {
            var customerSimulator = new ATMSimulator();
            return await customerSimulator.SimulateRequests();
        }
    }
}
