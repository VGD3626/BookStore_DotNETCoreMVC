using AutoMapper;
using BookStore_MVC.Models;
using BookStore_MVC.ViewModels;

namespace BookStore_MVC.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.CategoryNames, opt =>
                                    opt.MapFrom(src => src.Category.Select(c => c.Name)))
                                            .ForMember(dest => dest.CategoryIds, opt =>
                                    opt.MapFrom(src => src.Category.Select(c => c.Id)));
            CreateMap<BookViewModel, Book>();
        }
    }
}
