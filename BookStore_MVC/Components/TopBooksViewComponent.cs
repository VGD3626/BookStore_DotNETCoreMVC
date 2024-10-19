using AutoMapper;
using BookStore_MVC.Data;
using BookStore_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_MVC.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public TopBooksViewComponent(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await _bookRepository.GetTopBooks(count);
            return View(_mapper.Map<IEnumerable<BookViewModel>>(books));
        }
    }
}
