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
        Operation RetrieveLatestOperation();

        /// <summary>
        /// Stores the provided operation.
        /// </summary>
        void StoreOperation(object? sender, Operation @operation);
    }
}
