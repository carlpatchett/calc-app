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

        [HttpGet("GetLatest", Name = "GetLatest")]
        public IActionResult GetLatest()
        {
            this.Log($"Retrieving latest result");
            var op = mOperationDatabaseStorageHandler.GetLatest();

            if (op != null)
            {
                return Ok(op);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("Get", Name = "Get")]
        public IActionResult Get()
        {
            this.Log($"Getting all results");
            return Ok(mOperationDatabaseStorageHandler.Get());
        }

        [HttpGet("Get/{id}", Name = "Get/{id}")]
        public IActionResult Get(int id)
        {
            this.Log($"Getting specified result");
            var op = mOperationDatabaseStorageHandler.Get(id);

            if (op != null)
            {
                return Ok(op);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("Store", Name = "Store")]
        public IActionResult Store([FromBody] object operation)
        {
            var typedOperation = JsonConvert.DeserializeObject<Operation>(operation.ToString());
            this.Log($"Retrieving latest result");
            mOperationDatabaseStorageHandler.Store(typedOperation);

            return Ok();
        }

        private void Log(string message)
        {
            mLogger.LogTrace($"OperationDatabaseStorageController: {message}");
        }
    }
}
