namespace Module5.Data.Entities;

public class Author
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }

    public virtual AuthorProfile? Profile { get; set; }
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
