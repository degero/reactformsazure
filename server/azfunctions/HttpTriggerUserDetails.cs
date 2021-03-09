using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using shared;
using shared.DTO;

namespace ReactForm
{
    public static class HttpTriggerUserDetails
    {
        [FunctionName("HttpTriggerUserDetails")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HttpTriggerUserDetails " + req.Method);
            string responseMessage = "";
            // TODO fix ref
          //  var storageMan = new StorageService(System.Environment.GetEnvironmentVariable("StorageConnectionString"));
            if (req.Method == HttpMethods.Get)
            {
                string name = req.Query["name"];
                // TODO get name from storage
                var userData = new { name = "Joe", email = "test@test.com" };
                var list = new[] { userData };
                responseMessage = JsonConvert.SerializeObject(userData);
            }
            else if (req.Method == HttpMethods.Post)
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject<UserTableEntity>(requestBody);
                var tableEntity = new UserTableEntity(data.name, data.email);
               // storageMan.WriteTableData(tableEntity, "User");
                responseMessage = "User successfully saved";
            }

            return new OkObjectResult(responseMessage);
        }
    }
}
