using Microsoft.AspNetCore.Mvc;
using RiaTest.Application.DTOs;
using RiaTest.Application.IAppServices;
using System.Collections.Generic;

namespace RiaTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomerController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [HttpGet]
        public GenericResultDTO<List<CustomerDTO>> GetAll()
        {
            return _customerAppService.GetAll();
        }

        [HttpPost]
        public GenericResultDTO<List<InsertCustomerResultDTO>> Insert(List<InsertCustomerDTO> customers)
        {
            return _customerAppService.Insert(customers);
        }
    }
}
