namespace TodoAPI.Requests
{
    public class CreateActivityRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateOnly? Date { get; set; }
    }
}
