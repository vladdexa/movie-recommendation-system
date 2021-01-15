using System.Threading.Tasks;

namespace UserApi
{
    public interface IUserRepository
    {
        public Task<UserTableEntity> CreateUser(UserTableEntity userTableEntity);

        public Task<UserTableEntity> GetUser(string userId);

        public Task<UserTableEntity> GetUserByEmail(string email);

        public Task<UserTableEntity> UpdateUser(UserTableEntity userTableEntity);

        public Task DeleteUser(UserTableEntity userTableEntity);
    }
}