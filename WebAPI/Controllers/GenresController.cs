using BusinessLayer.Models.Genre;
using BusinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    /// <summary>
    /// Get genres
    /// </summary>
    [HttpGet]
    public async Task<ICollection<GenreModel>> GetGenres(string? filterName = null)
    { 
        return await _genreService.GetGenresAsync(filterName);
    }
    
    /// <summary>
    /// Get genre by id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenreModel>> GetGenre([FromRoute] int id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);

        if (genre == null)
        {
            return NotFound($"Genre with id {id} not found.");
        }
        return Ok(genre);
    }

    /// <summary>
    /// Create genre
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> CreateGenre(CreateGenreModel model)
    {
        var genre = await _genreService.CreateGenreAsync(model);
        
        return Created($"/genres/{genre.Id}", genre);
    }

    /// <summary>
    /// Edit genre
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditGenre([FromRoute] int id, EditGenreModel model)
    {
        var genre = await _genreService.EditGenreAsync(id, model);
        if (genre == null)
        {
            return NotFound($"Genre with id {id} not found.");
        }
        
        return Ok(genre);
    }
    
    /// <summary>
    /// Delete genre
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteGenre([FromRoute] int id)
    {
        var wasDeleted = await _genreService.DeleteGenreAsync(id);
        if (!wasDeleted)
        {
            return NotFound($"Genre with id {id} not found.");
        }

        return Ok();
    }
}