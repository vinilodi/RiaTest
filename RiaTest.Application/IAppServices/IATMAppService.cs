using RiaTest.Application.DTOs;
using System.Collections.Generic;

namespace RiaTest.Application.IAppServices
{
    public interface IATMAppService
    {
        GenericResultDTO<List<string>> CalculatePayout(int value);
    }
}
