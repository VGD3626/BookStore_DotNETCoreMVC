﻿using System.ComponentModel.DataAnnotations;

namespace BookStore_MVC.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        // Navigation Properties
        public ICollection<Book> Book { get; set; }
    }
}
