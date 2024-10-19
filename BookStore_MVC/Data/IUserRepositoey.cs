using Microsoft.AspNetCore.Identity;
using BookStore_MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore_MVC.Models;

namespace BookStore_MVC.Data
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUser(CreateUserViewModel newUser);
        Task<SignInResult> SignInUser(LoginUserViewModel user);
        Task SignOut();
        Task<IdentityResult> AssignUserRoles(string userEmail, IEnumerable<string> roles);
        Task<IEnumerable<string>> GetUserRoles(string userId);
        Task<IdentityResult> ChangePassword(ChangePasswordViewModel model);

        // New Methods
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string userId);
        Task<IdentityResult> UpdateUserAsync(UpdateUserViewModel model);
        Task<IdentityResult> DeleteUserAsync(string userId);
    }
}
