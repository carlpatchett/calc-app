namespace CalculatorApp.Core.Interfaces
{
    /// <summary>
    /// Provides a contract for handling operation storage.
    /// </summary>
    public interface IOperationStorageHandler
    {
        /// <summary>
        /// Retrieves the last operation that was stored.
        /// </summary>
        Operation? GetLatest();

        /// <summary>
        /// Retrieves the specified operation, if it exists.
        /// </summary>
        /// <param name="id">The id of the operation to retrieve.</param>
        Operation? Get(int id);

        /// <summary>
        /// Retrieves all operations.
        /// </summary>
        IEnumerable<Operation> Get();

        /// <summary>
        /// Stores the provided operation.
        /// </summary>
        void Store(Operation @operation);
    }
}
