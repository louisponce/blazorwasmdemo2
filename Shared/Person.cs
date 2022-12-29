using System;
using System.Runtime.Serialization;
using Azure;
using Azure.Data.Tables;

namespace SharedLibrary
{
    public class Person: BaseEntity, ITableEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? HobbyCode { get; set; }
        [IgnoreDataMember]
        public string? HobbyDescrition { get; set; }
    }
}
