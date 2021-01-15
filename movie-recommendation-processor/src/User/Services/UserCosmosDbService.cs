using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Table;

namespace UserApi
{
    public class UserCosmosDbService : IUserRepository
    {
        private readonly CloudTable _table;

        public UserCosmosDbService()
        {
            var connectionString = Environment.GetEnvironmentVariable("COSMOS_DB_DEV_CONNECTION");

            if (string.IsNullOrEmpty(connectionString)) throw new Exception("Please provide credentials");

            var tableName = Environment.GetEnvironmentVariable("USERS_TABLE_NAME");

            if (string.IsNullOrEmpty(tableName)) throw new Exception("Please provide the table name");

            var storageAccount = CloudStorageAccount.Parse(connectionString);

            var tableClient = storageAccount.CreateCloudTableClient();

            var table = tableClient.GetTableReference(tableName);

            _table = table;
        }

        public async Task<UserTableEntity> CreateUser(UserTableEntity userTableEntity)
        {
            try
            {
                return (await _table.ExecuteAsync(TableOperation.Insert(userTableEntity))).Result as UserTableEntity;
            }
            catch (CosmosException error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<UserTableEntity> GetUser(string userId)
        {
            try
            {
                return (await _table.ExecuteAsync(TableOperation.Retrieve<UserTableEntity>("USER", userId))).Result as
                    UserTableEntity;
            }
            catch (CosmosException error)
            {
                if (error.StatusCode == HttpStatusCode.NotFound)
                    throw new Exception(UserException.AuthorizeUserExceptions[UserExceptionType.UserNotFound]);

                throw new Exception(error.Message);
            }
        }

        public async Task<UserTableEntity> GetUserByEmail(string email)
        {
            try
            {
                var users = _table.CreateQuery<UserTableEntity>().Where(userTableEntity =>
                        userTableEntity.PartitionKey == "USER" && userTableEntity.Email == email)
                    .Select(userTableEntity => new UserTableEntity
                    {
                        PartitionKey = userTableEntity.PartitionKey,
                        RowKey = userTableEntity.RowKey,
                        FirstName = userTableEntity.FirstName,
                        LastName = userTableEntity.LastName,
                        Email = userTableEntity.Email,
                        Password = userTableEntity.Password
                    }).ToList();

                return users.Any() ? users.First() : null;
            }
            catch (CosmosException error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<UserTableEntity> UpdateUser(UserTableEntity userTableEntity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUser(UserTableEntity userTableEntity)
        {
            throw new NotImplementedException();
        }
    }
}