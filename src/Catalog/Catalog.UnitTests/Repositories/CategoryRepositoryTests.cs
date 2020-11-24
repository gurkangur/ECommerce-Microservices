using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Catalog.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Catalog.UnitTests.Repositories
{
    public class CategoryRepositoryTests : TestBase
    {
        private readonly CategoryRepository _sut;
        public CategoryRepositoryTests()
        {
            _sut = new CategoryRepository(_context);
        }
        [Fact]
        public async Task GetAllAsync_WhenThereAreCategories_ShouldReturnCategories()
        {
            //Arrange
            var categories = await _context.Categories.ToListAsync();

            //Act
            var result = await _sut.GetAllAsync();

            //Assert
            result.Should().BeAssignableTo<IEnumerable<Category>>();
            result.Should().NotBeNull();
            result.Count().Should().Be(categories.Count());
        }
    }
}