﻿// <auto-generated />
using System;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(BookHubBdContext))]
    [Migration("20231116100853_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Petr Ludwig"
                        },
                        new
                        {
                            Id = 2,
                            Name = "J. K. Rowling"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Hans Rosling"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Ola Rosling"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Anna Rosling Rönnlund"
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.AuthorBook", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.HasKey("AuthorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("AuthorBook");

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            BookId = 1
                        },
                        new
                        {
                            AuthorId = 2,
                            BookId = 3
                        },
                        new
                        {
                            AuthorId = 3,
                            BookId = 2
                        },
                        new
                        {
                            AuthorId = 4,
                            BookId = 2
                        },
                        new
                        {
                            AuthorId = 5,
                            BookId = 2
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PublisherId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Count = 0,
                            Description = "Konec prokrastinace je kniha, která vám pomůže překonat sklony k odkládání věcí na později.",
                            IsDeleted = false,
                            Isbn = "978-80-87270-51-6",
                            Name = "Konec prokrastinace",
                            PublisherId = 2
                        },
                        new
                        {
                            Id = 2,
                            Count = 0,
                            Description = "Faktomluva je kniha, která vás naučí, jak pracovat s fakty.",
                            IsDeleted = false,
                            Isbn = "978-80-7555-056-9",
                            Name = "Faktomluva",
                            PublisherId = 2
                        },
                        new
                        {
                            Id = 3,
                            Count = 0,
                            Description = "Harry Potter a Kámen mudrců je první díl ze série knih o Harrym Potterovi.",
                            IsDeleted = false,
                            Isbn = "978-80-0006-758-2",
                            Name = "Harry Potter a Kámen mudrců",
                            PublisherId = 1
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.BookCart", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<int>("CartId")
                        .HasColumnType("integer");

                    b.HasKey("BookId", "CartId");

                    b.HasIndex("CartId");

                    b.ToTable("BookCart");

                    b.HasData(
                        new
                        {
                            BookId = 2,
                            CartId = 1
                        },
                        new
                        {
                            BookId = 3,
                            CartId = 2
                        },
                        new
                        {
                            BookId = 1,
                            CartId = 2
                        },
                        new
                        {
                            BookId = 1,
                            CartId = 3
                        },
                        new
                        {
                            BookId = 3,
                            CartId = 3
                        },
                        new
                        {
                            BookId = 1,
                            CartId = 4
                        },
                        new
                        {
                            BookId = 3,
                            CartId = 5
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.BookGenre", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.HasKey("BookId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("BookGenre");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            GenreId = 3
                        },
                        new
                        {
                            BookId = 2,
                            GenreId = 3
                        },
                        new
                        {
                            BookId = 3,
                            GenreId = 1
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Carts");

                    b.HasData(
                        new
                        {
                            Id = 1
                        },
                        new
                        {
                            Id = 2
                        },
                        new
                        {
                            Id = 3
                        },
                        new
                        {
                            Id = 4
                        },
                        new
                        {
                            Id = 5
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sci-fi"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Osobní rozvoj"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Detektivka"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Odborná literatura"
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CartId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Phone")
                        .HasColumnType("bigint");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CartId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Hlavná 132, 84545, Bratislava",
                            CartId = 1,
                            Email = "poppar12@gmail.com",
                            Phone = 421958655988L,
                            State = 0,
                            Timestamp = new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalPrice = 9.99m
                        },
                        new
                        {
                            Id = 2,
                            Address = "Bukov 72, 02201, Cadca",
                            CartId = 2,
                            Email = "emmisek@zoznam.sk",
                            Phone = 421942333659L,
                            State = 2,
                            Timestamp = new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalPrice = 34.97m
                        },
                        new
                        {
                            Id = 3,
                            Address = "Botanická 68a, 60200, Brno",
                            CartId = 3,
                            Email = "prokop.dlouhy@gmail.com",
                            Phone = 420856999824L,
                            State = 3,
                            Timestamp = new DateTime(2023, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalPrice = 17.98m,
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            Address = "Školská 56, 73401, Karviná",
                            CartId = 4,
                            Email = "jerabkova.tereza@outlook.com",
                            Phone = 420744856932L,
                            State = 0,
                            Timestamp = new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalPrice = 9.99m,
                            UserId = 4
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<decimal>("BookPrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Prices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            BookPrice = 9.99m,
                            ValidFrom = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            BookId = 2,
                            BookPrice = 14.99m,
                            ValidFrom = new DateTime(2021, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            BookId = 2,
                            BookPrice = 9.99m,
                            ValidFrom = new DateTime(2021, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            BookId = 3,
                            BookPrice = 12.49m,
                            ValidFrom = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            BookId = 3,
                            BookPrice = 7.99m,
                            ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Publishers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Albatros Media a.s. is the largest publishing house in the Czech Republic and Slovakia.",
                            Name = "Albatros Media"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Jan Melvil Publishing is a Czech publishing house, which was founded in 1991.",
                            Name = "Jan Melvil Publishing"
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            Rating = 4,
                            Text = "Kniha je opravdu praktická, každopádně se nejedná o lék na prokrastinaci. To už je spíše práce samotného čtenáře.",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            BookId = 2,
                            Rating = 4,
                            Text = "Kniha se zabývá faktografickými údaji, které jsou ale podány velmi hezkou a čtivou formou. Neznalost těchto faktů nám však zásadním způsobem deformuje pohled na svět, na to, jak svět vnímáme a vyhodnocujeme.",
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            BookId = 2,
                            Rating = 5,
                            Text = "Nejvíce optimistická kniha, ač realistická kniha, kterou jsem v poslední době četl. Úchvatné! :)",
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            BookId = 3,
                            Rating = 5,
                            Text = "První díl působí jako dětská knížka. Myslím si, že je krásně a výstižně popsané chování hlavních postav úměrně jejich věku. V porovnání s filmem je kniha trefnější. Myslím si, že je to výborná kniha na začátek celé série. Líbí se mi drobnosti, které jsou důležité v pozdějších dílech.",
                            UserId = 2
                        },
                        new
                        {
                            Id = 5,
                            BookId = 3,
                            Rating = 4,
                            Text = "Na Harrym Potterovi jsem doslova vyrůstala, takže jsem jako dítě tuto knihu naprosto zbožňovala. I dnes si ji ráda přečtu, ale doporučím ji hlavně mladším.",
                            UserId = 4
                        },
                        new
                        {
                            Id = 6,
                            BookId = 3,
                            Rating = 1,
                            Text = "Pohádka o ničem, výmysl moderní doby.",
                            UserId = 3
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAdministrator")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "pavel.novak@seznam.cz",
                            IsAdministrator = false,
                            Name = "Pavel Novák",
                            Username = "bookworm"
                        },
                        new
                        {
                            Id = 2,
                            Email = "karolina.svobodova@email.cz",
                            IsAdministrator = false,
                            Name = "Karolína Svobodová",
                            Username = "kaja2000"
                        },
                        new
                        {
                            Id = 3,
                            Email = "prokop.dlouhy@gmail.com",
                            IsAdministrator = false,
                            Name = "Prokop Dlouhý",
                            Username = "pageturner"
                        },
                        new
                        {
                            Id = 4,
                            Email = "jerabkova.tereza@outlook.com",
                            IsAdministrator = false,
                            Name = "Tereza Jeřábková",
                            Username = "booknerd"
                        },
                        new
                        {
                            Id = 5,
                            Email = "honza.jelinek@seznam.cz",
                            IsAdministrator = true,
                            Name = "Jan Jelínek",
                            Username = "honza"
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Models.AuthorBook", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.Models.Book", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("DataAccessLayer.Models.BookCart", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Models.Cart", null)
                        .WithMany()
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.Models.BookGenre", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.Models.Order", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Cart", "Cart")
                        .WithOne("Order")
                        .HasForeignKey("DataAccessLayer.Models.Order", "CartId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Cart");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Price", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Book", "Book")
                        .WithMany("Prices")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Review", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Book", b =>
                {
                    b.Navigation("Prices");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Cart", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Publisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("DataAccessLayer.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}