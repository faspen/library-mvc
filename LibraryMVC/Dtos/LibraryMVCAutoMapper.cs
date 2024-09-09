using AutoMapper;
using LibraryMVC.Dtos.Authors;
using LibraryMVC.Dtos.Books;
using LibraryMVC.Dtos.Genres;
using LibraryMVC.Models;

namespace LibraryMVC.Dtos
{
    public class LibraryMVCAutoMapper : Profile
    {
        public LibraryMVCAutoMapper()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorAddEditDto, Author>();

            CreateMap<Book, BookDto>();
            CreateMap<BookAddEditDto, Book>();

            CreateMap<Genre, GenreDto>();
            CreateMap<GenreAddEditDto, Genre>();
        }
    }
}