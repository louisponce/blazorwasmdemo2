using Azure;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data
{
    internal class HobbyService
    {
        public readonly string hobbyTableName = "Hobby";
        private readonly AzureDataTableWrapper<Hobby> hobbyTableService;
        private string filter = string.Empty;
        public HobbyService(string connectionString)
        {
            hobbyTableService = new(connectionString, hobbyTableName);
        }

        public void SetFilter(string filter)
        {
            this.filter = filter;
        }

        public async Task<List<Hobby>> GetDataset()
        {
            List<Hobby> list = new();
            AsyncPageable<Hobby> data;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                data = await hobbyTableService.QueryWithFilterAsync(filters: filter);
            } else
            {
                data = await hobbyTableService.QueryAllAsync();
            }

            await foreach (var item in data)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
