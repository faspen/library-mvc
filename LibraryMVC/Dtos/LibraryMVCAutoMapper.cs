using AutoMapper;
using LibraryMVC.Dtos.Authors;
using LibraryMVC.Models;

namespace LibraryMVC.Dtos
{
    public class LibraryMVCAutoMapper : Profile
    {
        public LibraryMVCAutoMapper()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorAddEditDto, Author>();
        }
    }
}