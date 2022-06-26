namespace CalculatorApp.Core.Interfaces
{
    /// <summary>
    /// Provides a contract for handling operation database storage.
    /// </summary>
    public interface IOperationDatabaseStorageHandler : IOperationStorageHandler
    {
        /// <summary>
        /// Ensures the database and table(s) exist.
        /// </summary>
        void EnsureDb();
    }
}
