namespace CalculatorApp.Core.Interfaces
{
    /// <summary>
    /// Provides a contract for writing to the debugger.
    /// </summary>
    public interface IDiagnosticsWriter
    {
        void WriteToDebugger(string message);
    }
}
