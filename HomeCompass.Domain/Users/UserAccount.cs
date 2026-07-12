namespace HomeCompass.Domain.Users;

public class UserAccount
{
    public Guid Id { get; private set; }

    public string Email { get; private set; }

    public DateTime CreatedAt { get; private set; }


    private UserAccount()
    {
        // Required by ORM tools later
        Email = string.Empty;
    }


    public UserAccount(string email)
    {
        Id = Guid.NewGuid();
        Email = email;
        CreatedAt = DateTime.UtcNow;
    }
}
