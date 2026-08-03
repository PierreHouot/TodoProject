namespace TodoAPI.Requests
{
    public class CreateActivityRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
