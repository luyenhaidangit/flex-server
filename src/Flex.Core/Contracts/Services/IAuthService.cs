using Flex.Core.Models.Identity;

namespace Flex.Core.Contracts.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);

        //bool Authenticate(string username, string password);
        //string GenerateToken(string username);
        //bool ValidateToken(string token);
        //string RefreshToken(string oldToken);
        //void RevokeToken(string token);
        //bool Register(string username, string password, string email);
        //bool ChangePassword(string username, string oldPassword, string newPassword);
        //bool ForgotPassword(string email);
        //bool ConfirmEmail(string email, string confirmationCode);
        //bool IsUsernameAvailable(string username);
        //bool IsEmailAvailable(string email);
        //void Logout(string username);
        //bool HasPermission(string token, string requiredPermission);
    }
}
