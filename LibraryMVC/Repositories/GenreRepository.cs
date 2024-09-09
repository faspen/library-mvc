using LibraryMVC.Interfaces;
using LibraryMVC.Models;

namespace LibraryMVC.Controllers
{
    public class GenreRepository : IGenreRepository
    {
        private readonly LibraryMVCDbContext _context;

        public GenreRepository(LibraryMVCDbContext context)
        {
            _context = context;
        }

        public bool CreateGenre(Genre genre)
        {
            _context.Add(genre);
            return Save();
        }

        public bool DeleteGenre(Genre genre)
        {
            _context.Remove(genre);
            return Save();
        }

        public bool GenreExists(int genreId)
        {
            return _context.Genres.Any(x => x.Id == genreId);
        }

        public Genre GetGenre(int genreId)
        {
            return _context.Genres.Where(x => x.Id == genreId).FirstOrDefault();
        }

        public ICollection<Genre> GetGenres()
        {
            return _context.Genres.OrderBy(x => x.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateGenre(Genre genre)
        {
            _context.Update(genre);
            return Save();
        }
    }
}