using System.Net.Http;
using API;
using API.Routes;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace IntegrationTests
{
    public class Tests
    {
        private readonly HttpClient _client;

        public Tests()
        {
            var appFactoy = new WebApplicationFactory<Startup>();
            _client = appFactoy.CreateClient();
        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            _client.GetAsync(ApiRoutes.Group.GetAll);
        }
    }
}