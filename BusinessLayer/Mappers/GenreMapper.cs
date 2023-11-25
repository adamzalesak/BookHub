using DataAccessLayer.Models;
using Riok.Mapperly.Abstractions;
using BusinessLayer.Models.Genre;

namespace BusinessLayer.Mappers;

[Mapper]
public static partial class GenreMapper
{
    public static partial Genre MapCreateGenreModelToGenre(this CreateGenreModel model);

    public static partial GenreModel MapGenreToGenreModel(this Genre genre);
}