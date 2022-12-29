using Azure.Data.Tables;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data
{
    //public interface IAzureDataTableService<TEntity> where TEntity : class, ITableEntity, new()
    //{
    //    Task<AsyncPageable<TEntity>> QueryAllAsync(string tableName, string partitionKey = null);
    //    Task<AsyncPageable<TEntity>> QueryWithFilterAsync(string tableName, string filters);
    //    Task<TEntity> GetAsync(string tableName, string partitionKey, string rowKey);

    //    Task<Response> AddOrUpdateAsync(string tableName, TEntity entity);
    //    Task<Response> InsertAsync(string tableName, TEntity entity);
    //    Task<Response> UpdateAsync(string tableName, TEntity entity);
    //    Task<Response> DeleteAsync(string tableName, string partitionKey, string rowKey);
    //}

    //public class AzureDataTableService<TEntity> : IAzureDataTableService<TEntity> where TEntity : class, ITableEntity, new()
    //{
    //    private TableServiceClient tableServiceClient = null;
    //    private readonly string connectionString;

    //    public AzureDataTableService(string connectionString, string tableName)
    //    {
    //        this.connectionString = connectionString;
    //    }

    //    private async Task<TableClient> GetTableClient(string tableName)
    //    {
    //        tableServiceClient ??= new TableServiceClient(connectionString);
    //        TableClient tableClient = tableServiceClient.GetTableClient(tableName);
    //        await tableClient.CreateIfNotExistsAsync();
    //        return tableClient;
    //    }

    //    public async Task<AsyncPageable<TEntity>> QueryAllAsync(string tableName, string partitionKey = null)
    //    {
    //        var tableClient = await GetTableClient(tableName);
    //        AsyncPageable<TEntity> queryResultsFilter;
    //        if (partitionKey is not null)
    //        {
    //            queryResultsFilter = tableClient.QueryAsync<TEntity>(filter: $"PartitionKey eq '{partitionKey}'");
    //        }
    //        else
    //        {
    //            queryResultsFilter = tableClient.QueryAsync<TEntity>();
    //        }
    //        return queryResultsFilter;
    //    }

    //    public async Task<AsyncPageable<TEntity>> QueryWithFilterAsync(string tableName, string filters)
    //    {
    //        var tableClient = await GetTableClient(tableName);

    //        AsyncPageable<TEntity> queryResults = tableClient.QueryAsync<TEntity>(filter: $"{filters}");
    //        return queryResults;
    //    }

    //    public async Task<TEntity> GetAsync(string tableName, string partitionKey, string rowKey)
    //    {
    //        var tableClient = await GetTableClient(tableName);

    //        var queryResultsFilter = await tableClient.GetEntityAsync<TEntity>(
    //            rowKey: rowKey,
    //            partitionKey: partitionKey
    //        );

    //        return queryResultsFilter;
    //    }

    //    public async Task<Response> AddOrUpdateAsync(string tableName, TEntity entity)
    //    {
    //        var tableClient = await GetTableClient(tableName);
    //        var entityReturn = await tableClient.UpsertEntityAsync(entity);
    //        return entityReturn;
    //    }
    //    public async Task<Response> InsertAsync(string tableName, TEntity entity)
    //    {
    //        var tableClient = await GetTableClient(tableName);
    //        var entityReturn = await tableClient.AddEntityAsync(entity);
    //        return entityReturn;
    //    }
    //    public async Task<Response> UpdateAsync(string tableName, TEntity entity)
    //    {
    //        var tableClient = await GetTableClient(tableName);
    //        var entityReturn = await tableClient.UpdateEntityAsync(entity, entity.ETag);
    //        return entityReturn;
    //    }

    //    public async Task<Response> DeleteAsync(string tableName, string rowKey, string partitionKey)
    //    {
    //        var tableClient = await GetTableClient(tableName);
    //        var entity = await tableClient.DeleteEntityAsync(rowKey: rowKey, partitionKey: partitionKey);
    //        return entity;
    //    }
    //}
}
