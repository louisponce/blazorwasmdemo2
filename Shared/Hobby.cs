using Azure.Data.Tables;
using Azure;


namespace SharedLibrary
{
    public class Hobby : BaseEntity, ITableEntity
    {
        public string? Description { get; set; } = string.Empty;
    }
}
