namespace UserApi
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RegisteredUserDto
    {
        public string UserToken;
        public UserInfo UserInfo { get; set; }
    }
}