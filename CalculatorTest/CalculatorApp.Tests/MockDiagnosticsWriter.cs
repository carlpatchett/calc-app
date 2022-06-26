using CalculatorApp.Core.Interfaces;

namespace CalculatorApp.Tests
{
    internal class MockDiagnosticsWriter : IDiagnosticsWriter
    {
        public void WriteToDebugger(string category, object value)
        {
            this.LastCategory = category;
            this.LastValue = value;
        }

        public string LastCategory { get; set; }

        public object LastValue { get; set; }
    }
}
