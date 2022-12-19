using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
using System;
using Api.Data;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Api
{
    public class GetPeople
    {
        public IData data { get; }
        //public IConfiguration configuration { get; }

        public GetPeople(IData data)
        {
            this.data = data;
        }


        [FunctionName("GetPeople")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //await data.InsertEntry();

            string json = "[" +
                "{\"Id\":10,\"FirstName\":\"Jaynell\",\"LastName\":\"Bawdon\",\"Email\":\"jbawdon0@blog.com\",\"Gender\":\"Female\"},{\"Id\":2,\"FirstName\":\"Casi\",\"LastName\":\"Itzkovsky\",\"Email\":\"citzkovsky1@themeforest.net\",\"Gender\":\"Male\"},{\"Id\":3,\"FirstName\":\"Elisha\",\"LastName\":\"Bowkett\",\"Email\":\"ebowkett2@answers.com\",\"Gender\":\"Male\"}," +
                "{\"Id\":20,\"FirstName\":\"Jaynell\",\"LastName\":\"Bawdon\",\"Email\":\"jbawdon0@blog.com\",\"Gender\":\"Female\"},{\"Id\":2,\"FirstName\":\"Casi\",\"LastName\":\"Itzkovsky\",\"Email\":\"citzkovsky1@themeforest.net\",\"Gender\":\"Male\"},{\"Id\":3,\"FirstName\":\"Elisha\",\"LastName\":\"Bowkett\",\"Email\":\"ebowkett2@answers.com\",\"Gender\":\"Male\"}," +
                "{\"Id\":30,\"FirstName\":\"Jaynell\",\"LastName\":\"Bawdon\",\"Email\":\"jbawdon0@blog.com\",\"Gender\":\"Female\"},{\"Id\":2,\"FirstName\":\"Casi\",\"LastName\":\"Itzkovsky\",\"Email\":\"citzkovsky1@themeforest.net\",\"Gender\":\"Male\"},{\"Id\":3,\"FirstName\":\"Elisha\",\"LastName\":\"Bowkett\",\"Email\":\"ebowkett2@answers.com\",\"Gender\":\"Male\"}," +
                "{\"Id\":40,\"FirstName\":\"Jaynell\",\"LastName\":\"Bawdon\",\"Email\":\"jbawdon0@blog.com\",\"Gender\":\"Female\"},{\"Id\":2,\"FirstName\":\"Casi\",\"LastName\":\"Itzkovsky\",\"Email\":\"citzkovsky1@themeforest.net\",\"Gender\":\"Male\"},{\"Id\":3,\"FirstName\":\"Elisha\",\"LastName\":\"Bowkett\",\"Email\":\"ebowkett2@answers.com\",\"Gender\":\"Male\"}," +
                "{\"Id\":50,\"FirstName\":\"Jaynell\",\"LastName\":\"Bawdon\",\"Email\":\"jbawdon0@blog.com\",\"Gender\":\"Female\"},{\"Id\":2,\"FirstName\":\"Casi\",\"LastName\":\"Itzkovsky\",\"Email\":\"citzkovsky1@themeforest.net\",\"Gender\":\"Male\"},{\"Id\":3,\"FirstName\":\"Elisha\",\"LastName\":\"Bowkett\",\"Email\":\"ebowkett2@answers.com\",\"Gender\":\"Male\"}" +

                "]";
            //var people = JsonConvert.DeserializeObject<SharedLibrary.Person[]>(json);

            var people = data.GetEntries();
            json =  JsonSerializer.Serialize(people);
            return new OkObjectResult(json);
        }
    }
}
