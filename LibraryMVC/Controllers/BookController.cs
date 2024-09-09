using AutoMapper;
using LibraryMVC.Dtos.Books;
using LibraryMVC.Interfaces;
using LibraryMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookAddEditDto book)
        {
            if (ModelState.IsValid)
            {
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