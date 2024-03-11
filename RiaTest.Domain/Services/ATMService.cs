using RiaTest.Domain.IServices;
using System;
using System.Collections.Generic;

namespace RiaTest.Domain.Services
{
    public class ATMService : IATMService
    {
        public ATMService()
        {
        }

        public List<string> CalculatePayout(int value)
        {
            if (value <= 0 || value % 10 != 0)
            {
                throw new ArgumentException("Value must be greater than 0 and a multiple of 10.");
            }

            List<string> combinations = new List<string>();
            int[] denominations = { 10, 50, 100 };
            FindCombinations(value, denominations, 0, "", combinations);

            return combinations;
        }

        public static void FindCombinations(int target, int[] denominations, int index, string current, List<string> combinations)
        {
            if (target == 0)
            {
                combinations.Add(current.TrimStart(' ', '+'));
                return;
            }

            if (index == denominations.Length)
                return;

            int denomination = denominations[index];
            int maxCount = target / denomination;

            for (int count = 0; count <= maxCount; count++)
            {
                string separator = count > 0 ? " + " : "";
                string newCombination = current + separator + (count > 0 ? count + " x " + denomination + " EUR" : "");
                int newTarget = target - count * denomination;
                FindCombinations(newTarget, denominations, index + 1, newCombination, combinations);
            }
        }
    }
}
