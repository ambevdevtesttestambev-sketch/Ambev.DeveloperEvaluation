using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<SalesController> _logger;

    public SalesController(IMediator mediator, ILogger<SalesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleCommand command)
    {
        var result = await _mediator.Send(command);
        _logger.LogInformation("Event: SaleCreated | SaleId: {SaleId}", result.Id);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
        

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteSaleCommand { Id = id };
        await _mediator.Send(command);
        _logger.LogInformation("Event: SaleCancelled | SaleId: {SaleId}", id);
        return NoContent();
    }
    
    [HttpPost("{saleId}/items/{itemId}/cancel")]
    public IActionResult CancelItem(Guid saleId, Guid itemId)
    {
        //not implemented in the application layer as per the requirements
        _logger.LogInformation("Event: ItemCancelled | SaleId: {SaleId} | ItemId: {ItemId}", saleId, itemId);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetSaleByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        if (result == null)
            return NotFound();
        return Ok(result);
    }
}