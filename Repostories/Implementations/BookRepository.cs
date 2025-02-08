using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Data;
using Project___ConsoleApp__Library_Management_Application_.Models;
using Project___ConsoleApp__Library_Management_Application_.Repostories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp__Library_Management_Application_.Repostories.Implementations
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly AppDbContext _appDbContext;

        public BookRepository()
        {
            _appDbContext = new AppDbContext();
        }

        public Book? GetByIdInclude(int id)
        {
            var bookInclude = _appDbContext.Books
                .Include(x => x.BookAuthors)
                .FirstOrDefault(x => x.Id == id);
            return bookInclude;
        }

        List<Book> IBookRepository.GetAllByInclude()
        {

            return _appDbContext.Books
                .Include(x => x.BookAuthors)
                .ToList();
        }
    }
}

