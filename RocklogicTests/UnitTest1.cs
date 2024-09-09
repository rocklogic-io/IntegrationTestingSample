// namespace RocklogicTests
// {
//     public class UnitTest1
//     {
//         [Fact]
//         public void TestRocklogicApi()
//         {

//         }
//     }
// }


using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Xunit;

namespace RocklogicTests
{
    
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

      [Fact]
public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
{
    // Arrange
    var request = new HttpRequestMessage(HttpMethod.Get, "/api/transactions");
    var factory = new WebApplicationFactory<Program>(); // Program is the main class of your API
    var _client = factory.CreateClient();
    // Act
    var response = await _client.SendAsync(request);

    // Log the response for debugging
    var contentType = response.Content.Headers.ContentType.ToString();
    var content = await response.Content.ReadAsStringAsync();

    // Output the actual content for debugging purposes
    Console.WriteLine($"Content Type: {contentType}");
    Console.WriteLine($"Content: {content}");

    // Assert
    response.EnsureSuccessStatusCode(); // Check for 200 OK
    Assert.StartsWith("application/json", contentType);
}

[Fact]
public async Task Post_CreateTransaction_ReturnsSuccess()
{
    // Arrange
   
    var request = new HttpRequestMessage(HttpMethod.Post, "/api/Transactions")
    {
        Content = new StringContent("{\"Amount\": 100.0, \"Description\": \"Test Transaction\"}", Encoding.UTF8, "application/json")
    };
    
    var _client = _factory.CreateClient();
    
    // Act
    var response = await _client.SendAsync(request);

    // Assert
    response.EnsureSuccessStatusCode(); // 200 OK or 201 Created
    var contentType = response.Content?.Headers?.ContentType?.ToString();
if (contentType == null) throw new Exception("ContentType is null");

    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
}

[Fact]
public async Task Get_TransactionById_ReturnsTransaction()
{
    // Arrange
    var request = new HttpRequestMessage(HttpMethod.Get, "/api/transactions");
    var _client = _factory.CreateClient();
    
    // Act
    var response = await _client.SendAsync(request);
    var content = await response.Content.ReadAsStringAsync();

    // Assert
    response.EnsureSuccessStatusCode(); // 200 OK
    Assert.Contains("\"Amount\":", content); // Check if the response contains transaction data
}

[Fact]
public async Task Put_UpdateTransaction_ReturnsSuccess()
{
    // Arrange
    var updatePayload = new
    {
        Amount = 200.0,
        Description = "Updated Transaction"
    };
    var request = new HttpRequestMessage(HttpMethod.Put, "/api/Transactions/0") // Replace 1 with a valid ID
    {
        Content = new StringContent(JsonConvert.SerializeObject(updatePayload), Encoding.UTF8, "application/json")
    };

    var _client = _factory.CreateClient();

    // Act
    var response = await _client.SendAsync(request);
    var responseContent = await response.Content.ReadAsStringAsync();

    // Log the response for debugging
    Console.WriteLine($"Response Status Code: {response.StatusCode}");
    Console.WriteLine($"Response Content: {responseContent}");

    // Assert
    response.EnsureSuccessStatusCode(); // Should be 200 OK or 204 No Content
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
}



[Fact]
public async Task Delete_Transaction_ReturnsSuccess()
{
    // Arrange
    var request = new HttpRequestMessage(HttpMethod.Delete, "/api/transactions/2");
    var _client = _factory.CreateClient();
    
    // Act
    var response = await _client.SendAsync(request);

    // Assert
    response.EnsureSuccessStatusCode(); // 204 No Content
}


        // Add more tests here for your specific endpoints
    }
}