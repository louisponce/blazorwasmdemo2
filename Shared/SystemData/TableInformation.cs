using Azure;
using Azure.Data.Tables;

namespace SharedLibrary.SystemData
{
    public class TableInformation: BaseEntity, ITableEntity
    {
        public int LineCount { get; set; }
        public string LastRowKey { get; set; }
    }
}
