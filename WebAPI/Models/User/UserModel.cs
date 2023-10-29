namespace WebAPI.Models.User;

public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public List<int> OrderIds { get; set; }
}