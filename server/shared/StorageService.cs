using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using shared.DTO;

namespace shared
{
    public interface IStorageService
    {
        CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString);
        Task<CloudTable> CreateTableAsync(string tableName);
        Task DeleteEntityAsync(string tableName, string partitionKey, string rowKey);
        Task<IList<T>> GetEntity<T>(string tableName, string id = null, string[] cols = null) where T : ITableEntity, new();
        Task<object> InsertOrMergeEntityAsync(CloudTable table, object entity);
        Task WriteTableData(object jsonData, string table);
    }

    /// <summary>
    /// Azure storage service for table crud actions
    /// </summary>
    public class StorageService : IStorageService
    {
        private string connString = "";

        CloudStorageAccount storageAccount;

        private readonly ILogger<StorageService> _logger;

        public StorageService(ILogger<StorageService> logger, IConfiguration configuration)
        {
            _logger = logger;
            connString = configuration["StorageConnectionString"];
            _logger.LogDebug("Using storage connection string:" + connString);

            // Retrieve storage account information from connection string.
            storageAccount = CreateStorageAccountFromConnectionString(connString);
        }

        public async Task WriteTableData(object jsonData, string table)
        {
            var storageTable = await CreateTableAsync(table);
            await InsertOrMergeEntityAsync(storageTable, jsonData);
        }

        public CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                _logger.LogDebug("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                _logger.LogDebug("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                
                throw;
            }

            return storageAccount;
        }

        public async Task<CloudTable> CreateTableAsync(string tableName)
        {

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(tableName);
            if (await table.CreateIfNotExistsAsync())
            {
                _logger.LogInformation("Created Table named: {0}", tableName);
            }
            else
            {
                _logger.LogDebug("Table {0} already exists", tableName);
            }

            return table;
        }

        public async Task<object> InsertOrMergeEntityAsync(CloudTable table, object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                // TODO use Entity Adapter

                //OperationContext operationContext = new OperationContext();

                // TableEntityAdapter<object> writeToTableStorage = new TableEntityAdapter<object>(entity);
                // var result = writeToTableStorage.WriteEntity(operationContext);

                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity as ITableEntity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                var insertedData = result.Result;

                if (result.RequestCharge.HasValue)
                {
                    _logger.LogDebug("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                }

                return result;
            }
            catch (StorageException e)
            {
                _logger.LogError(e.Message);
                
                throw;
            }
        }

        /// <summary>
        /// Retrieve all entities or a specific id in format name;email
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IList<T>> GetEntity<T>(string tableName, string id = null, string[] cols = null) where T : ITableEntity, new()
        {
            try
            {
                var table = await CreateTableAsync(tableName);

                if (string.IsNullOrEmpty(id))
                {
                    var query = new TableQuery<T>();
                    //query.SelectColumns = cols;
                    var items = table.ExecuteQuery(query);
                    return Enumerable.Cast<T>(items).ToList();// (List<T>)items;
                }
                else
                {
                    TableOperation retrieveOperation = TableOperation.Retrieve<T>(id.Split(';')[0], id.Split(';')[1]);
                    TableResult result = await table.ExecuteAsync(retrieveOperation);

                    if (result.Result != null)
                    {
                        _logger.LogDebug("Request Charge of Retrieve Operation: " + result.RequestCharge);
                        return new List<T> { (T)result.Result };
                    }
                    else
                    {
                        return new List<T>();
                    }

                }
            }
            catch (StorageException e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="deleteEntity"></param>
        /// <returns></returns>
        public async Task DeleteEntityAsync(string tableName, string partitionKey, string rowKey)
        {
            try
            {
                
                var table = await CreateTableAsync(tableName);
                var retreiveOperation = TableOperation.Retrieve(partitionKey, rowKey);
                TableResult retrieveResult = await table.ExecuteAsync(retreiveOperation);
                if (retrieveResult.Result == null)
                {
                    throw new NullReferenceException ("deleteEntity is null");
                }
                var deleteEntity = retrieveResult.Result as ITableEntity;
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                TableResult result = await table.ExecuteAsync(deleteOperation);

                if (result.RequestCharge.HasValue)
                {
                    _logger.LogDebug("Request Charge of Delete Operation: " + result.RequestCharge);
                }

            }
            catch (StorageException e)
            {
                _logger.LogError(e.Message);
                
                throw;
            }
        }
    }
}