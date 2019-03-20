using SamlSSO.Models;

namespace SamlSSO.Services
{
    public class UserService
    {
        private static User _storedUser = new User
        {
            Username = "mstatz@pragmaticcoder.com",
            SSOToken = null
        };

        public static User Get(string username)
        {
            if (username == "mstatz@pragmaticcoder.com")
            {
                return _storedUser;
            }
            return null;
        }

        public static void Update(User user)
        {
            _storedUser.Username = user.Username;
            _storedUser.SSOToken = user.SSOToken;
        }
    }
}