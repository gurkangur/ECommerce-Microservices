using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Catalog.Api.Entities;
using Catalog.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _categoryRepository.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var category = await _categoryRepository.GetAsync(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            await _categoryRepository.CreateAsync(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == id);

            if (category == null)
            {
                return NotFound(new { Message = $"Category with id {id} not found." });
            }

            await _categoryRepository.DeleteAsync(category);

            return NoContent();
        }

    }
}