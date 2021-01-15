namespace UserApi
{
    public static class UserMapper
    {
        public static UserTableEntity ToUserTableEntity(this User user)
        {
            return new UserTableEntity
            {
                PartitionKey = "USER",
                RowKey = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Email = user.Email
            };
        }

        public static User ToUser(this UserTableEntity userTableEntity)
        {
            return new User
            {
                UserId = userTableEntity.RowKey,
                FirstName = userTableEntity.FirstName,
                LastName = userTableEntity.LastName,
                Email = userTableEntity.Email,
                Password = userTableEntity.Password
            };
        }
    }
}