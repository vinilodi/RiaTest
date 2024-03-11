using AutoMapper;
using Newtonsoft.Json;
using RiaTest.Application.DTOs;
using RiaTest.Application.IAppServices;
using RiaTest.Application.Mappings;
using RiaTest.Domain.Entities;
using RiaTest.Domain.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RiaTest.Application.AppServices
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private static readonly object fileLock = new object();

        public CustomerAppService(ICustomerService customerService)
        {
            _customerService = customerService;
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile)));
            _mapper = config.CreateMapper();
        }

        public GenericResultDTO<List<CustomerDTO>> GetAll()
        {
            var result = new GenericResultDTO<List<CustomerDTO>>();

            try
            {
                string filePath = "customers.json";
                string data = "";

                lock (fileLock)
                {
                    data = File.ReadAllText(filePath);
                }

                if (!string.IsNullOrWhiteSpace(data))
                {
                    result.Result = JsonConvert.DeserializeObject<List<CustomerDTO>>(data);
                    result.Count = result.Result.Count;
                }
            }
            catch (Exception)
            {
                result.Errors.Add("An error occurred while trying to get the customers.");
                result.Success = false;
                result.Count = 0;
            }

            return result;
        }

        public GenericResultDTO<List<InsertCustomerResultDTO>> Insert(List<InsertCustomerDTO> newCustomersDTO)
        {
            var result = new GenericResultDTO<List<InsertCustomerResultDTO>>
            {
                Result = new List<InsertCustomerResultDTO>()
            };

            try
            {
                string filePath = "customers.json";

                lock (fileLock)
                {
                    if (!File.Exists(filePath))
                    {
                        File.WriteAllText(filePath, "[]");
                    }

                    string data = File.ReadAllText(filePath);

                    var customers = new List<Customer>();
                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        customers = JsonConvert.DeserializeObject<List<Customer>>(data);
                    }
                    var newCustomers = _mapper.Map<List<Customer>>(newCustomersDTO);

                    foreach (var c in newCustomers)
                    {
                        string[] errors = new string[4];

                        try
                        {
                            errors = _customerService.Validate(c, customers.Select(e => e.Id), ref errors);
                            if (!errors.Any())
                            {
                                customers = _customerService.Insert(c, customers);
                            }
                        }
                        catch (Exception)
                        {
                            errors[0] = "An error occurred while trying to insert the customer.";
                            continue;
                        }
                        finally
                        {
                            result.Result.Add(new InsertCustomerResultDTO
                            {
                                Id = c.Id,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Age = c.Age,
                                Errors = errors
                            });
                        }
                    }

                    File.WriteAllText(filePath, JsonConvert.SerializeObject(customers));

                    result.Count = result.Result.Where(e => !e.Errors.Any()).Count();
                }
            }
            catch (ArgumentException ex)
            {
                result.Errors.Add(ex.Message);
                result.Success = false;
                result.Count = 0;
            }
            catch (Exception)
            {
                result.Errors.Add("An error occurred while trying to insert the customers.");
                result.Success = false;
                result.Count = 0;
            }
            return result;
        }
    }
}
