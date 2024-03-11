using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RiaTest.Application.Simulators
{
    public class ATMSimulator
    {
        private readonly HttpClient _httpClient;

        public ATMSimulator()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<string>> SimulateRequests()
        {
            var results = new List<string>();

            var amounts = new int[] { 30, 50, 60, 80, 140, 230, 370, 610, 980 };

            foreach (int amount in amounts)
            {
                results.Add(await ExecutePayoutRequest(amount));
            }

            return results;
        }

        private async Task<string> ExecutePayoutRequest(int amount)
        {
            var response = await _httpClient.GetAsync($"http://localhost:23956/api/atm/calculate-payout/{amount}");

            return $"{amount} - {await response.Content.ReadAsStringAsync()}";
        }
    }
}
