namespace Module6.WebApi.Services;

public class AppUser
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public interface IUserStore
{
    AppUser? Validate(string username, string password);
}

public class InMemoryUserStore : IUserStore
{
    private readonly List<AppUser> _users = new()
    {
        new AppUser { Username = "admin", Password = "Admin@123", Role = "Admin" },
        new AppUser { Username = "reader", Password = "Reader@123", Role = "Reader" }
    };

    public AppUser? Validate(string username, string password)
    {
        return _users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
            u.Password == password);
    }
}
