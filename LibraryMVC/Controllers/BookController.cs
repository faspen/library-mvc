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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _repository.GetBook(id);
            if (book == null)
            {
                return View("Error");
            }

            var dto = _mapper.Map<BookAddEditDto>(book);
            var authors = _authorRepository.GetAuthors();
            var genres = _genreRepository.GetGenres();
            ViewBag.Authors = new SelectList(authors, "Id", "LastName");
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookAddEditDto dto)
        {
            if (ModelState.IsValid)
            {
                if (!_repository.BookExists(dto.Id))
                {
                    return View("Error");
                }

                var book = _mapper.Map<Book>(dto);
                var result = _repository.UpdateBook(book);

                if (result)
                    return RedirectToAction("Index");
                else
                    return StatusCode(500, ModelState);
            }

            return View("Error");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = _repository.GetBook(id);
            if (book == null)
            {
                return View("Error");
            }

            var dto = _mapper.Map<BookAddEditDto>(book);
            var authors = _authorRepository.GetAuthors();
            var genres = _genreRepository.GetGenres();
            ViewBag.Authors = new SelectList(authors, "Id", "LastName");
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            return View(dto);
        }

        [HttpPost]
        public IActionResult Delete(BookAddEditDto book)
        {
            if (!_repository.BookExists(book.Id))
                return View("Error");
            
            var bookToDelete = _repository.GetBook(book.Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (!_repository.DeleteBook(bookToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deletign.");
                return StatusCode(500, ModelState);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}