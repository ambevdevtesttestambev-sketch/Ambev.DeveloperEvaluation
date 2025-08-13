using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, GetSalesResult>
{
    private readonly ISaleRepository _salesRepository;
    private readonly IMapper _mapper;

    public GetSaleByIdHandler(ISaleRepository salesRepository,
       IMapper mapper)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
    }
    

public async Task<GetSalesResult> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _salesRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null)
            return null;

        // Use AutoMapper to map Sale to GetSalesResult
        return _mapper.Map<GetSalesResult>(sale);
    }
}