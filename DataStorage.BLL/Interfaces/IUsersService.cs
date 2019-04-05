using System.Threading.Tasks;
using DataStorage.BLL.DTOs;
using Microsoft.AspNetCore.Identity;

namespace DataStorage.BLL.Interfaces
{
    public interface IUsersService
    {
        Task<SignInResult> SignInUserAsync(string userEmail, string userPassword, bool rememberMe);
        Task<IdentityResult> CreateUserAsync(string userEmail, string userPassword);
        Task<IdentityResult> ConfirmEmailAsync(UserDTO user, string token);
        Task<UserDTO> GetUserByNameAsync(string userEmail);
        Task<UserDTO> GetUserByIdAsync(string userId);
        Task<string> GetEmailTokenAsync(UserDTO user);
        Task<bool> IsEmailConfirmedAsync(UserDTO user);
        //void LogOut();
    }
}