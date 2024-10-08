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
            CreateMap<Author, AuthorAddEditDto>();

            CreateMap<Book, BookDto>();
            CreateMap<BookAddEditDto, Book>();
            CreateMap<Book, BookAddEditDto>();

            CreateMap<Genre, GenreDto>();
            CreateMap<GenreAddEditDto, Genre>();
            CreateMap<Genre, GenreAddEditDto>();
        }
    }
}