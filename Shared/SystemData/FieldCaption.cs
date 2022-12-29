using Azure;
using Azure.Data.Tables;

namespace SharedLibrary.SystemData
{
    public class FieldCaption : ITableEntity
    {
        public string? TableName { get; set; }
        public string? FieldName { get; set; }
        public string? LanguageCode { get; set; }
        public string? Caption { get; set; }


        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
