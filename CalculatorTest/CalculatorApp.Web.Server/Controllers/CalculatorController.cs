using CalculatorApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorApp.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : Controller
    {
        private readonly ILogger<CalculatorController> mLogger;
        private readonly ISimpleCalculator mCalculator;

        public CalculatorController(ILogger<CalculatorController> logger, ISimpleCalculator calculator)
        {
            mLogger = logger;
            mCalculator = calculator;
        }

        [HttpGet("Add/{x}/{y}", Name = "Add")]
        public double Add(int x, int y)
        {
            this.Log($"Adding {x} and {y}");
            var result = mCalculator.Add(x, y);
            return mCalculator.Add(x, y);
        }

        [HttpGet("Subtract/{x}/{y}", Name = "Subtract")]
        public double Subtract(int x, int y)
        {
            this.Log($"Subtracting {x} from {y}");
            return mCalculator.Subtract(x, y);
        }

        [HttpGet("Multiply/{x}/{y}", Name = "Multiply")]
        public double Multiply(int x, int y)
        {
            this.Log($"Multiplying {x} by {y}");
            return mCalculator.Multiply(x, y);

        }

        [HttpGet("Divide/{x}/{y}", Name = "Divide")]
        public double Divide(int x, int y)
        {
            this.Log($"Dividing {x} by {y}");
            return mCalculator.Divide(x, y);
        }
        private void Log(string message)
        {
            mLogger.LogTrace($"Calculator: {message}");
        }
    }
}
