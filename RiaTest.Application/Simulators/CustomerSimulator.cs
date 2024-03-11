using Newtonsoft.Json;
using RiaTest.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RiaTest.Application.Simulators
{
    public class CustomerSimulator
    {
        private readonly HttpClient _httpClient;
        private readonly Random _random;

        public CustomerSimulator()
        {
            _httpClient = new HttpClient();
            _random = new Random();
        }

        public async Task<List<string>> SimulateRequests(int requests)
        {
            var results = new List<string>();
            var tasks = new List<Task>();

            for (int i = 1; i <= requests; i++)
            {
                tasks.Add(SimulatePostRequest(i * 2 - 1, results));

                tasks.Add(SimulateGetRequest(results));
            }

            await Task.WhenAll(tasks);

            return results;
        }

        private async Task SimulatePostRequest(int id, List<string> results)
        {
            var result = await ExecutePostRequest(id);
            results.Add(result);
        }

        private async Task<string> ExecutePostRequest(int id)
        {
            var customers = GenerateCustomers(id);
            var json = JsonConvert.SerializeObject(customers);
            var requestContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:23956/api/customer", requestContent);

            return $"POST: {await response.Content.ReadAsStringAsync()}";
        }

        private async Task SimulateGetRequest(List<string> results)
        {
            var result = await ExecuteGetRequest();
            results.Add(result);
        }

        private async Task<string> ExecuteGetRequest()
        {
            var response = await _httpClient.GetAsync("http://localhost:23956/api/customer");

            return $"GET: {await response.Content.ReadAsStringAsync()}";
        }

        private List<InsertCustomerDTO> GenerateCustomers(int id)
        {
            var customers = new List<InsertCustomerDTO>();

            var firstNames = new string[] { "Leia", "Sadie", "Jose", "Sara", "Frank", "Dewey", "Tomas", "Joel", "Lukas", "Carlos" };
            var lastNames = new string[] { "Liberty", "Ray", "Harrison", "Ronan", "Drew", "Powell", "Larsen", "Chan", "Anderson", "Lane" };

            for (int i = id; i < id + 2; i++)
            {
                var firstName = firstNames[_random.Next(firstNames.Length)];
                var lastName = lastNames[_random.Next(lastNames.Length)];
                var age = _random.Next(10, 91);

                customers.Add(new InsertCustomerDTO
                {
                    Id = i,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                });
            }

            return customers;
        }
    }
}
