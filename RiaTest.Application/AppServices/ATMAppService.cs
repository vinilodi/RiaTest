using RiaTest.Application.DTOs;
using RiaTest.Application.IAppServices;
using RiaTest.Domain.IServices;
using System;
using System.Collections.Generic;

namespace RiaTest.Application.AppServices
{
    public class ATMAppService : IATMAppService
    {
        private readonly IATMService _atmService;

        public ATMAppService(IATMService atmService)
        {
            _atmService = atmService;
        }

        public GenericResultDTO<List<string>> CalculatePayout(int value)
        {
            var result = new GenericResultDTO<List<string>>();
            try
            {
                result.Result = _atmService.CalculatePayout(value);
                result.Count = result.Result.Count;
            }
            catch (ArgumentException ex)
            {
                result.Errors.Add(ex.Message);
                result.Success = false;
            }
            catch (Exception)
            {
                result.Errors.Add("An error occurred while trying to calculate payout.");
                result.Success = false;
            }
            return result;
        }
    }
}
