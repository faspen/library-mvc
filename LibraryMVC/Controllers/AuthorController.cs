using AutoMapper;
using LibraryMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult GetAuthors()
        {
            var authors = _repository.GetAuthors();
            return View(authors);
        }

        public IActionResult GetAuthor(int authorId)
        {
            var author = _repository.GetAuthor(authorId);
            return View(author);
        }
    }
}