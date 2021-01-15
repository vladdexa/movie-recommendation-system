using System;
using System.Text.RegularExpressions;

namespace UserApi
{
    public class User
    {
        public static int PasswordLowerBoundConstraint { get; } = 6;

        public static int PasswordUpperBoundConstraint { get; } = 16;

        public static Regex EmailPattern { get; } = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public string UserId { get; set; } = Guid.NewGuid().ToString();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}