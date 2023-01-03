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
using SharedLibrary.SystemData;
using System.IO;
using System.Linq;
using System.Text;

namespace Api
{
    public class PeopleApi
    {
        private static PeopleService peopleService;

        public PeopleApi()
        {
            peopleService ??= new(Environment.GetEnvironmentVariable("ConnectionString"));
        }


        //[FunctionName("Person-Get")]
        //public async Task<IActionResult> Get(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "people")] HttpRequest req,
        //    ILogger log)
        //{
        //    var entries = await peopleService.GetEntries(string.Empty);
        //    var json = JsonSerializer.Serialize(entries);
        //    return new OkObjectResult(json);
        //}

        [FunctionName("Person-Get")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "people/{rowkey?}")] HttpRequest req, string rowkey,
            ILogger log)
        {
            var entries = await peopleService.GetEntries(rowkey);
            var json = JsonSerializer.Serialize(entries);
            return new OkObjectResult(json);
        }

        [FunctionName("Person-Post")]
        public async Task<IActionResult> Post(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "people")] HttpRequest req,
        ILogger log)
        {
            string requestBody = String.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }
            var person = JsonSerializer.Deserialize<Person>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            person = await peopleService.AddOrUpdateAsync(person);
            var json = JsonSerializer.Serialize(person);
            return new OkObjectResult(json);
        }

        [FunctionName("Person-Delete")]
        public async Task<IActionResult> Delete(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "people/{partitionkey}/{rowkey}")] HttpRequest req, string partitionkey, string rowkey,
        ILogger log)
        {
            await peopleService.DeleteAsync(rowkey, partitionkey);
            return new OkResult();
        }

        [FunctionName("ClientPrincipal-Get")]
        public IActionResult ClientPrincipalGet(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "clientprincipal")] HttpRequest req,
            ILogger log)
        {
            var principal = new ClientPrincipal();

            if (req.Headers.TryGetValue("x-ms-client-principal", out var header))
            {
                var data = header[0];
                var decoded = Convert.FromBase64String(data);
                var json = Encoding.UTF8.GetString(decoded);
                principal = JsonSerializer.Deserialize<ClientPrincipal>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            principal.UserRoles = principal.UserRoles?.Except(new string[] { "anonymous" }, StringComparer.CurrentCultureIgnoreCase);

            //if (!principal.UserRoles?.Any() ?? true)
            //{
            //    return new OkObjectResult(new ClaimsPrincipal());
            //}

            return new OkObjectResult(principal);
        }
    }
}
