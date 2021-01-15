using System;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using Microsoft.AspNetCore.Http;
using UserApi;

namespace User.Config.Utils
{
    public class Jwt : IJwt
    {
        private readonly IJwtEncoder _jwtEncoder;
        private readonly string _jwtKey;

        public Jwt()
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder base64Encoder = new JwtBase64UrlEncoder();
            _jwtEncoder = new JwtEncoder(algorithm, serializer, base64Encoder);
            _jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
        }

        public string Encode(UserInfo userInfo)
        {
            var payload = new JwtToken
            {
                UserInfo = userInfo,
                ExpirationTime = DateTime.Now.AddMinutes(60).Ticks
            };

            return _jwtEncoder.Encode(payload, _jwtKey);
        }

        public JwtToken Decode(HttpRequest request)
        {
            try
            {
                var token = GetAuthorizationTokenFromRequest(request);

                var payload = new JwtBuilder().WithAlgorithm(new HMACSHA256Algorithm()).WithSecret(_jwtKey)
                    .MustVerifySignature().Decode<JwtToken>(token);

                if (string.IsNullOrEmpty(payload.UserInfo.UserId)
                    || string.IsNullOrEmpty(payload.UserInfo.Email)
                    || string.IsNullOrEmpty(payload.UserInfo.FirstName)
                    || string.IsNullOrEmpty(payload.UserInfo.LastName)
                    || payload.ExpirationTime.Equals(null))
                    throw new Exception(
                        UserException.AuthorizeUserExceptions[UserExceptionType.UserAuthorizationRequestInvalidToken]);

                return payload;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private string GetAuthorizationTokenFromRequest(HttpRequest request)
        {
            if (!request.Headers.Keys.Contains("Authorization"))
                throw new Exception(
                    UserException.AuthorizeUserExceptions[UserExceptionType.UserAuthorizationRequestHeadersMissing]);

            string authorizationHeader = request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
                throw new Exception(
                    UserException.AuthorizeUserExceptions[UserExceptionType.UserAuthorizationRequestHeadersMissing]);

            if (!authorizationHeader.StartsWith("Bearer"))
                throw new Exception(
                    UserException.AuthorizeUserExceptions[UserExceptionType.UserAuthorizationRequestMissingBearer]);

            return authorizationHeader.Substring(6);
        }
    }
}