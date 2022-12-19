using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using SharedLibrary;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data
{
    public interface IData
    {
        Task InsertEntry();
        Person[] GetEntries();
    }

    public class Data : IData
    {
        static string tableName = "People";
        TableClient tableClient;
        private readonly AppSettings appSettings;

        public Data(IOptions<AppSettings> options)
        {
            this.appSettings = options.Value;

            // Construct a new "TableClient" using a "TableSharedKeyCredential" />.
            tableClient = new TableClient(
                            //new Uri(appSettings.StorageUri),
                            //tableName,
                            //new TableSharedKeyCredential(appSettings.AccountName, appSettings.StorageAccountKey));
            new Uri(System.Environment.GetEnvironmentVariable("StorageUri")),
                tableName,
                new TableSharedKeyCredential(System.Environment.GetEnvironmentVariable("AccountName"), System.Environment.GetEnvironmentVariable("StorageAccountKey")));

            // Create the table in the service.
            tableClient.CreateIfNotExists();
        }

        public async Task InsertEntry()
        {
            // Make a dictionary entity by defining a <see cref="TableEntity">.
            string partitionKey = "Female";
            string rowKey = "01";
            var entity = new TableEntity(partitionKey, rowKey)
            {
                { "FirstName", "Heidi" },
                { "LastName", "High" },
                { "Email", "heidi.high@email.com" }
            };
            await tableClient.UpsertEntityAsync(entity);
        }

        public Person[] GetEntries()
        {
            var queryResultsLINQ = tableClient.Query<Person>();
            return queryResultsLINQ.ToArray();
        }
    }
}
