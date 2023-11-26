using BusinessLayer.Mappers;
using BusinessLayer.Models.User;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class UserService : IUserService
{
    private readonly BookHubBdContext _dbContext;

    public UserService(BookHubBdContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserModel> CreateUserAsync(CreateUserModel model)
    {
        var newUser = model.MapToUser();

        await _dbContext.Users.AddAsync(newUser);
        await SaveAsync();

        return newUser.MapToUserModel();
    }

    public async Task<UserModel?> EditUserAsync(int userId, EditUserModel model)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        if (user == null)
        {
            return null;
        }

        user.Name = model.Name ?? user.Name;
        user.Username = model.Username ?? user.Username;
        user.Email = model.Email ?? user.Email;
        user.IsAdministrator = model.IsAdministrator ?? user.IsAdministrator;

        await SaveAsync();
        
        return user.MapToUserModel();
    }

    public async Task<UserModel?> GetUserByIdAsync(int userId)
    {
        var userModel = await _dbContext.Users
            .Where(u => u.Id == userId)
            .Include(u => u.Orders)
            .Select(u => u.MapToUserModel())
            .FirstOrDefaultAsync();

        return userModel;
    }

    public async Task<List<UserModel>> GetUsersAsync()
    {
        var userModels = await _dbContext.Users
            .Include(u => u.Orders)
            .Select(u => u.MapToUserModel())
            .ToListAsync();
        
        return userModels;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        if (user == null)
        {
            return false;
        }
        
        _dbContext.Users.Remove(user);
        await SaveAsync();

        return true;
    }
    
    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}