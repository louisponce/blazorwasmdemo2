using Azure;
using Azure.Data.Tables;
using SharedLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Data
{
    public interface IBaseInterface<TEntity> where TEntity : class, ITableEntity, new()
    {
        Task<List<TEntity>> GetEntries();
        Task AddOrUpdateAsync(TEntity entity);

        Task DeleteAsync(string rowkey, string partitionkey);
    }
}
