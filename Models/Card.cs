namespace AnkiCopyBase.Models
{
    public record struct Card(string Front, string Back, string? Hint = null);
}