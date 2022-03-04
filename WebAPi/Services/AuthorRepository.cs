using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPi.Context;
using WebAPi.Entities;

namespace WebAPi.Services
{
    public class AuthorRepository : IRepository<Author>
    {
        private readonly BookContext _bookContext;

        public AuthorRepository(BookContext bookContext)
        {
            _bookContext = bookContext ?? throw new ArgumentNullException(nameof(bookContext));
        }

        public void AddEntity(Author author)
        {
            _bookContext.Authors.Add(author);
        }

        public bool EntityExist(int id)
        {
            return _bookContext.Authors.Any(a => a.Id == id);
        }

        public IEnumerable<Author> GetEntities()
        {
            return _bookContext.Authors.Include(a => a.Books).ToList();
        }

        public Author GetEntity(int id, bool includeBook)
        {

            if (includeBook)
            {
                return _bookContext.Authors.Include(a => a.Books)
                    .Where(a => a.Id == id)
                    .FirstOrDefault();
            }

            return _bookContext.Authors.Where(a => a.Id == id).FirstOrDefault();
        }

        public void  Save()
        {
            _bookContext.SaveChanges();
        }
    }
}
