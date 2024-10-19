using BookStore_MVC.Data;
using BookStore_MVC.Models;
using BookStore_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;


namespace BookStore_MVC.Controllers.API
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBooks()
        {
            IEnumerable<Book> books = await _bookRepository.GetAllBooks();
            return Ok(_mapper.Map<IEnumerable<BookViewModel>>(books));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookById(int id)
        {
            Book book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            return Ok(_mapper.Map<BookViewModel>(book));
        }
    }
}
