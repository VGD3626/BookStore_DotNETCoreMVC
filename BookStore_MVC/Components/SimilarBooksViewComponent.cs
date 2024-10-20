﻿using AutoMapper;
using BookStore_MVC.Data;
using BookStore_MVC.Models;
using BookStore_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_MVC.Components
{
    public class SimilarBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public SimilarBooksViewComponent(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int bookId, IEnumerable<string> categories, int count = 5)
        {
            IEnumerable<Book> books = await _bookRepository.GetSimilarBooks(bookId, categories, count);
            return View(_mapper.Map<IEnumerable<BookViewModel>>(books));
        }
    }
}
