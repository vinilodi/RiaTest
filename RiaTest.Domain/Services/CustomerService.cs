using RiaTest.Domain.Entities;
using RiaTest.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RiaTest.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        public CustomerService()
        {
        }

        public string[] Validate(Customer customer, IEnumerable<int> customerIds, ref string[] errors)
        {
            int index = 0;

            if (string.IsNullOrEmpty(customer.FirstName))
            {
                errors[index++] = "First name cannot be empty.";
            }

            if (string.IsNullOrEmpty(customer.LastName))
            {
                errors[index++] = "Last name cannot be empty.";
            }

            if (customer.Age <= 18)
            {
                errors[index++] = "Age must be greater than 18.";
            }

            if (customerIds.Any(e => e == customer.Id))
            {
                errors[index++] = "Customer ID already exists.";
            }

            Array.Resize(ref errors, index);

            return errors;
        }

        public List<Customer> Insert(Customer customer, List<Customer> customers)
        {
            int i = customers.Count - 1;
            while (i >= 0 && CompareCustomers(customers[i], customer) > 0)
            {
                i--;
            }
            customers.Insert(i + 1, customer);
            return customers;
        }

        private static int CompareCustomers(Customer a, Customer b)
        {
            int result = string.Compare(a.LastName, b.LastName, StringComparison.Ordinal);
            if (result == 0)
            {
                result = string.Compare(a.FirstName, b.FirstName, StringComparison.Ordinal);
            }
            return result;
        }
    }
}
