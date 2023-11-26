using BusinessLayer.Mappers;
using BusinessLayer.Models.Genre;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class GenreService : IGenreService
{
    private readonly BookHubDbContext _dbContext;

    public GenreService(BookHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GenreModel> CreateGenreAsync(CreateGenreModel model)
    {
        var newGenre = model.MapToGenre();

        await _dbContext.Genres.AddAsync(newGenre);
        await SaveAsync();

        return newGenre.MapToGenreModel();
    }

    public async Task<GenreModel?> EditGenreAsync(int genreId, EditGenreModel model)
    {
        var genre = await _dbContext.Genres.FindAsync(genreId);
        if (genre == null)
        {
            return null;
        }

        genre.Name = model.Name;
        await SaveAsync();

        return genre.MapToGenreModel();
    }

    public async Task<List<GenreModel>> GetGenresAsync()
    {
        var genreModels = await _dbContext.Genres
            .Select(g => g.MapToGenreModel())
            .ToListAsync();

        return genreModels;
    }

    public async Task<GenreModel?> GetGenreByIdAsync(int genreId)
    {
        var genreModel = await _dbContext.Genres
            .Where(g => g.Id == genreId)
            .Select(g => g.MapToGenreModel())
            .FirstOrDefaultAsync();

        return genreModel;
    }

    public async Task<bool> DeleteGenreAsync(int genreId)
    {
        var genre = await _dbContext.Genres.FindAsync(genreId);
        if (genre == null)
        {
            return false;
        }
        
        _dbContext.Genres.Remove(genre);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }
    
    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}