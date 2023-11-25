using BusinessLayer.Mappers;
using BusinessLayer.Models.Genre;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class GenreService : IGenreService
{
    private readonly BookHubBdContext _dbContext;

    public GenreService(BookHubBdContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GenreModel> CreateGenreAsync(CreateGenreModel model)
    {
        var newGenre = model.MapCreateGenreModelToGenre();

        await _dbContext.Genres.AddAsync(newGenre);

        return newGenre.MapGenreToGenreModel();
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

        return genre.MapGenreToGenreModel();
    }

    public async Task<List<GenreModel>> GetGenresAsync()
    {
        var genreModels = await _dbContext.Genres
            .Select(g => g.MapGenreToGenreModel())
            .ToListAsync();

        return genreModels;
    }

    public async Task<GenreModel?> GetGenreByIdAsync(int genreId)
    {
        var genreModel = await _dbContext.Genres
            .Where(g => g.Id == genreId)
            .Select(g => g.MapGenreToGenreModel())
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