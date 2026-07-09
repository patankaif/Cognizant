namespace Module5.Data.Entities;

public class AuthorProfile
{
    public int AuthorId { get; set; }
    public string Biography { get; set; } = string.Empty;
    public string? Website { get; set; }

    public virtual Author Author { get; set; } = null!;
}
