using BusinessLayer.Services.Abstraction;
using BusinessLayer.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    [HttpGet]
    public async Task<ICollection<UserModel>> GetUsers()
    {
        return await _userService.GetUsersAsync();
    }
    
    /// <summary>
    /// Get user by id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserModel>> GetUser([FromRoute] int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound($"User with id {id} not found.");
        }
        return Ok(user);
    }

    /// <summary>
    /// Create user
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<UserModel>> CreateUser(CreateUserModel model)
    {
        var user = await _userService.CreateUserAsync(model);
        return Created($"/users/{user.Id}", user);
    }

    /// <summary>
    /// Edit user
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserModel>> EditUser([FromRoute] int id, EditUserModel model)
    {
        var user = await _userService.EditUserAsync(id, model);
        if (user == null)
        {
            return NotFound($"User with id {id} not found.");
        }
        return Ok(user);
    }
    
    /// <summary>
    /// Delete user
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        var wasDeleted = await _userService.DeleteUserAsync(id);
        if (!wasDeleted)
        {
            return NotFound($"User with id {id} not found.");
        }
        return Ok();
    }
}