using System;
using Api;
using Api.Data;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using System.Xml;
using Azure.Data.Tables;
using System.Threading;
using SharedLibrary;
using SharedLibrary.SystemData;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Api
{
    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Use the Environment variables instead
            //builder.Services.AddOptions<AppSettings>()
            //.Configure<IConfiguration>((settings, configuration) =>
            //{
            //    configuration.GetSection("AppSettings").Bind(settings);
            //});

            //
            //builder.Services.AddSingleton<IBaseInterface<Person>>(x => { return new PeopleService<Person>(Environment.GetEnvironmentVariable("ConnectionString")); });
            builder.Services.AddLogging();
        }
    }
}
