namespace BusinessLayer.Services.Abstraction;

public interface IBooksService : IBaseService
{
    public Task<bool> DeleteBookAsync(int bookId);
}