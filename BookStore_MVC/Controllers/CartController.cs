using Microsoft.AspNetCore.Mvc;
using BookStore_MVC.Services;
using BookStore_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using BookStore_MVC.Models;
using System.Linq;
using BookStore_MVC.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class CartController : Controller
{
    private readonly IOrderService _orderService;
    private readonly UserManager<User> _userManager;
    private readonly BookStoreDbContext _context;

    public CartController(IOrderService orderService, UserManager<User> userManager, BookStoreDbContext context)
    {
        _orderService = orderService;
        _userManager = userManager;
        _context = context;
    }

    [HttpGet("Card/Index")]
    public async Task<IActionResult> Index() // This method should show the user's cart
    {
        // Get the logged-in user ID
        var userId = _userManager.GetUserId(User);

        // Fetch the cart items from the database
        var cartItems = await _context.CartItems
            .Include(ci => ci.Book) // Include the book details
            .Where(ci => ci.UserId == userId)
            .Select(ci => new BookViewModel
            {
                Id = ci.Book.Id,
                Title = ci.Book.Title,
                Author = ci.Book.Author,
                Price = ci.Book.Price,
                CoverPhotoPath = ci.Book.CoverPhotoPath,
                Description = ci.Book.Description
            })
            .ToListAsync();

        return View(cartItems);
    }


    [HttpGet("Cart/Checkout")]
    public async Task<IActionResult> Checkout()
    {
        var userId = _userManager.GetUserId(User);
        var cartItems = await _orderService.GetCartItemsAsync(userId);

        if (cartItems == null || !cartItems.Any())
        {
            TempData["ErrorMessage"] = "Your cart is empty.";
            return RedirectToAction("Index");
        }

        var model = new CheckoutViewModel
        {
            CartItems = cartItems.ToList(), // Ensure this matches your BookViewModel
        };

        return View(model);
    }





    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = _userManager.GetUserId(User);
            // Process the order
            bool isPurchaseSuccessful = await _orderService.ProcessOrderAsync(model, userId);

            if (isPurchaseSuccessful)
            {
                TempData["SuccessMessage"] = "Your order has been placed successfully!";
                return RedirectToAction("OrderConfirmation");
            }

            ModelState.AddModelError("", "There was an issue with your order. Please try again.");
        }

        return View(model); // If the model is invalid, return the same view with the current model to show errors
    }


    [HttpGet]
    public IActionResult OrderConfirmation()
    {
        return View();
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var userId = _userManager.GetUserId(User);

        // Find the cart item associated with the user and the book id
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.BookId == id);

        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem); // Remove the cart item
            await _context.SaveChangesAsync(); // Save changes to the database
        }

        return RedirectToAction("Index"); // Redirect back to the cart view
    }
}
