using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Catalog.Api.Controllers;
using Catalog.Api.Entities;
using Catalog.Api.Repositories.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;



namespace Catalog.UnitTests.Controllers
{
    public class CategoriesControllerTests
    {
        //Fakes
        private readonly ICategoryRepository _categoryRepository;

        //Dummy Data Generator
        private readonly Fixture _fixture;

        //System under test
        private readonly CategoriesController _sut;
        public CategoriesControllerTests()
        {
            _categoryRepository = A.Fake<ICategoryRepository>();
            _sut = new CategoriesController(_categoryRepository);

            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                     .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllAsync_WhenThereAreCategories_ShouldReturnActionResultOfCategoriesWith200StatusCode()
        {
            //Arrange
            var mockCategories = _fixture.CreateMany<Category>(3).ToList();
            A.CallTo(() => _categoryRepository.GetAllAsync()).Returns(mockCategories);

            //Act
            var getCategoriesResult = await _sut.GetCategories();

            //Assert
            A.CallTo(() => _categoryRepository.GetAllAsync()).MustHaveHappenedOnceExactly();
            var okObjectResult = getCategoriesResult.Result.Should().BeOfType<OkObjectResult>().Subject;
            var result = okObjectResult.Value.Should().BeAssignableTo<IEnumerable<Category>>().Subject;
            result.Should().HaveCount(mockCategories.Count());
        }

        [Fact]
        public async Task GetCategory_WhenValidId_ShouldReturnActionResultOfCategoryWith200StatusCode()
        {
            //Arrange
            var category = _fixture.Create<Category>();
            A.CallTo(() => _categoryRepository.GetAsync(x => x.Id == category.Id)).Returns(category);

            //Act
            var getCategoryResult = await _sut.GetCategory(category.Id);

            //Assert
            var okObjectResult = getCategoryResult.Result.Should().BeOfType<OkObjectResult>().Subject;
            var result = okObjectResult.Value.Should().BeAssignableTo<Category>().Subject;
        }

        [Fact]
        public async Task GetCategory_WhenIdIsNegativeInValid_ShouldReturnActionResultOfCategoryWith200StatusCode()
        {
            //Act
            var getCategoryResult = await _sut.GetCategory(-1);

            //Assert
            getCategoryResult.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task CreateCategory_WhenIdValid_ShouldReturnActionResultOfCategoryWith201StatusCode()
        {
            var newCategory = _fixture.Create<Category>();
            //Act
            var result = await _sut.CreateCategory(newCategory);

            //Assert
  
            var okResult = result.Should().BeOfType<ActionResult<Category>>().Subject.Result.Should().BeAssignableTo<CreatedAtActionResult>().Subject;
            var category = okResult.Value.Should().BeAssignableTo<Category>().Subject;
            category.Id.Should().Be(newCategory.Id);
        }

    }
}