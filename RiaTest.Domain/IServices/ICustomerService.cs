using RiaTest.Domain.Entities;
using System.Collections.Generic;

namespace RiaTest.Domain.IServices
{
    public interface ICustomerService
    {
        string[] Validate(Customer customer, IEnumerable<int> customerIds, ref string[] errors);
        List<Customer> Insert(Customer customer, List<Customer> customers);
    }
}
