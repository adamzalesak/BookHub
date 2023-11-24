namespace WebAPI.Models.User;

public class CreateUserModel
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool IsAdministrator { get; set; }
}