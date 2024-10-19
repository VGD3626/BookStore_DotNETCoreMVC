using AutoMapper;
using BookStore_MVC.Models;
using BookStore_MVC.Services;
using BookStore_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BookStore_MVC.Data
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SqlUserRepository(UserManager<User> userManager, SignInManager<User> signInManager,
                                IUserService userService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(_userService.GetUserId());
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            return result;
        }

        public async Task<IdentityResult> CreateUser(CreateUserViewModel newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.UserName = newUser.FirstName + newUser.LastName;

            var result = await _userManager.CreateAsync(user, newUser.Password);
            return result;
        }

        public async Task<IdentityResult> AssignUserRoles(string userEmail, IEnumerable<string> roles)
        {
            User user = await _userManager.FindByEmailAsync(userEmail);
            var result = await _userManager.AddToRolesAsync(user, roles);
            return result;
        }

        public async Task<IEnumerable<string>> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                // Handle the case where the user was not found
                return Enumerable.Empty<string>(); // Return an empty list of roles
            }

            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }


        public async Task<SignInResult> SignInUser(LoginUserViewModel user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser == null)
            {
                return SignInResult.Failed;
            }
            var result = await _signInManager.PasswordSignInAsync(existingUser.UserName, user.Password, user.RememberMe, false);
            return result;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        // New Methods Implementations

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await Task.FromResult(_userManager.Users.ToList()); // Fetch all users
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId); // Fetch user by ID
        }

        public async Task<IdentityResult> UpdateUserAsync(UpdateUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            user.UserName = model.UserName; // Update user properties
            user.Email = model.Email;

            return await _userManager.UpdateAsync(user); // Update user in the database
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            return await _userManager.DeleteAsync(user); // Delete user from the database
        }
    }
}
