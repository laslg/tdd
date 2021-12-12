using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using tdd;
using TDD.Model;

namespace TDD.Tests
{
    public class PatientTests : IClassFixture<PatientTestsDbWAF<Startup>>
    {
        // HttpClient to call our api's
        private readonly HttpClient httpClient;
        public WebApplicationFactory<Startup> _factory;

        public PatientTests(PatientTestsDbWAF<Startup> factory)
        {
            _factory = factory;

            // Initiate the HttpClient
            httpClient = _factory.CreateClient();
        }

    [Theory]
    [InlineData("Test Name 2", "1234567891", 20, "Male", HttpStatusCode.Created)]
    [InlineData("T", "1234567891", 20, "Male", HttpStatusCode.BadRequest)]
    [InlineData("A very very very very very very loooooooooong name", "1234567891", 20, "Male", HttpStatusCode.BadRequest)]
    [InlineData(null, "1234567890", 20, "Invalid Gender", HttpStatusCode.BadRequest)]
    [InlineData("Test Name", "InvalidNumber", 20, "Male", HttpStatusCode.BadRequest)]
    [InlineData("Test Name", "1234567890", -10, "Male", HttpStatusCode.BadRequest)]
    [InlineData("Test Name", "1234567890", 20, "Invalid Gender", HttpStatusCode.BadRequest)]
    [InlineData("Test Name", "12345678901234444", 20, "Invalid Gender", HttpStatusCode.BadRequest)]
    public async Task PatientTestsAsync(String Name, String PhoneNumber, int Age, String Gender, HttpStatusCode ResponseCode)
    {
        var scopeFactory = _factory.Services;
        using (var scope = scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<DataContext>();

            // Initialize the database, so that 
            // changes made by other tests are reset. 
            await DBUtilities.InitializeDbForTestsAsync(context);

                // Arrange
                var request = new HttpRequestMessage(HttpMethod.Post, "api/patient")
                {
                    Content = new StringContent(JsonSerializer.Serialize(new Patient
                    {
                        Name = Name,
                        PhoneNumber = PhoneNumber,
                        Age = Age,
                        Gender = Gender
                    }), Encoding.UTF8, "application/json")
                };

                // Act
                var response = await httpClient.SendAsync(request);

            // Assert
            var StatusCode = response.StatusCode;
            Assert.Equal(ResponseCode, StatusCode);
        }
    }
}
}