using MediatR;
using MediatRApi.Data;
using Microsoft.EntityFrameworkCore;
using MediatRApi.Entities; // Ensure ProductImage is accessible

namespace MediatRApi.Application.Commands
{
  public class CreateProductImageCommand : IRequest
  {
    public int ProductId { get; set; }
    public string FileName { get; set; } = null!;
    public byte[] Image { get; set; } = [];
    public bool IsCover { get; set; } = false;
  }

  public class CreateProductImageCommandHandler(AppDbContext context) : IRequestHandler<CreateProductImageCommand>
  {
    public async Task Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
    {
      var product = await context.Products
        .Include(p => p.ProductImages)
        .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

      if (product == null)
        throw new KeyNotFoundException($"Product with Id {request.ProductId} not found.");

      if (!product.ProductImages.Any())
        request.IsCover = true;

      var productImage = new ProductImage
      {
        ProductId = request.ProductId,
        FileName = request.FileName,
        Image = request.Image,
        IsCover = request.IsCover
      };

      context.Add(productImage);
      await context.SaveChangesAsync(cancellationToken);
    }
  }
}
