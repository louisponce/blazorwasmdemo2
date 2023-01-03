using Azure.Data.Tables;
using SharedLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class PeopleService // <Person> : IBaseInterface<Person> where Person : class, ITableEntity, new()
    {
        public readonly string personTableName = "People";
        public readonly string hobbyTableName = "Hobby";
        private readonly AzureDataTableWrapper<Person> personTableService;
        private readonly AzureDataTableWrapper<Hobby> hobbyTableService;
        private List<Hobby> hobbyList;
        public PeopleService(string connectionString)
        {
            personTableService = new(connectionString, personTableName);
            hobbyTableService = new(connectionString, hobbyTableName);
        }

        // Here we should implement Setfilter
        //
        //

        public async Task<List<Person>> GetEntries(string rowkey)
        {
            // Get hobbies
            await LoadHobbyList();

            List<Person> personList = new();
            if (string.IsNullOrEmpty(rowkey))
            {
                var data = await personTableService.QueryAllAsync();
                await foreach (var item in data)
                {
                    item.HobbyDescrition = GetHobbyDescription(item);
                    personList.Add(item);
                }
            }
            else
            {
                var item = await personTableService.GetAsync(string.Empty, rowkey);
                if (item is not null)
                {
                    item.HobbyDescrition = GetHobbyDescription(item);
                    personList.Add(item);
                }
            }

            return personList;
        }

        public async Task<Person> AddOrUpdateAsync(Person entity)
        {
            // Perform busines logic
            //
            Person person = new();
            await LoadHobbyList();

            // Save
            if (string.IsNullOrWhiteSpace(entity.RowKey))
            {
                person = await personTableService.InsertAsync(entity, autoIncreaseRowKey: true);
            }
            else
            {
                person = await personTableService.AddOrUpdateAsync(entity);
            }
            person.HobbyDescrition = GetHobbyDescription(person);
            return person;
        }

        public async Task DeleteAsync(string rowkey, string partitionkey)
        {
            await personTableService.DeleteAsync(rowkey, partitionkey);
        }


        private async Task LoadHobbyList()
        {
            hobbyList = new();
            var hobbyData = await hobbyTableService.QueryAllAsync();
            await foreach (var item in hobbyData)
            {
                hobbyList.Add(item);
            }
        }

        private string GetHobbyDescription(Person item)
        {
            // Add the hobbby description
            var hobby = hobbyList.Where(obj => obj.RowKey == item.HobbyCode).ToList();
            if (hobby.Count > 0)
                return hobby[0].Description;
            else
                return string.Empty;
        }
    }
}
