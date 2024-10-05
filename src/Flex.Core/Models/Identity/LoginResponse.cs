namespace Flex.Core.Models.Identity
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public int ExpiresIn { get; set; }

        public UserInfo UserInfo { get; set; }

        public LoginResponse(string accessToken, int expiresIn,UserInfo userInfo, string tokenType = "Bearer")
        {
            AccessToken = accessToken;
            TokenType = tokenType;
            ExpiresIn = expiresIn;
            UserInfo = userInfo;
        }
    }
}
