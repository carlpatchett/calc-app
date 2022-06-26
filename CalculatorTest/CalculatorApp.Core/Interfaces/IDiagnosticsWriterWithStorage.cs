namespace CalculatorApp.Core.Interfaces
{
    /// <summary>
    /// Provides a contract for writing to the debugger and storing the results in a database for 
    /// future use.
    /// </summary>
    public interface IDiagnosticsWriterWithDatabaseStorage : IDiagnosticsWriter, IOperationDatabaseStorageHandler
    {
    }
}
