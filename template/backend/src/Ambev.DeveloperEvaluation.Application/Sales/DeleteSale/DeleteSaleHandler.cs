using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
{
    private readonly ISaleRepository _salesRepository;
    private readonly IMapper _mapper;

    public DeleteSaleHandler(ISaleRepository salesRepository, IMapper mapper)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
    }

    public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        await _salesRepository.DeleteAsync(request.Id, cancellationToken);
        return new DeleteSaleResponse { Success = true };
    }
}