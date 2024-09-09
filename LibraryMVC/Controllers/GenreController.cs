using AutoMapper;
using LibraryMVC.Dtos.Genres;
using LibraryMVC.Interfaces;
using LibraryMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;

        public GenreController(IGenreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var entities = _repository.GetGenres();
            var genres = _mapper.Map<List<GenreDto>>(entities);
            return View(genres);
        }

        [HttpGet]
        public IActionResult GetGenre(int genreId)
        {
            var entity = _repository.GetGenre(genreId);
            var genre = _mapper.Map<GenreDto>(entity);
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GenreAddEditDto genre)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Genre>(genre);
                var saved = _repository.CreateGenre(entity);
                if (saved)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while saving the new Author.");
                }
            }

            return View(genre);
        }
    }
}