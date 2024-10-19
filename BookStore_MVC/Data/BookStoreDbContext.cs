using System;
using System.Collections.Generic;
using BookStore_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore_MVC.Data
{
    public class BookStoreDbContext : IdentityDbContext<User>
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gallery> BooksGalleries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                        .HasIndex(book => book.Title)
                        .IsUnique();

            modelBuilder.Entity<Language>()
                        .HasMany(l => l.Books)
                        .WithOne(b => b.Language)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                        .HasMany(b => b.Category)
                        .WithMany(c => c.Book);

            modelBuilder.Entity<Book>()
                        .HasMany(b => b.BookGallery)
                        .WithOne(p => p.Book)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                        .HasOne(b => b.User)
                        .WithMany() // Specify navigation property if it exists
                        .HasForeignKey(b => b.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
            .HasOne(c => c.Book) // Assuming you have a Book navigation property
            .WithMany() // Adjust based on your model
            .HasForeignKey(c => c.BookId);

            // Call your seed method if needed
            // modelBuilder.seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
