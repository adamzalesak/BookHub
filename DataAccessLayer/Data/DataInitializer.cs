using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var authors = PrepareAuthorModels();
        var publishers = PreparePublisherModels();
        var books = PrepareBookModels();
        var authorBooks = PrepareAuthorBookModels();

        modelBuilder.Entity<Author>()
            .HasData(authors);
        modelBuilder.Entity<Publisher>()
            .HasData(publishers);
        modelBuilder.Entity<Book>()
            .HasData(books);
        modelBuilder.Entity<AuthorBook>()
            .HasData(authorBooks);
    }

    private static List<Author> PrepareAuthorModels()
    {
        return new List<Author>
        {
            new()
            {
                Id = 1,
                Name = "Petr Ludwig",
            },
            new()
            {
                Id = 2,
                Name = "J. K. Rowling",
            },
            new()
            {
                Id = 3,
                Name = "Hans Rosling",
            },
            new()
            {
                Id = 4,
                Name = "Ola Rosling",
            },
            new()
            {
                Id = 5,
                Name = "Anna Rosling Rönnlund",
            },
        };
    }

    private static List<Publisher> PreparePublisherModels()
    {
        return new List<Publisher>
        {
            new()
            {
                Id = 1,
                Name = "Albatros Media",
                Description = "Albatros Media a.s. is the largest publishing house in the Czech Republic and Slovakia.",
            },
            new()
            {
                Id = 2,
                Name = "Jan Melvil Publishing",
                Description = "Jan Melvil Publishing is a Czech publishing house, which was founded in 1991.",
            },
        };
    }

    private static List<Book> PrepareBookModels()
    {
        return new List<Book>
        {
            new()
            {
                Id = 1,
                Name = "Konec prokrastinace",
                Description = "Konec prokrastinace je kniha, která vám pomůže překonat sklony k odkládání věcí na později.",
                Isbn = "978-80-87270-51-6",
                Price = 24.90m,
                PublisherId = 2,
            },
            new()
            {
                Id = 2,
                Name = "Faktomluva",
                Description = "Faktomluva je kniha, která vás naučí, jak pracovat s fakty.",
                Isbn = "978-80-7555-056-9",
                Price = 29.90m,
                PublisherId = 2,
            },
            new()
            {
                Id = 3,
                Name = "Harry Potter a Kámen mudrců",
                Description = "Harry Potter a Kámen mudrců je první díl ze série knih o Harrym Potterovi.",
                Isbn = "978-80-0006-758-2",
                Price = 49,
                PublisherId = 1,
            },
        };
    }

    private static List<AuthorBook> PrepareAuthorBookModels()
    {
        return new List<AuthorBook>
        {
            new()
            {
                AuthorId = 1,
                BookId = 1,
            },
            new()
            {
                AuthorId = 2,
                BookId = 3,
            },
            new()
            {
                AuthorId = 3,
                BookId = 2,
            },
            new()
            {
                AuthorId = 4,
                BookId = 2,
            },
            new()
            {
                AuthorId = 5,
                BookId = 2,
            },
        };
    }
}