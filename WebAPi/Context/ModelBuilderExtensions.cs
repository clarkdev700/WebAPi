using Microsoft.EntityFrameworkCore;
using WebAPi.Entities;

namespace WebAPi.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Author>().HasData(
                new Author
                {
                     Id = 1,
                     Name = "William",
                    LastName = "Shakespeare"
                },
                
                new Author
                {
                     Id = 2,
                     Name = "Eric",
                    LastName = "Dupuis"
                },
                new Author
                {
                     Id = 3,
                     Name = "Philips",
                    LastName = "lham"
                },
                
                new Author
                {
                     Id = 4,
                     Name = "Clark",
                    LastName = "Emmerson"
                },
                new Author
                {
                     Id = 5,
                     Name = "Samuel",
                    LastName = "Konate"
                },
                
                new Author
                {
                     Id = 6,
                     Name = "Alain",
                    LastName = "Lebelier"
                }
            );
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, PageNumber = 40 , AuthorId = 1, Title = "Hamlet" },
                new Book { Id = 2, PageNumber = 33 , AuthorId = 1, Title = "King Lear" },
                new Book { Id = 3, PageNumber = 20, AuthorId = 1, Title = "Othello" },
                new Book { Id = 4, PageNumber = 80, AuthorId = 3, Title = "Le book noir" },
                new Book { Id = 5, PageNumber = 24 , AuthorId = 3, Title = "L'Afrique" },
                new Book { Id = 6, PageNumber = 55 , AuthorId = 4, Title = "First day at Paris" },
                new Book { Id = 7, PageNumber = 24, AuthorId = 4, Title = "Le marchand" },
                new Book { Id = 8, PageNumber = 60, AuthorId = 4, Title = "My Dream" },
                new Book { Id = 9, PageNumber = 50, AuthorId = 5, Title = "Le reanard et le corbeau" },
                new Book { Id = 10, PageNumber = 125, AuthorId = 5, Title = "Sa majeste" }
            );
        }
    }
}
