namespace TodoAPI.Requests
{
    public class CreateActivityRequest
    {
        public required string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
