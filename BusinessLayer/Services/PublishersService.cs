using BusinessLayer.Mappers;
using BusinessLayer.Models.Publisher;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Services;

public class PublishersService : IPublishersService
{
    private readonly BookHubDbContext _dbContext;
    private readonly IMemoryCache _memoryCache;
    
    public PublishersService(BookHubDbContext dbContext, IMemoryCache memoryCache)
    {
        _dbContext = dbContext;
        _memoryCache = memoryCache;
    }

    public async Task<ICollection<PublisherModel>> GetPublishersAsync()
    {
        if (_memoryCache.TryGetValue(Constants.GetPublishersCacheKey, out List<PublisherModel>? publishers) && publishers != null)
        {
            return publishers;
        }

        var publishersFromDb = await _dbContext.Publishers
            .Select(p => p.MapToPublisherModel())
            .ToListAsync();

        _memoryCache.Set(Constants.GetPublishersCacheKey, publishersFromDb,
            new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(3)));

        return publishersFromDb;
    }
    
    public async Task<PublisherModel?> GetPublisherByIdAsync(int id)
    {
        var publisher = await _dbContext.Publishers
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return publisher?.MapToPublisherModel();
    }

    public Task SaveAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}