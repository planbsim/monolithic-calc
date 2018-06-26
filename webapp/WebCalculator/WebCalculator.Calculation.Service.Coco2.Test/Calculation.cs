using System;
using Xunit;

namespace WebCalculator.Calculation.Service.Coco2.Test
{
    public class Calculation
    {
        [Fact]
        public void Addition()
        {
            // Arrange
            ICalculator calc = new Calculator();
            // Act
            int result = calc.Calculate("(5+6)");
            // Assert
            Assert.Equal<int>(11, result);
        }
    }
}
