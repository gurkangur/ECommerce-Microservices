using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api;
using Catalog.Api.Entities;
using Catalog.IntegrationTests.Common;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Catalog.IntegrationTests.Controllers
{
    public class CategoriesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CategoriesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCategories()
        {
            // Act
            var client = _factory.GetAnonymousClient();
            var response = await client.GetAsync("/api/v1/categories/"); ;

            // Assert
            response.EnsureSuccessStatusCode();
            var categories = await Utilities.GetResponseContent<IEnumerable<Category>>(response);
            categories.Count().Should().Be(3);
            categories.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetCategory()
        {
            // Act
            var client = _factory.GetAnonymousClient();
            var response = await client.GetAsync("/api/v1/categories/1"); ;

            // Assert
            response.EnsureSuccessStatusCode();
            var category = await Utilities.GetResponseContent<Category>(response);
            category.Id.Should().Be(1);
        }

        [Fact]
        public async Task GetCategory_WhenIdIsNegative()
        {
            // Act
            var client = _factory.GetAnonymousClient();
            var response = await client.GetAsync("/api/v1/categories/-1"); ;

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task GetCategory_WhenIdNotFound()
        {
            // Act
            var client = _factory.GetAnonymousClient();
            var response = await client.GetAsync("/api/v1/categories/99"); ;

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}