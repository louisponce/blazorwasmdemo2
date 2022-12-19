using System;
using Api;
using Api.Data;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Api
{
    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<AppSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("AppSettings").Bind(settings);
            });

            builder.Services.AddSingleton<IData, Api.Data.Data>();
            builder.Services.AddLogging();
        }
    }
}
