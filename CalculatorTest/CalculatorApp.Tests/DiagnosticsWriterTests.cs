namespace CalculatorApp.Tests
{
    [TestClass]
    public class DiagnosticsWriterTests
    {
        private MockDiagnosticsWriterWithDatabaseStorage? mDiagnosticsWriter;
        private Random? mRandom;

        [TestInitialize()]
        public void Initialize()
        {
            mDiagnosticsWriter = new MockDiagnosticsWriterWithDatabaseStorage();
            mRandom = new Random();
        }

        [TestMethod]
        public void DiagnosticsWriter_Write()
        {
            if (mDiagnosticsWriter == null || mRandom == null)
            {
                Assert.Fail($"{GetMethodName()} failed due to bad initialization.");
            }

            var x = mRandom.Next(1000);
            var y = mRandom.Next(1000);
            var result = x + y;
            var operationToWrite = new Operation(Core.ArithmeticOperators.Add, x, y, result);

            // Write the operation to the Debugger and the DB
            mDiagnosticsWriter.WriteToDebugger(operationToWrite);

            // Retrieve the most recent operation added to the DB
            var latestOperation = mDiagnosticsWriter.RetrieveLatestOperation();

            // Ensure it matches the one we just wrote to the debugger
            Assert.AreEqual(operationToWrite.@operator, latestOperation.@operator);
            Assert.AreEqual(operationToWrite.x, latestOperation.x);
            Assert.AreEqual(operationToWrite.y, latestOperation.y);
            Assert.AreEqual(operationToWrite.result, latestOperation.result);
        }

        private static string GetMethodName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().Name;
        }
    }
}