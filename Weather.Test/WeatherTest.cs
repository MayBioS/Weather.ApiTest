using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.WebApi;
using Xunit;
namespace Weather.Tests
{
    public class WeatherTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public WeatherTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>());
            _client = _server.CreateClient();
        }
        [Fact]
        public async Task Quando_Buscado_Deve_Retornar_O_Tempo()
        {
            // Act
            var response = await
            _client.GetAsync("/weatherforecast");
            response.EnsureSuccessStatusCode(); // 200 - 299
            var forecast =
            JsonConvert.DeserializeObject<WeatherForecast[]>(
            await response.Content.ReadAsStringAsync()
            );
            // Assert
            Assert.Equal(5, forecast.Length);
        }
    }
}