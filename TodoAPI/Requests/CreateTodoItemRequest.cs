namespace TodoAPI.Requests
{
    public class CreateTodoItemRequest
    {
        public required string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
