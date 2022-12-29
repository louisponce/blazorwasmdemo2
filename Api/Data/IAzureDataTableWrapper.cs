using Azure;
using Azure.Data.Tables;
using System.Threading.Tasks;

namespace Api.Data
{
    public interface IAzureDataTableWrapper<TEntity> where TEntity : class, ITableEntity, new()
    {
        Task<AsyncPageable<TEntity>> QueryAllAsync(string partitionKey = null);
        Task<AsyncPageable<TEntity>> QueryWithFilterAsync(string filters);
        Task<TEntity> GetAsync(string partitionKey, string rowKey);

        Task<TEntity> AddOrUpdateAsync(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity, bool autoIncreaseRowKey = false);
        Task<Response> UpdateAsync(TEntity entity);
        Task<Response> DeleteAsync(string partitionKey, string rowKey);
    }
}
