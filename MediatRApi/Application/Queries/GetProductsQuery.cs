using AutoMapper;
using MediatR;
using MediatRApi.Application.Common.Responses;
using MediatRApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MediatRApi.Application.Queries
{
  public class GetProductsQuery : IRequest<IEnumerable<ProductResponse>>
  {
  }

  public class GetProductsQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetProductsQuery, IEnumerable<ProductResponse>>
  {
    public async Task<IEnumerable<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
      var products = await context.Products
          .AsNoTracking()
          .ToListAsync(cancellationToken);

      var response = mapper.Map<IEnumerable<ProductResponse>>(products);
      return response;
    }
  }
}
