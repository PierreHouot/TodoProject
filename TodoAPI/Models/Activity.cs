namespace TodoAPI.Models
{
    public record Activity
    {
        public required string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateOnly? Date { get; set; }
    }
}
