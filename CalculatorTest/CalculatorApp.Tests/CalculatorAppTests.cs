using CalculatorApp.Core;
using CalculatorApp.Core.Interfaces;

namespace CalculatorApp.Tests
{
    [TestClass]
    public class CalculatorAppTests
    {
        private Random? mRandom;
        private ISimpleCalculator? mCalculator;

        [TestInitialize()]
        public void Initialize()
        {
            mCalculator = new Calculator();
            mRandom = new Random(12345);
        }

        [TestMethod]
        public void Calculator_Add()
        {
            if (mCalculator == null || mRandom == null) { 
                Assert.Fail($"{GetMethodName()} failed due to bad initialization.");
            }

            var firstNumber = mRandom.Next(1000);
            var secondNumber = mRandom.Next(1000);
            var result = mCalculator.Add(firstNumber, secondNumber);

            Assert.AreEqual(firstNumber + secondNumber, result);
        }

        [TestMethod]
        public void Calculator_Subtract()
        {
            if (mCalculator == null || mRandom == null)
            {
                Assert.Fail($"{GetMethodName()} failed due to bad initialization.");
            }
            var firstNumber = mRandom.Next(1000);
            var secondNumber = mRandom.Next(1000);
            var result = mCalculator.Subtract(firstNumber, secondNumber);

            Assert.AreEqual(firstNumber - secondNumber, result);
        }

        [TestMethod]
        public void Calculator_Multiply()
        {
            if (mCalculator == null || mRandom == null)
            {
                Assert.Fail($"{GetMethodName()} failed due to bad initialization.");
            }
            var firstNumber = mRandom.Next(1000);
            var secondNumber = mRandom.Next(1000);
            var result = mCalculator.Multiply(firstNumber, secondNumber);

            Assert.AreEqual(firstNumber * secondNumber, result);
        }

        [TestMethod]
        public void Calculator_Divide()
        {
            if (mCalculator == null || mRandom == null)
            {
                Assert.Fail($"{GetMethodName()} failed due to bad initialization.");
            }
            var firstNumber = mRandom.Next(1000);
            var secondNumber = mRandom.Next(1000);
            var result = mCalculator.Divide(firstNumber, secondNumber);

            Assert.AreEqual(firstNumber / secondNumber, result);
        }

        private static string GetMethodName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().Name;
        }
    }
}