namespace Module5.Data.Entities;

public class Genre
{
    public int GenreId { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
}
