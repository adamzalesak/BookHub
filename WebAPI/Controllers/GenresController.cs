using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Genre;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GenresController : ControllerBase
{
    private readonly BookHubBdContext _dbContext;

    public GenresController(BookHubBdContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ICollection<GenreModel>> GetGenres()
    {
        var genres = await _dbContext.Genres
            .Select(genre => new GenreModel
            {
                Id = genre.Id,
                Name = genre.Name,
            })
            .ToListAsync();

        return genres;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GenreModel>> GetGenre(int id)
    {
        var genre = await _dbContext.Genres
            .Where(g => g.Id == id)
            .Select(genre => new GenreModel
            {
                Id = genre.Id,
                Name = genre.Name,
            }).FirstOrDefaultAsync();

        if (genre == null)
        {
            return NotFound("Genre not found.");
        }
        return Ok(genre);
    }

    [HttpPost]
    public async Task<ActionResult> CreateGenre(CreateGenreModel createData)
    {
        var newGenre = new Genre
        {
            Name = createData.Name,
        };

        await _dbContext.Genres.AddAsync(newGenre);
        await _dbContext.SaveChangesAsync();

        var dataToSend = new GenreModel
        {
            Id = newGenre.Id,
            Name = newGenre.Name,
        };
        
        return Created($"/genres/{dataToSend.Id}", dataToSend);
    }

    [HttpPut]
    public async Task<ActionResult> EditGenre(GenreModel editData)
    {
        var genre = await _dbContext.Genres.FindAsync(editData.Id);
        if (genre == null)
        {
            return NotFound("Genre not found.");
        }

        genre.Name = editData.Name;
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGenre(int id)
    {
        var genre = await _dbContext.Genres.FindAsync(id);
        if (genre == null)
        {
            return NotFound("Genre not found.");
        }
        
        _dbContext.Genres.Remove(genre);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}