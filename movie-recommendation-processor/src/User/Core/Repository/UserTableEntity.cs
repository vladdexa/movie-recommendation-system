using Microsoft.Azure.Cosmos.Table;

namespace UserApi
{
    public class UserTableEntity : TableEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}