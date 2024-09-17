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
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while saving the new Author.");
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

        [HttpDelete]
        public IActionResult Delete(int genreId)
        {
            if (!_repository.GenreExists(genreId))
                return View("Error");
            
            var genreToDelete = _repository.GetGenre(genreId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (!_repository.DeleteGenre(genreToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting!");
                return StatusCode(500, ModelState);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}