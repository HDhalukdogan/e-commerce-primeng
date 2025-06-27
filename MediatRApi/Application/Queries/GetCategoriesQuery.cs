using AutoMapper;
using MediatR;
using MediatRApi.Application.Common.Responses;
using MediatRApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MediatRApi.Application.Queries
{
  public class GetCategoriesQuery : IRequest<IEnumerable<CategoryResponse>>
  {
  }

  public class GetCategoriesQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryResponse>>
  {
    public async Task<IEnumerable<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
      var categories = await context.Categories
          .AsNoTracking()
          .ToListAsync(cancellationToken);

      var response = mapper.Map<IEnumerable<CategoryResponse>>(categories);
      return response;
    }
  }
}
