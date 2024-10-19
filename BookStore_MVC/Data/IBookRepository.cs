using BookStore_MVC.Models;

namespace BookStore_MVC.Data
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Book>> GetTopBooks(int count);
        Task<IEnumerable<Book>> GetSimilarBooks(int bookId, IEnumerable<string> categories, int count);
        Task<IEnumerable<Book>> GetLibrary(string userId);
        Task<Book> GetBookById(int id);
        Task DeleteBook(Book book);
        Task<bool> CheckBookName(string name, int id = 0);
        Task<int> AddBook(Book newBook);
        Task SaveUpdates();
        Task<IEnumerable<Book>> SearchByTitle(string bookTitle);
        Task<IEnumerable<Book>> SearchByCategory(string bookCategory);
        Task<IEnumerable<Book>> SearchByAuthor(string bookAuthor);
    }
}
