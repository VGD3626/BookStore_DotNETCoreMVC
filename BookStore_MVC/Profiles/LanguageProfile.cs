using AutoMapper;
using BookStore_MVC.Models;
using BookStore_MVC.ViewModels;

namespace BookStore_MVC.Profiles
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            CreateMap<Language, LanguageViewModel>();
        }
    }
}
