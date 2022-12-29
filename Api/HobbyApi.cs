using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using Api.Data;
using System.Text.Json;
using SharedLibrary;

namespace Api
{
    public class HobbyApi
    {
        private static HobbyService service;

        public HobbyApi()
        {
            service ??= new(Environment.GetEnvironmentVariable("ConnectionString"));
        }

        [FunctionName("Hobby-Get")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "hobby")] HttpRequest req,
            ILogger log)
        {
            var entries = await service.GetDataset();
            var json = JsonSerializer.Serialize(entries);
            return new OkObjectResult(json);
        }
    }
}
