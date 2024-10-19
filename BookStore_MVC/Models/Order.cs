// Models/Order.cs
using System;
using System.Collections.Generic;

namespace BookStore_MVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string Address { get; set; }
        public string Status { get; set; } = "Pending";

        // Navigation Property
        public List<OrderItem> OrderItems { get; set; }
    }
}

// Models/OrderItem.cs
namespace BookStore_MVC.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation Properties
        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}
