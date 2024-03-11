using Microsoft.AspNetCore.Mvc;
using RiaTest.Application.Simulators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiaTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSimulatorController : ControllerBase
    {
        [HttpGet("{requests}")]
        public async Task<List<string>> SimulateCustomers(int requests)
        {
            var customerSimulator = new CustomerSimulator();
            return await customerSimulator.SimulateRequests(requests);
        }
    }
}
