using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public class BookHubBdContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        var dbPath = Path.Join(Environment.GetFolderPath(folder), "bookHub.db");

        optionsBuilder
            .UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity<AuthorBook>();

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books)
            .UsingEntity<BookGenre>();
        
        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}