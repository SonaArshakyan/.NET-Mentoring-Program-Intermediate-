using Catalog_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Service.Controllers;

[Route("api/v1/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryService  categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new Category
    /// </summary>
    /// <response code="201">The Category was created successfully. </response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="500">A server fault occurred</response>
    
    [HttpPost]//(Name = nameof(CreateCategory))
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCategory([FromBody] Category category)
    {
        var result = await _categoryService.CreateCategoryAsync(category);

        return CreatedAtAction(
            actionName: nameof(CreateCategory),
            routeValues: new { ID = result.Id },
            value: result);
    }

    /// <summary>
    /// Delete Category
    /// </summary>
    /// <response code="204">The Category was deleted successfully.</response>
    /// <response code="404">A Category having specified Category id was not found</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="500">A server fault occurred</response>
    
    [HttpDelete("{categoryId:int}")] //Name = nameof(DeleteCategory)
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
    {
        await _categoryService.DeleteCategoryAsync(categoryId);
        return NoContent();
    }

    /// <summary>
    /// Update an existing Category
    /// </summary>
    /// <response code="204">The Category was updated successfully</response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="404">The Category was not found for specified Category id</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="500">A server fault occurred</response>
    
    [HttpPut]//(Name = nameof(UpdateCategory))
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCategory([FromBody] Category  category)
    {
        await _categoryService.UpdateCategoryAsync(category);
        return NoContent();
    }
}
