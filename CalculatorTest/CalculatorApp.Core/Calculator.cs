using CalculatorApp.Core.Interfaces;
using System.Diagnostics;

namespace CalculatorApp.Core
{
    /// <summary>
    /// A calculator capable of basic arithmetic.
    /// </summary>
    public class Calculator : ISimpleCalculator, IDiagnosticsWriter
    {
        /// <summary>
        /// Adds two numbers together.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number</param>
        /// <returns>The result of adding both numbers together.</returns>
        public int Add(int x, int y)
        {
            WriteToDebugger($"Add({x}, {y})");
            var result = x + y;
            return result;
        }

        /// <summary>
        /// Subtracts two numbers from eachother.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <returns>The result of subtracting the first number from the second.</returns>
        public int Subtract(int x, int y)
        {
            WriteToDebugger($"Subtract({x}, {y})");
            var result = x - y;
            return result;
        }

        /// <summary>
        /// Multiplies two numbers by eachother.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <returns>The result of multiplying the first number by the second number.</returns>
        public int Multiply(int x, int y)
        {
            WriteToDebugger($"Multiply({x}, {y})");
            var result = x * y;
            return result;
        }

        /// <summary>
        /// Divides two numbers by eachother.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <returns>The result of dividing the first number by the second number.</returns>
        public float Divide(int x, int y)
        {
            WriteToDebugger($"Divide({x}, {y})");
            var result = x / y;
            return result;
        }

        public void WriteToDebugger(string message)
        {
            Debug.Write($"Calculator: {message}");
        }
    }
}