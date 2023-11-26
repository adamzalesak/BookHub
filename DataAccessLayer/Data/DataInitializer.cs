﻿using DataAccessLayer.Models;
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
        var users = PrepareUserModels();
        var reviews = PrepareReviewModels();
        var genres = PrepareGenreModels();
        var bookGenres = PrepareBookGenreModels();
        var prices = PreparePriceModels();
        var carts = PrepareCartModel();
        var bookCarts = PrepareBookCartModel();
        var orders = PrepareOrderModel();

        modelBuilder.Entity<Author>()
            .HasData(authors);
        modelBuilder.Entity<Publisher>()
            .HasData(publishers);
        modelBuilder.Entity<Book>()
            .HasData(books);
        modelBuilder.Entity<AuthorBook>()
            .HasData(authorBooks);
        modelBuilder.Entity<User>()
            .HasData(users);
        modelBuilder.Entity<Review>()
            .HasData(reviews);
        modelBuilder.Entity<Genre>()
            .HasData(genres);
        modelBuilder.Entity<BookGenre>()
            .HasData(bookGenres);
        modelBuilder.Entity<Price>()
            .HasData(prices);
        modelBuilder.Entity<Cart>()
            .HasData(carts);
        modelBuilder.Entity<Order>()
            .HasData(orders);
        modelBuilder.Entity<BookCart>()
            .HasData(bookCarts);
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
                PublisherId = 2,
            },
            new()
            {
                Id = 2,
                Name = "Faktomluva",
                Description = "Faktomluva je kniha, která vás naučí, jak pracovat s fakty.",
                Isbn = "978-80-7555-056-9",
                PublisherId = 2,
            },
            new()
            {
                Id = 3,
                Name = "Harry Potter a Kámen mudrců",
                Description = "Harry Potter a Kámen mudrců je první díl ze série knih o Harrym Potterovi.",
                Isbn = "978-80-0006-758-2",
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

    private static List<User> PrepareUserModels()
    {
        return new List<User>
        {
            new User()
            {
                Id = 1,
                Name = "Pavel Novák",
                Username = "bookworm",
                Email = "pavel.novak@seznam.cz",
                IsAdministrator = false,
            },
            new User()
            {
                Id = 2,
                Name = "Karolína Svobodová",
                Username = "kaja2000",
                Email = "karolina.svobodova@email.cz",
                IsAdministrator = false,
            },
            new User()
            {
                Id = 3,
                Name = "Prokop Dlouhý",
                Username = "pageturner",
                Email = "prokop.dlouhy@gmail.com",
                IsAdministrator = false,
            },
            new User()
            {
                Id = 4,
                Name = "Tereza Jeřábková",
                Username = "booknerd",
                Email = "jerabkova.tereza@outlook.com",
                IsAdministrator = false,
            },
            new User()
            {
                Id = 5,
                Name = "Jan Jelínek",
                Username = "honza",
                Email = "honza.jelinek@seznam.cz",
                IsAdministrator = true,
            },
        };
    }

    private static List<Review> PrepareReviewModels()
    {
        return new List<Review>
        {
            new Review()
            {
                Id = 1,
                Rating = 4,
                Text = "Kniha je opravdu praktická, každopádně se nejedná o lék na prokrastinaci. " +
                       "To už je spíše práce samotného čtenáře.",
                BookId = 1,
                UserId = 1,
            },
            new Review()
            {
                Id = 2,
                Rating = 4,
                Text = "Kniha se zabývá faktografickými údaji, které jsou ale podány velmi hezkou a čtivou formou. " +
                       "Neznalost těchto faktů nám však zásadním způsobem deformuje pohled na svět, na to, " +
                       "jak svět vnímáme a vyhodnocujeme.",
                BookId = 2,
                UserId = 2,
            },
            new Review()
            {
                Id = 3,
                Rating = 5,
                Text = "Nejvíce optimistická kniha, ač realistická kniha, kterou jsem v poslední době četl. " +
                       "Úchvatné! :)",
                BookId = 2,
                UserId = 3,
            },
            new Review()
            {
                Id = 4,
                Rating = 5,
                Text = "První díl působí jako dětská knížka. Myslím si, že je krásně a výstižně popsané chování " +
                       "hlavních postav úměrně jejich věku. V porovnání s filmem je kniha trefnější. Myslím si, " +
                       "že je to výborná kniha na začátek celé série. Líbí se mi drobnosti, " +
                       "které jsou důležité v pozdějších dílech.",
                BookId = 3,
                UserId = 2,
            },
            new Review()
            {
                Id = 5,
                Rating = 4,
                Text = "Na Harrym Potterovi jsem doslova vyrůstala, takže jsem jako dítě tuto knihu naprosto " +
                       "zbožňovala. I dnes si ji ráda přečtu, ale doporučím ji hlavně mladším.",
                BookId = 3,
                UserId = 4,
            },
            new Review()
            {
                Id = 6,
                Rating = 1,
                Text = "Pohádka o ničem, výmysl moderní doby.",
                BookId = 3,
                UserId = 3,
            },
        };
    }

    private static List<Genre> PrepareGenreModels()
    {
        return new List<Genre>
        {
            new Genre()
            {
                Id = 1,
                Name = "Fantasy",
            },
            new Genre()
            {
                Id = 2,
                Name = "Sci-fi",
            },
            new Genre()
            {
                Id = 3,
                Name = "Osobní rozvoj",
            },
            new Genre()
            {
                Id = 4,
                Name = "Detektivka",
            },
            new Genre()
            {
                Id = 5,
                Name = "Odborná literatura",
            }
        };
    }

    private static List<BookGenre> PrepareBookGenreModels()
    {
        return new List<BookGenre>
        {
            new BookGenre()
            {
                BookId = 1,
                GenreId = 3,
            },
            new BookGenre()
            {
                BookId = 2,
                GenreId = 3,
            },
            new BookGenre()
            {
                BookId = 3,
                GenreId = 1,
            },
        };
    }

    private static List<Price> PreparePriceModels()
    {
        return new List<Price>
        {
            new Price
            {
                Id = 1,
                BookPrice = 9.99m,
                ValidFrom = new DateTime(2021, 1, 1),
                BookId = 1
            },
            new Price
            {
                Id = 2,
                BookPrice = 14.99m,
                ValidFrom = new DateTime(2021, 7, 4),
                BookId = 2
            },
            new Price
            {
                Id = 3,
                BookPrice = 9.99m,
                ValidFrom = new DateTime(2021, 11, 12),
                BookId = 2
            },
            new Price
            {
                Id = 4,
                BookPrice = 12.49m,
                ValidFrom = new DateTime(2021, 1, 1),
                BookId = 3
            },
            new Price
            {
                Id = 5,
                BookPrice = 7.99m,
                ValidFrom = new DateTime(2023, 1, 1),
                BookId = 3
            }
        };
    }

    private static List<Cart> PrepareCartModel()
    {
        return new List<Cart>
        {
            new Cart { Id = 1,},
            new Cart { Id = 2,},
            new Cart { Id = 3,},
            new Cart { Id = 4,},
            new Cart { Id = 5,},
        };
    }
    
    private static List<BookCart> PrepareBookCartModel()
    {
        return new List<BookCart>
        {
            new BookCart {
                CartId = 1,
                BookId = 2,
            },
            new BookCart
            {
                CartId = 2,
                BookId = 3,
            },
            new BookCart
            {
                CartId = 2,
                BookId = 1,
            },
            new BookCart
            {
                CartId  = 3,
                BookId = 1,
            },
            new BookCart
            {
                CartId = 3,
                BookId = 3,
            },
            new BookCart
            {
                CartId = 4,
                BookId = 1,
            },
            new BookCart
            {
                CartId = 5,
                BookId = 3,
            }
        };
    }

    private static List<Order> PrepareOrderModel()
    {
        return new List<Order>
        {
            new Order
            {
                Id = 1,
                Email = "poppar12@gmail.com",
                Address = "Hlavná 132, 84545, Bratislava",
                Phone = 421958655988,
                TotalPrice = 9.99m,
                State = OrderState.Created,
                Timestamp = new DateTime(2023, 1, 5),
                CartId = 1
            },
            new Order
            {
                Id = 2,
                Email = "emmisek@zoznam.sk",
                Address = "Bukov 72, 02201, Cadca",
                Phone = 421942333659,
                TotalPrice = 34.97m,
                State = OrderState.Payed,
                Timestamp = new DateTime(2022, 1, 10),
                CartId = 2
            },
            new Order
            {
                Id = 3,
                Email = "prokop.dlouhy@gmail.com",
                Address = "Botanická 68a, 60200, Brno",
                Phone = 420856999824,
                TotalPrice = 17.98m,
                State = OrderState.Delivered,
                Timestamp = new DateTime(2023, 7, 15),
                CartId = 3,
                UserId = 3
            },
            new Order
            {
                Id = 4,
                Email = "jerabkova.tereza@outlook.com",
                Address = "Školská 56, 73401, Karviná",
                Phone = 420744856932,
                TotalPrice = 9.99m,
                State = OrderState.Created,
                Timestamp = new DateTime(2023, 10, 10),
                CartId = 4,
                UserId = 4
            }
        };
    }
}