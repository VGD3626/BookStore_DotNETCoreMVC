using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore_MVC.Data;
using BookStore_MVC.Enums;
using BookStore_MVC.Models;
using BookStore_MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Login Actions
        [HttpGet("login")]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserViewModel user, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var result = await _userRepository.SignInUser(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is invalid");
                return View(user);
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        // Signup Actions
        [HttpGet("signup")]
        public ViewResult Signup()
        {
            return View();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(CreateUserViewModel newUser)
        {
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }

            var result = await _userRepository.CreateUser(newUser);
            if (!result.Succeeded)
            {
                foreach (var errorMessage in result.Errors)
                {
                    ModelState.AddModelError("", errorMessage.Description);
                }
                return View(newUser);
            }

            result = await _userRepository.AssignUserRoles(newUser.Email, new List<string> { "Normal User" });
            if (!result.Succeeded)
            {
                foreach (var errorMessage in result.Errors)
                {
                    ModelState.AddModelError("", errorMessage.Description);
                }
                return View(newUser);
            }

            ModelState.Clear();

            await _userRepository.SignInUser(new LoginUserViewModel
            {
                Email = newUser.Email,
                Password = newUser.Password
            });

            return RedirectToAction("Index", "Home", new { userStatus = UserStatus.NewUser });
        }

        // Logout Action
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userRepository.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // Change Password Actions
        [HttpGet("change-password")]
        public ViewResult ChangePassword()
        {
            return View();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(changePasswordViewModel);
            }

            var result = await _userRepository.ChangePassword(changePasswordViewModel);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(changePasswordViewModel);
            }

            ModelState.Clear();
            ViewBag.Success = true;
            return View();
        }

        // User Management Actions
        [Authorize(Roles = "ADMIN")]
        //[HttpGet("admin/users")]
        public async Task<IActionResult> ShowUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var userViewModels = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            });

            return View(userViewModels);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("admin/users/edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UpdateUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return View(model);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("admin/users/edit")]
        public async Task<IActionResult> Edit(UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userRepository.UpdateUserAsync(model);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            return RedirectToAction("ShowUsers");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("admin/users/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("admin/users/delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var result = await _userRepository.DeleteUserAsync(id);
            if (!result.Succeeded)
            {
                // Handle errors if needed
            }

            return RedirectToAction("ShowUsers");
        }

        // Admin Dashboard
        [Authorize(Roles = "ADMIN")]
        [HttpGet("admin/dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
