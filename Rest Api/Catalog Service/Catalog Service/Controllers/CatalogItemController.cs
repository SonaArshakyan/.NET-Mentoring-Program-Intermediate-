using Catalog_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Service.Controllers;

[Route("api/v1/catalogItems")]
[ApiController]
public class CatalogItemController : ControllerBase
{
    private readonly ICatalogItemService  _catalogItemService;
    private readonly ILogger<CatalogItemController> _logger;

    public CatalogItemController(ICatalogItemService catalogItemService, ILogger<CatalogItemController> logger)
    {
        _catalogItemService = catalogItemService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new catalog Item
    /// </summary>
    /// <response code="201">The Item was created successfully. </response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="500">A server fault occurred</response>
    
    [HttpPost]//(Name = nameof(CreateCatalogItem))
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCatalogItem([FromBody] CatalogItem catalogItem)
    {
        var result = await _catalogItemService.CreateItem(catalogItem);

        return CreatedAtAction(
            actionName: nameof(CreateCatalogItem),
            routeValues: new { ID = result.Id },
            value: result);
    }

    /// <summary>
    /// Delete catalog Item
    /// </summary>
    /// <response code="204">The Item was deleted successfully.</response>
    /// <response code="404">A Item having specified catalog Item id was not found</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="500">A server fault occurred</response>
    
    [HttpDelete("{itemId:int}")]//Name = nameof(DeleteCatalogItem)
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCatalogItem([FromRoute] int itemId)
    {
        await _catalogItemService.DeleteItemAsync(itemId);
        return NoContent();
    }

    /// <summary>
    /// Update an existing catalog Item
    /// </summary>
    /// <response code="204">The Item was updated successfully</response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="404">The Item was not found for specified catalog Item id</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="500">A server fault occurred</response>
    
    [HttpPut]//(Name = nameof(UpdateCatalogItem))
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCatalogItem([FromBody] CatalogItem  catalogItem)
    {
        await _catalogItemService.UpdateItemAsync(catalogItem);
        return NoContent();
    }
}
