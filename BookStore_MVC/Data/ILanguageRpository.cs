using BookStore_MVC.Models;

namespace BookStore_MVC.Data
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<Language>> GetAllLanguages();
    }
}
