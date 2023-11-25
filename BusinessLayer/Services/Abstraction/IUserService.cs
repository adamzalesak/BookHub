using DataAccessLayer.Models;
using BusinessLayer.Models.User;

namespace BusinessLayer.Services.Abstraction;

public interface IUserService : IBaseService
{
    public Task<UserModel> CreateUserAsync(CreateUserModel model);
    public Task<UserModel?> EditUserAsync(int userId, EditUserModel model); 
    public Task<UserModel?> GetUserByIdAsync(int userId);
    public Task<List<UserModel>> GetUsersAsync();
    public Task<bool> DeleteUserAsync(int userId);
}