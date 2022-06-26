using CalculatorApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CalculatorApp.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationDatabaseStorageController : Controller
    {
        private readonly ILogger<OperationDatabaseStorageController> mLogger;
        private readonly IOperationDatabaseStorageHandler mOperationDatabaseStorageHandler;

        public OperationDatabaseStorageController(ILogger<OperationDatabaseStorageController> logger, IOperationDatabaseStorageHandler operationDatabaseStorageHandler)
        {
            mOperationDatabaseStorageHandler = operationDatabaseStorageHandler;
            mLogger = logger;
        }

        [HttpGet("RetrieveLatestResult", Name = "RetrieveLatestResult")]
        public IActionResult RetrieveLatestResult()
        {
            this.Log($"Retrieving latest result");
            return Ok(mOperationDatabaseStorageHandler.RetrieveLatestOperation());
        }

        [HttpPost("Store", Name = "Store")]
        public IActionResult Store([FromBody] object operation)
        {
            var typedOperation = JsonConvert.DeserializeObject<Operation>(operation.ToString());
            this.Log($"Retrieving latest result");
            mOperationDatabaseStorageHandler.StoreOperation(this, typedOperation);

            return Ok();
        }

        private void Log(string message)
        {
            mLogger.LogTrace($"OperationDatabaseStorageController: {message}");
        }
    }
}
