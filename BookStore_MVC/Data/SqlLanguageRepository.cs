using BookStore_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore_MVC.Data
{
    public class SqlLanguageRepository : ILanguageRepository
    {
        private readonly BookStoreDbContext _context;

        public SqlLanguageRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Language>> GetAllLanguages()
        {
            IEnumerable<Language> languages = await _context.Languages.OrderBy(l => l.Id).ToListAsync();
            return languages;
        }
    }
}
