using BusinessLayer.Models.Publisher;

namespace BusinessLayer.Services.Abstraction;

public interface IPublishersService : IBaseService
{
    public Task<ICollection<PublisherModel>> GetPublishersAsync();
    public Task<PublisherModel?> GetPublisherByIdAsync(int id);
}