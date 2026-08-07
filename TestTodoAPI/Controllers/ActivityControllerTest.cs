using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Json;
using TodoAPI.Models;
using TodoAPI.Requests;
namespace TestTodoAPI.Controllers
{
    public class ActivityControllerTest : IClassFixture<WebAppFactory<Program>>
    {
        private readonly WebAppFactory<Program> _factory;
        private readonly string apiUrl = "/api/activity";
        private readonly HttpClient _client;

        public ActivityControllerTest(WebAppFactory<Program> factory)
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
            var item = new CreateActivityRequest() { Name = "task", Description = "very describe"};
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
            var item = new CreateActivityRequest() { Name = "task", Description = "very describe", Date = DateOnly.FromDateTime(DateTime.Today)};
            var content = JsonContent.Create(item);
            await _client.PostAsync(apiUrl, content, TestContext.Current.CancellationToken);

            // Act
            var response = await _client.GetAsync(apiUrl, TestContext.Current.CancellationToken);
            var data = (await response.Content
                .ReadFromJsonAsync<List<Activity>>(cancellationToken: TestContext.Current.CancellationToken))?
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
            var item = new CreateActivityRequest() { Name = "task", Description = "very describe" };
            var content = JsonContent.Create(item);
            var itemId = await (await _client.PostAsync(apiUrl, content, TestContext.Current.CancellationToken)).Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

            // Act
            var response = await _client.DeleteAsync(Path.Combine(apiUrl, itemId), TestContext.Current.CancellationToken);
           

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_ExistingItem_ReturnsNoContent()
        {
            // Arrange
            var item = new CreateActivityRequest() { Name = "task", Description = "very describe" };
            var content = JsonContent.Create(item);
            var itemId = await (await _client.PostAsync(apiUrl, content, TestContext.Current.CancellationToken)).Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

            // Act
            var updatedContent = JsonContent.Create( new Activity { Id= itemId, Name = item.Name, Description = "new describe"});
            var response = await _client.PutAsync(Path.Combine(apiUrl, itemId), updatedContent, TestContext.Current.CancellationToken);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_ExistingItem_IsUpdated()
        {
            // Arrange
            var item = new CreateActivityRequest() { Name = "task", Description = "very describe" };
            var content = JsonContent.Create(item);
            var itemId = await (await _client.PostAsync(apiUrl, content, TestContext.Current.CancellationToken)).Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

            // Act
            var updatedContent = JsonContent.Create(new Activity { Id = itemId, Name = item.Name, Description = "new describe" });
            await _client.PutAsync(Path.Combine(apiUrl, itemId), updatedContent, TestContext.Current.CancellationToken);

            var updatedItem = await _client.GetAsync(Path.Combine(apiUrl, itemId), TestContext.Current.CancellationToken);
            var data = await updatedItem.Content.ReadFromJsonAsync<Activity>(cancellationToken: TestContext.Current.CancellationToken);

            // Assert
            data?.Description.Should().BeEquivalentTo("new describe");
        }

        [Fact]
        public async Task Update_WrongId_ReturnsBadRequest()
        {
            // Arrange
            var item = new CreateActivityRequest() { Name = "task", Description = "very describe" };
            var content = JsonContent.Create(item);
            var itemId = await (await _client.PostAsync(apiUrl, content, TestContext.Current.CancellationToken)).Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

            // Act
            var updatedContent = JsonContent.Create(new Activity { Id = Guid.NewGuid().ToString(), Name = item.Name, Description = "new describe" });
            var response = await _client.PutAsync(Path.Combine(apiUrl, itemId), updatedContent, TestContext.Current.CancellationToken);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
