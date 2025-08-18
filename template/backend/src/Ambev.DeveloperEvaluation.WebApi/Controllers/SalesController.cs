using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Events;
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
        var saleCreatedEvent = new SaleCreatedEvent(result.Id);
        _logger.LogInformation("Event: SaleCreated | SaleId: {SaleId} | EventTime: {EventTime}", saleCreatedEvent.SaleId, saleCreatedEvent.CreatedAt);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
        

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteSaleCommand { Id = id };
        await _mediator.Send(command);
        var saleCancelledEvent = new SaleCancelledEvent(id);
        _logger.LogInformation("Event: SaleCancelled | SaleId: {SaleId} | EventTime: {EventTime}", saleCancelledEvent.SaleId, saleCancelledEvent.CancelledAt);
        return NoContent();
    }    

    [HttpPost("{saleId}/items/{itemId}/cancel")]
    public IActionResult CancelItem(Guid saleId, Guid itemId)
    {
        var itemCancelledEvent = new ItemCancelledEvent(saleId, itemId);
        _logger.LogInformation("Event: ItemCancelled | SaleId: {SaleId} | ItemId: {ItemId} | EventTime: {EventTime}", itemCancelledEvent.SaleId, itemCancelledEvent.ItemId, itemCancelledEvent.CancelledAt);
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