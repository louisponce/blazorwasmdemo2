using Azure;
using Azure.Data.Tables;
using SharedLibrary.SystemData;
using System.Threading.Tasks;

namespace Api.Data
{
    public class AzureDataTableWrapper<TEntity> : IAzureDataTableWrapper<TEntity> where TEntity : class, ITableEntity, new()
    {
        private static TableServiceClient tableServiceClient = null;
        private static TableClient tableClient = null;
        private readonly string connectionString;
        private readonly string tableName;
        //private static readonly string systemTablePreffix = "SYSTEM-";
        private static readonly string mainPartionKey = "MAIN";
        private static readonly string tableInformationRowKey = "SYSTEM-TableInformation";
        //private static readonly string defaultStartN

        public AzureDataTableWrapper(string connectionString, string tableName)
        {
            this.connectionString = connectionString;
            this.tableName = tableName;
            tableServiceClient = new(connectionString);
        }

        private async Task InitializeTableClient()
        {
            if (tableClient is null)
            {
                // Check if table exists
                bool exists = false;
                await foreach (var tbl in tableServiceClient.QueryAsync(t => t.Name == tableName))
                {
                    exists = true;
                }

                // Get the table client. Create the table if it doesn't exists
                tableClient = tableServiceClient.GetTableClient(tableName);
                await tableClient.CreateIfNotExistsAsync();

                if (!exists)
                {
                    // Store "metadata" for no of lines, etc
                    TableEntity entity = new(mainPartionKey, tableInformationRowKey)
                    {
                        { "LineCount", 0 },
                        { "LastEntryKey", "" }
                    };
                    await tableClient.AddEntityAsync(entity);
                }
                
            }
        }

        public async Task<AsyncPageable<TEntity>> QueryAllAsync(string partitionKey = null)
        {
            await InitializeTableClient();
            AsyncPageable<TEntity> queryResultsFilter;
            if (partitionKey is not null)
            {
                queryResultsFilter = tableClient.QueryAsync<TEntity>(filter: $"(PartitionKey eq '{partitionKey}') and (RowKey ne '{tableInformationRowKey}')");
            } else
            {
                queryResultsFilter = tableClient.QueryAsync<TEntity>(filter: $"(RowKey ne '{tableInformationRowKey}')");
            }
            return queryResultsFilter;
        }

        public async Task<AsyncPageable<TEntity>> QueryWithFilterAsync(string filters)
        {
            AsyncPageable<TEntity> queryResults;
            await InitializeTableClient();
            if (string.IsNullOrWhiteSpace(filters))
            {
                queryResults = tableClient.QueryAsync<TEntity>(filter: $"(RowKey ne '{tableInformationRowKey}')");
            }
            else
            {
                queryResults = tableClient.QueryAsync<TEntity>(filter: $"{filters} and (RowKey ne '{tableInformationRowKey}')");
            }
            
            return queryResults;
        }

        public async Task<TEntity> GetAsync(string partitionKey, string rowKey)
        {
            await InitializeTableClient();
            if (string.IsNullOrEmpty(partitionKey))
                partitionKey = mainPartionKey;

            var queryResultsFilter = await tableClient.GetEntityAsync<TEntity>(
                rowKey: rowKey,
                partitionKey: partitionKey
            );

            return queryResultsFilter;
        }

        public async Task<TEntity> AddOrUpdateAsync(TEntity entity)
        {
            await InitializeTableClient();
            var entityReturn = await tableClient.UpsertEntityAsync(entity);
            //return entityReturn;
            return entity;
        }
        public async Task<TEntity> InsertAsync(TEntity entity, bool autoIncreaseRowKey = false)
        {
            await InitializeTableClient();

            // TODO: Implement Batch-insert to create a transaction in case of any error
            TableInformation informationEntity = await tableClient.GetEntityAsync<TableInformation>(rowKey: tableInformationRowKey, partitionKey: mainPartionKey);
            if (informationEntity is not null)
            {
                if (autoIncreaseRowKey)
                    entity.RowKey = IncreaseStringNumber(informationEntity.LastRowKey, 1);
                informationEntity.LineCount += 1;
                informationEntity.LastRowKey = entity.RowKey;
                await tableClient.UpsertEntityAsync(informationEntity);
            }

            if (string.IsNullOrWhiteSpace(entity.PartitionKey))
                entity.PartitionKey = mainPartionKey;
            var entityReturn = await tableClient.AddEntityAsync(entity);
            //return entityReturn;
            return entity;
        }
        public async Task<Response> UpdateAsync(TEntity entity)
        {
            await InitializeTableClient();
            var entityReturn = await tableClient.UpdateEntityAsync(entity, entity.ETag);
            return entityReturn;
        }

        public async Task<Response> DeleteAsync(string rowKey, string partitionKey)
        {
            await InitializeTableClient();
            var entity = await tableClient.DeleteEntityAsync(rowKey: rowKey, partitionKey: partitionKey);
            return entity;
        }

        private string IncreaseStringNumber(string s, int increaseBy)
        {
            string numberString = string.Empty;
            string restOfString = string.Empty;
            int val;

            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]))
                    numberString += s[i];
                else
                    restOfString += s[i];
            }

            if (numberString.Length == 0)
                return s;

            val = int.Parse(numberString) + increaseBy;

            return restOfString + val.ToString().PadLeft((s.Length - restOfString.Length), '0');
        }
    }
}
