using System;
using System.Runtime.Serialization;
using Azure;
using Azure.Data.Tables;

namespace SharedLibrary
{
    public class Person: BaseEntity, ITableEntity
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? HobbyCode { get; set; } = string.Empty;
        [IgnoreDataMember]
        public string? HobbyDescrition { get; set; } = string.Empty;
    }
}
