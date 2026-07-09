namespace Module5.Data.Dtos;

public class BookSummaryDto
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AuthorFullName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int PublishedYear { get; set; }
}
