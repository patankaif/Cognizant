namespace Module5.Data.Entities;

public class BookGenre
{
    public int BookId { get; set; }
    public virtual Book Book { get; set; } = null!;

    public int GenreId { get; set; }
    public virtual Genre Genre { get; set; } = null!;
}
