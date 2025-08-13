using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdQuery : IRequest<GetSalesResult>
{
    public Guid Id { get; set; }
}