using AutoMapper;
using BookStore_MVC.Models;
using BookStore_MVC.ViewModels;

namespace BookStore_MVC.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
