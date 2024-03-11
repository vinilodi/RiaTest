using RiaTest.Application.DTOs;
using System.Collections.Generic;

namespace RiaTest.Application.IAppServices
{
    public interface ICustomerAppService
    {
        GenericResultDTO<List<CustomerDTO>> GetAll();
        GenericResultDTO<List<InsertCustomerResultDTO>> Insert(List<InsertCustomerDTO> newCustomersDTO);
    }
}
