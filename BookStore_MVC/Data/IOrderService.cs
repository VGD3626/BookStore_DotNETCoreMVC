using BookStore_MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore_MVC.Data
{
    public interface IOrderService
    {
        Task<bool> ProcessOrderAsync(CheckoutViewModel model, string userId);
        Task<IList<BookViewModel>> GetCartItemsAsync(string userId);
    }
}
