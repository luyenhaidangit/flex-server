namespace Flex.Core.Models.Identity
{
    public class UserInfo
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public UserInfo(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }
    }
}
