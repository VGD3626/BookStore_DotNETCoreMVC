using AutoMapper;
using BookStore_MVC.Models;
using BookStore_MVC.ViewModels;

namespace BookStore_MVC.Profiles
{
    public class GalleryProfile : Profile
    {
        public GalleryProfile()
        {
            CreateMap<Gallery, GalleryViewModel>();
        }
    }
}
