using LibraryMVC.Interfaces;
using LibraryMVC.Models;

namespace LibraryMVC.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryMVCDbContext _context;

        public BookRepository(LibraryMVCDbContext context)
        {
            _context = context;
        }

        public bool BookExists(int bookId)
        {
            return _context.Books.Any(x => x.Id == bookId);
        }

        public bool CreateBook(Book book)
        {
            _context.Add(book);
            return Save();
        }

        public bool DeleteBook(Book book)
        {
            _context.Remove(book);
            return Save();
        }

        public Book GetBook(int bookId)
        {
            return _context.Books.Where(x => x.Id == bookId).FirstOrDefault();
        }

        public ICollection<Book> GetBooks()
        {
            return _context.Books.OrderBy(x => x.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBook(Book book)
        {
            _context.Update(book);
            return Save();
        }
    }
}