using MediatR;
using MediatRApi.Data;
using MediatRApi.Entities;

namespace MediatRApi.Application.Commands
{
  public class CreateProductCommand : IRequest
  {
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsShow { get; set; } = true;
    public bool IsCarousel { get; set; } = false;
    public int CategoryId { get; set; }
  }

  public class CreateProductCommandHandler(AppDbContext context) : IRequestHandler<CreateProductCommand>
  {
    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

      var product = new Product
      {
        Name = request.Name,
        Description = request.Description,
        Price = request.Price,
        IsShow = request.IsShow,
        IsCarousel = request.IsCarousel,
        CategoryId = request.CategoryId
      };

      context.Products.Add(product);
      await context.SaveChangesAsync(cancellationToken);
    }
  }
}
