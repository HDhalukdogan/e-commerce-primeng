using AutoMapper;
using MediatR;
using MediatRApi.Application.Common.Responses;
using MediatRApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MediatRApi.Application.Queries
{
  public class GetHomePageQuery : IRequest<HomePageResponse>
  {
  }
  public class GetHomePageQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetHomePageQuery, HomePageResponse>
  {
    public async Task<HomePageResponse> Handle(GetHomePageQuery request, CancellationToken cancellationToken)
    {
      var products = await context.Products.Where(s => s.IsShow)
        .Include(s => s.ProductImages)
        .AsNoTracking()
        .ToListAsync(cancellationToken);

      var homePageResponse = new HomePageResponse();
      var cImages = products.Where(s=>s.IsCarousel).SelectMany(p => p.ProductImages)
        .Where(pi => pi.IsCover)
        .ToList();
      homePageResponse.CarouselImages = mapper.Map<IEnumerable<ProductImageResponse>>(cImages);
      homePageResponse.HomeProducts = mapper.Map<IEnumerable<HomeProduct>>(products);

      return homePageResponse;
    }
  }
}
