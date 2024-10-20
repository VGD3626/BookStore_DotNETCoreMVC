﻿using System.ComponentModel.DataAnnotations;

namespace BookStore_MVC.Models
{
    public class Language
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        // Navigation Properties
        public ICollection<Book> Books { get; set; }
    }
}
