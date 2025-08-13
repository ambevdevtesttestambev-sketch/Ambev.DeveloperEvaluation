using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResponse>
{
    
    private readonly ISaleRepository _salesRepository;
    private readonly IMapper _mapper;

    public CreateSaleHandler(ISaleRepository salesRepository, IMapper mapper)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;        
    }    

public async Task<CreateSaleResponse> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var saleItems = request.Items?.ConvertAll(i =>
        {
            // Apply business rules for discounts and quantity
            if (i.Quantity > 20)
                throw new InvalidOperationException($"Cannot sell more than 20 units of product {i.ProductName}.");

            decimal discount = 0m;
            if (i.Quantity >= 10 && i.Quantity <= 20)
            {
                discount = 0.20m;
            }
            else if (i.Quantity > 4 && i.Quantity < 10)
            {
                discount = 0.10m;
            }
            else if (i.Quantity < 4 && i.Discount > 0)
            {
                throw new InvalidOperationException($"Cannot apply discount for less than 4 units of product {i.ProductName}.");
            }

            var totalAmount = i.Quantity * i.UnitPrice * (1 - discount);

            return new SaleItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = discount,
                TotalAmount = totalAmount
            };
        }) ?? new();

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = request.SaleNumber,
            SaleDate = request.SaleDate,
            CustomerId = request.CustomerId,
            CustomerName = request.CustomerName,
            BranchId = request.BranchId,
            BranchName = request.BranchName,
            TotalAmount = saleItems.Sum(x => x.TotalAmount),
            IsCancelled = request.IsCancelled,
            Items = saleItems
        };

        await _salesRepository.CreateAsync(sale, cancellationToken);

        return new CreateSaleResponse
        {
            Id = sale.Id,
            SaleNumber = sale.SaleNumber
        };
    }
}