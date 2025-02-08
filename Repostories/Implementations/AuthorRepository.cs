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
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly AppDbContext _appDbContext;
        public AuthorRepository()
        {
            _appDbContext = new AppDbContext();
        }
        public List<Author> GetAllByInclude()
        {
            return _appDbContext.Authors
                .Include(x => x.BookAuthors)
                .ToList();
        }

        public Author? GetByIdInclude(int id)
        {
            var authorInclude = _appDbContext.Authors
                .Include(x => x.BookAuthors)
                .FirstOrDefault(x => x.Id == id);
            return authorInclude;
        }
    }
}
