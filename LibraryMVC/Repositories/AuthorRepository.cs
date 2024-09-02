using LibraryMVC.Interfaces;
using LibraryMVC.Models;

namespace LibraryMVC.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryMVCDbContext _context;

        public AuthorRepository(LibraryMVCDbContext context)
        {
            _context = context;
        }

        public bool AuthorExists(int authorId)
        {
            return _context.Authors.Any(x => x.Id == authorId);
        }

        public bool CreateAuthor(Author author)
        {
            _context.Add(author);
            return Save();
        }

        public bool DeleteAuthor(Author author)
        {
            _context.Remove(author);
            return Save();
        }

        public Author GetAuthor(int authorId)
        {
            return _context.Authors.Where(x => x.Id == authorId).FirstOrDefault();
        }

        public ICollection<Author> GetAuthors()
        {
            return _context.Authors.OrderBy(x => x.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAuthor(Author author)
        {
            _context.Update(author);
            return Save();
        }
    }
}