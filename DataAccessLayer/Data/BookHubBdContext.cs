using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public class BookHubBdContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Publisher> Publishers { get; set; }

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

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);

    }
}