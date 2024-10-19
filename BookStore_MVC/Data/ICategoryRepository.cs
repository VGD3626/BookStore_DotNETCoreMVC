using BookStore_MVC.Models;

namespace BookStore_MVC.Data
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<IEnumerable<Category>> GetCategoriesById(IEnumerable<int> ids);
    }
}
