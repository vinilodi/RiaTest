using System.Collections.Generic;

namespace RiaTest.Domain.IServices
{
    public interface IATMService
    {
        List<string> CalculatePayout(int value);
    }
}
