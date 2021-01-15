using Microsoft.AspNetCore.Http;
using UserApi;

namespace User.Config.Utils
{
    public interface IJwt
    {
        public string Encode(UserInfo userInfo);

        public JwtToken Decode(HttpRequest request);
    }
}