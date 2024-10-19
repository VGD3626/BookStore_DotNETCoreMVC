using BookStore_MVC.Data;
using BookStore_MVC.Models;
using BookStore_MVC.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_MVC.Data
{
    public class OrderService : IOrderService
    {
        private readonly BookStoreDbContext _context;

        public OrderService(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ProcessOrderAsync(CheckoutViewModel model, string userId)
        {
            try
            {
                // Calculate the total amount while handling nullable prices
                var totalAmount = model.CartItems.Sum(item => item.Price);

                // Create a new order for the user
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount ?? 0m,
                    PaymentMethod = model.PaymentMethod,
                    Address = model.Address,
                    Status = "Pending"
                };

                // Add the order to the database
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Add each cart item as an order item
                foreach (var item in model.CartItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        BookId = item.Id,
                        Quantity = 1, // Assuming each cart item has a quantity of 1
                        Price = item.Price ?? 0m // Handle nullable price
                    };
                    _context.OrderItems.Add(orderItem);
                }

                // Save the order items to the database
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the error (add a logger if needed)
                // Example: _logger.LogError(ex, "Error processing order");
                return false;
            }
        }

        public async Task<IList<BookViewModel>> GetCartItemsAsync(string userId)
        {
            var cartItems = await _context.CartItems
                .Include(ci => ci.Book) // Include the book details
                .Where(ci => ci.UserId == userId)
                .Select(ci => new BookViewModel
                {
                    Id = ci.Book.Id, // Access the Id property of the Book model
                    Title = ci.Book.Title,
                    Author = ci.Book.Author,
                    Price = ci.Book.Price,
                    CoverPhotoPath = ci.Book.CoverPhotoPath,
                    Description = ci.Book.Description
                })
                .ToListAsync();

            return cartItems;
        }


    }
}
