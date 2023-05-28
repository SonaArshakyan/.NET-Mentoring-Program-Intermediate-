using Catalog_Service.Models;
using Catalog_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Service.Controllers;

[Route("api/v1/catalogs")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> _logger;
    private readonly ICatalogService _catalogService;

    public CatalogController(ILogger<CatalogController> logger, ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    /// <summary>
    /// Get a paginated list of categories.
    /// </summary>
    /// <response code="200">A List of categories </response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="500">A server fault occurred</response>
    
    [HttpGet] //(Name = nameof(GetCategoryList))
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategoryList()
    {
        var result = await _catalogService.GetCategoryListAsync();
        return Ok(result);
    }

    /// <summary>
    /// Get a paginated list of catalog Items.
    /// </summary>
    /// <response code="200">A paginated list of items </response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="500">A server fault occurred</response>
    
    [HttpGet("{id:int}")] //(Name = nameof(GetCatalogItemList))
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCatalogItemList([FromQuery] int categoryId, [FromQuery] PaginationFilter filter)
    {
        var result =  await _catalogService.GetCatalogItemsAsync(categoryId , filter);
        return Ok(result);
    }
}