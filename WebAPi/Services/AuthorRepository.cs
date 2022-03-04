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
        
        /// <summary>
        /// Add new entity to the context.
        /// </summary>
        /// <param name="author"></param>
        public void AddEntity(Author author)
        {
            _bookContext.Authors.Add(author);
        }

        /// <summary>
        /// Check if the entity exist.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EntityExist(int id)
        {
            return _bookContext.Authors.Any(a => a.Id == id);
        }

        /// <summary>
        /// Get authors with book associated.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Author> GetEntities()
        {
            return _bookContext.Authors.Include(a => a.Books).ToList();
        }

        /// <summary>
        /// Get author with Book if incudeBook is true.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeBook"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Save entity to database.
        /// </summary>
        public void  Save()
        {
            _bookContext.SaveChanges();
        }
    }
}
