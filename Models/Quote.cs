namespace AntigravityQuotes.Models;

public class Quote
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public required string Author { get; set; }
    public string? Source { get; set; }
    public string? Tags { get; set; } // Comma-separated tags
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
