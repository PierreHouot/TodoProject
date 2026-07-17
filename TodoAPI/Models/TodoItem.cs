namespace TodoAPI.Models
{
    public class TodoItem
    {
        public required string Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
