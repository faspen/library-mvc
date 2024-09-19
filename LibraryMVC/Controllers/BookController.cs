using AutoMapper;
using LibraryMVC.Dtos.Books;
using LibraryMVC.Interfaces;
using LibraryMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _repository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public BookController(
            IBookRepository repository, 
            IAuthorRepository authorRepository, 
            IGenreRepository genreRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var entities = _repository.GetBooks();
            var books = _mapper.Map<List<BookDto>>(entities);
            return View(books);
        }

        [HttpGet]
        public IActionResult GetBook(int bookId)
        {
            var entity = _repository.GetBook(bookId);
            var book = _mapper.Map<BookDto>(entity);
            return View(book);
        }

        public IActionResult Create()
        {
            var authors = _authorRepository.GetAuthors();
            var genres = _genreRepository.GetGenres();
            ViewBag.Authors = new SelectList(authors, "Id", "LastName");
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookAddEditDto book)
        {
            if (ModelState.IsValid)
            {
                if (book.GenreId < 1)
                {
                    book.GenreId = 0;
                }
                var entity = _mapper.Map<Book>(book);
                var saved = _repository.CreateBook(entity);
                if (saved)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while saving the new Author.");
                }
            }

            return View(book);
        }
    }
}