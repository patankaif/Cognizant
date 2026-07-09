using System.ComponentModel.DataAnnotations;

namespace Module5.Data.Entities;

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public decimal Price { get; set; }

    public int AuthorId { get; set; }
    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();

    [Timestamp]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}
