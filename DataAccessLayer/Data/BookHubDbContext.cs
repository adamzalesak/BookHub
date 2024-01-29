using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public class BookHubDbContext : IdentityDbContext
{
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<AuthorBook> AuthorBooks { get; set; }
    public virtual DbSet<Publisher> Publishers { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Price> Prices { get; set; }
    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<User> AppUsers { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<BookGenre> BookGenres { get; set; }

    public BookHubDbContext(DbContextOptions<BookHubDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        modelBuilder.Entity<Book>()
            .HasQueryFilter(b => !b.IsDeleted);
        modelBuilder.Entity<Price>()
            .HasQueryFilter(p => !p.Book.IsDeleted);
        modelBuilder.Entity<Review>()
            .HasQueryFilter(r => !r.Book.IsDeleted);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity<AuthorBook>();

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books)
            .UsingEntity<BookGenre>();

        modelBuilder.Entity<Book>()
            .HasOne(b => b.PrimaryGenre)
            .WithMany()
            .HasForeignKey(b => b.PrimaryGenreId);
        
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.Books)
            .WithMany()
            .UsingEntity<BookCart>();
        

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}