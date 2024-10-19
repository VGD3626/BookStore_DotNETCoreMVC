
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore_MVC.Models
{
    [Table("CartItems")]
    public class CartItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        // BookId is correctly an int
        public int BookId { get; set; }
        public int Quantity { get; set; }

        // Navigation property
        public virtual Book Book { get; set; }
    }
}