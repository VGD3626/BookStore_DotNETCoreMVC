using AutoMapper;
using BookStore_MVC.Models;
using BookStore_MVC.ViewModels;

namespace BookStore_MVC.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserViewModel, User>();
        }
    }
}
