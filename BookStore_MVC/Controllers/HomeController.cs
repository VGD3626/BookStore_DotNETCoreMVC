using BookStore_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using BookStore_MVC.Enums;

using System.Diagnostics;

namespace BookStore_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index(UserStatus userStatus = UserStatus.NormalUser)
        {
            ViewBag.userStatus = userStatus;
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
