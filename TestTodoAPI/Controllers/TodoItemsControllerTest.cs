using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using TodoAPI.Models;
using TodoAPI.Requests;
namespace TestTodoAPI.Controllers
{
    public class TodoItemsControllerTest : IClassFixture<WebAppFactory<Program>>
    {
        private readonly WebAppFactory<Program> _factory;
        private readonly string apiUrl = "/api/todoitems";
        private HttpClient _client;

        public TodoItemsControllerTest(WebAppFactory<Program> factory)
        {
            _factory = factory;
            _factory.ResetDatabase();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Get_Endpoint_ReturnOk()
        {
            // Act
            var response = await _client.GetAsync(apiUrl, TestContext.Current.CancellationToken);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Post_Items_ReturnCreated()
        {

            // Arrange
            var item = new CreateTodoItemRequest() { Name = "task", IsComplete = false };
            var content = JsonContent.Create(item);

            // Act
            var response = await _client.PostAsync(apiUrl, content, TestContext.Current.CancellationToken);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Get_PreviouslyPostedItem_ReturnContent()
        {

            // Arrange
            var item = new CreateTodoItemRequest() { Name = "task", IsComplete = false };
            var content = JsonContent.Create(item);
            await _client.PostAsync(apiUrl, content, TestContext.Current.CancellationToken);

            // Act
            var response = await _client.GetAsync(apiUrl, TestContext.Current.CancellationToken);
            var data = (await response.Content
                .ReadFromJsonAsync<List<TodoItem>>(cancellationToken: TestContext.Current.CancellationToken))?
                .First();

            // Assert
            data.Should().NotBeNull();
            item.Should().BeEquivalentTo(data, option => option.Excluding(x => x.Id));
        }

        [Fact]
        public async Task Delete_EmptyEndpoint_ReturnsNotFound()
        {
            // Arrange
            var address = string.Concat(apiUrl, "/1");

            // Act
            var response = await _client.DeleteAsync(address, TestContext.Current.CancellationToken);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ExistingItem_ReturnsNoContent()
        {
            // Arrange
            var item = new CreateTodoItemRequest() { Name = "task", IsComplete = false };
            var content = JsonContent.Create(item);
            var itemId = await (await _client.PostAsync(apiUrl, content, TestContext.Current.CancellationToken)).Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

            // Act
            var response = await _client.DeleteAsync(Path.Combine(apiUrl, itemId), TestContext.Current.CancellationToken);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
