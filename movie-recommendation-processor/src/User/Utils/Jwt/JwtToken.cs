using UserApi;

namespace User.Config.Utils
{
    public class JwtToken
    {
        public UserInfo UserInfo { get; set; }

        public long ExpirationTime { get; set; }
    }
}