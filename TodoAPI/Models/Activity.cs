namespace TodoAPI.Models
{
    public record Activity
    {
        public required string Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
