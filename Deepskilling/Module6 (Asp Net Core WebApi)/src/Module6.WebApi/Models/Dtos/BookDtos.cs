using System.ComponentModel.DataAnnotations;

namespace Module6.WebApi.Models.Dtos;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public decimal Price { get; set; }
}

public class CreateBookDto
{
    [Required, MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string Author { get; set; } = string.Empty;

    [Range(1450, 2100)]
    public int PublishedYear { get; set; }

    [Range(0, 100000)]
    public decimal Price { get; set; }
}

public class UpdateBookDto
{
    [Required, MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string Author { get; set; } = string.Empty;

    [Range(1450, 2100)]
    public int PublishedYear { get; set; }

    [Range(0, 100000)]
    public decimal Price { get; set; }
}
