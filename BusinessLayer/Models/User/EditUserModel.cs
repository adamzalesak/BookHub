namespace BusinessLayer.Models.User;

public class EditUserModel
{
    public string? Name { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public bool? IsAdministrator { get; set; }
}