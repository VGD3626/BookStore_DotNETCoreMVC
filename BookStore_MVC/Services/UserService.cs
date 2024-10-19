﻿using System.Security.Claims;

namespace BookStore_MVC.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
