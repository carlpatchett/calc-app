namespace CalculatorApp.Tests
{
    [TestClass]
    public class DiagnosticsWriterTests
    {
        private MockDiagnosticsWriter? mDiagnosticsWriter;

        [TestInitialize()]
        public void Initialize()
        {
            mDiagnosticsWriter = new MockDiagnosticsWriter();
        }

        [TestMethod]
        public void DiagnosticsWriter_Write()
        {
            if (mDiagnosticsWriter == null)
            {
                Assert.Fail($"{GetMethodName()} failed due to bad initialization.");
            }

            mDiagnosticsWriter.WriteToDebugger("TestCategory", true);

            Assert.AreEqual("TestCategory", mDiagnosticsWriter.LastCategory);
            Assert.AreEqual(true, mDiagnosticsWriter.LastValue);
        }

        private static string GetMethodName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().Name;
        }
    }
}