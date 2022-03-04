using System;
using System.Collections.Generic;
using System.Linq;
using WebAPi.Context;
using WebAPi.Entities;

namespace WebAPi.Services
{
    public class BookRepository : IRepository<Book>
    {
        private readonly BookContext _bookContext;

        public BookRepository(BookContext bookContext)
        {
            _bookContext = bookContext;
        }
        public void AddEntity(Book entity)
        {
            throw new NotImplementedException();
        }

        public bool EntityExist(int id)
        {
            return _bookContext.Books.Any(a => a.Id == id);
        }

        public IEnumerable<Book> GetEntities()
        {
            return _bookContext.Books.ToList();
        }

        public Book GetEntity(int id, bool includeRelatedEntity = false)
        {
           return _bookContext.Books.Where(b => b.Id == id).FirstOrDefault();
        }

        public void Save()
        {
            _bookContext.SaveChanges();
        }
    }
}
