using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace UserApi
{
    public class Authorization
    {
        public static Func<OkObjectResult> Authorize = () => new OkObjectResult(new Authorization(true, null));

        public static Func<string, ObjectResult> Reject = message =>
            new ObjectResult(new Authorization(false, message))
                {StatusCode = (int) HttpStatusCode.Unauthorized};

        public readonly bool IsAuthorized;
#nullable enable
        public readonly string? Message;

        private Authorization(bool isAuthorized, string? message)
        {
            IsAuthorized = isAuthorized;
            if (message != null) Message = message;
        }

#nullable disable
    }
}