using System;

namespace WebCalculator.Calculation.Service
{
    public interface ICalculator
    {
        int Calculate(string expression);
    }
}
