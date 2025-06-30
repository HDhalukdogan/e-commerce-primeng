using MediatR;
using MediatRApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MediatRApi.Application.Commands
{
  public class UpdateProductCommand : IRequest
  {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public bool? IsShow { get; set; }
    public bool? IsCarousel { get; set; }
    public int? CategoryId { get; set; }
  }

  public class UpdateProductCommandHandler(AppDbContext context) : IRequestHandler<UpdateProductCommand>
  {
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
      var product = await context.Products
        .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

      if (product == null)
        throw new KeyNotFoundException($"Product with Id {request.Id} not found.");

      if (request.Name is not null)
        product.Name = request.Name;

      if (request.Description is not null)
        product.Description = request.Description;

      if (request.Price.HasValue)
        product.Price = request.Price.Value;

      if (request.IsShow.HasValue)
        product.IsShow = request.IsShow.Value;

      if (request.IsCarousel.HasValue)
        product.IsCarousel = request.IsCarousel.Value;

      if (request.CategoryId.HasValue)
        product.CategoryId = request.CategoryId.Value;

      await context.SaveChangesAsync(cancellationToken);
    }
  }
}
