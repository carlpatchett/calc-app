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
            var result = x + y;
            WriteToDebugger("Add", result);
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
            var result = x - y;
            WriteToDebugger("Subtract", result);
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
            var result = x * y;
            WriteToDebugger("Multiply", result);
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
            var result = x / y;
            WriteToDebugger("Divide", result);
            return result;
        }

        public void WriteToDebugger(string category, object value)
        {
            Debug.Write(value, category);
        }
    }
}