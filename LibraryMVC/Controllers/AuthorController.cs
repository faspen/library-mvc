using AutoMapper;
using LibraryMVC.Dtos.Authors;
using LibraryMVC.Interfaces;
using LibraryMVC.Models;
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

        [HttpGet]
        public IActionResult Index()
        {
            var entities = _repository.GetAuthors();
            var authors = _mapper.Map<List<AuthorDto>>(entities);
            return View(authors);
        }

        [HttpGet]
        public IActionResult GetAuthor(int authorId)
        {
            var entity = _repository.GetAuthor(authorId);
            var author = _mapper.Map<AuthorDto>(entity);
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AuthorAddEditDto author)
        {
            if (ModelState.IsValid) 
            {
                var entity = _mapper.Map<Author>(author);
                var result = _repository.CreateAuthor(entity);

                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong while saving.");
                    return StatusCode(500, ModelState);
                }
            }
            else
            {
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = _repository.GetAuthor(id);
            if (author == null)
            {
                return View("Error");
            }

            var dto = _mapper.Map<AuthorAddEditDto>(author);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AuthorAddEditDto dto)
        {
            if (ModelState.IsValid)
            {
                if (!_repository.AuthorExists(dto.Id))
                {
                    return View("Error");
                }

                var author = _mapper.Map<Author>(dto);
                var result = _repository.UpdateAuthor(author);

                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return StatusCode(500, ModelState);
                }
            }

            return View("Error");
        }
    }
}