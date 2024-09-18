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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var genre = _repository.GetGenre(id);
            if (genre == null)
            {
                return View("Error");
            }

            var dto = _mapper.Map<GenreAddEditDto>(genre);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GenreAddEditDto dto)
        {
            if (ModelState.IsValid)
            {
                if (!_repository.GenreExists(dto.Id))
                {
                    return View("Error");
                }

                var genre = _mapper.Map<Genre>(dto);
                var result = _repository.UpdateGenre(genre);

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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var genre = _repository.GetGenre(id);
            if (genre == null)
            {
                return View("Error");
            }

            var dto = _mapper.Map<GenreAddEditDto>(genre);
            return View(dto);
        }

        [HttpPost]
        public IActionResult Delete(GenreAddEditDto genre)
        {
            if (!_repository.GenreExists(genre.Id))
                return View("Error");
            
            var genreToDelete = _repository.GetGenre(genre.Id);

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