using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCalculator.Calculation.Service;
using WebCalculator.Models;

namespace WebCalculator.Controllers
{
    public class HomeController : Controller
    {
        public readonly ICalculator Calculator;

        public HomeController(ICalculator calculator)
        {
            this.Calculator = calculator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new CalculateViewModel());
        }

        [HttpPost]
        public IActionResult Calculate(string expression)
        {
            int result = Calculator.Calculate(expression ?? String.Empty);

            return View("Index", new CalculateViewModel() { Result = result, Calculated = true, Expression = expression });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
