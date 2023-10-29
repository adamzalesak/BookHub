using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.User;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly BookHubBdContext _dbContext;

    public UsersController(BookHubBdContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    [HttpGet]
    public async Task<ICollection<UserModel>> GetUsers()
    {
        var users = await _dbContext.Users
            .Include(u => u.Orders)
            .Select(user => new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                OrderIds = user.Orders.Select(o => o.Id).ToList(),
            })
            .ToListAsync();

        return users;
    }
    
    /// <summary>
    /// Get user by id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserModel>> GetUser([FromRoute] int id)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == id)
            .Include(u => u.Orders)
            .Select(user => new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                OrderIds = user.Orders.Select(o => o.Id).ToList(),
            }).FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound("User not found.");
        }
        return Ok(user);
    }

    /// <summary>
    /// Create user
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> CreateUser(CreateUserModel createData)
    {
        var newUser = new User
        {
            Name = createData.Name,
            Username = createData.Username,
            Email = createData.Email,
            IsAdministrator = createData.IsAdministrator,
            Orders = new List<Order>(),
        };

        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();

        var dataToSend = new UserModel
        {
            Id = newUser.Id,
            Name = newUser.Name,
            Username = newUser.Username,
            Email = newUser.Email,
            OrderIds = new List<int>(),
        };
        
        return Created($"/users/{dataToSend.Id}", dataToSend);
    }

    /// <summary>
    /// Edit user
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditUser([FromRoute] int id, EditUserModel editData)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        user.Name = editData.Name ?? user.Name;
        user.Username = editData.Username ?? user.Username;
        user.Email = editData.Email ?? user.Email;
        user.IsAdministrator = editData.IsAdministrator ?? user.IsAdministrator;

        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    
    /// <summary>
    /// Delete user
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        
        return Ok();
    }
}