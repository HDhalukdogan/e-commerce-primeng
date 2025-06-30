using MediatR;
using MediatRApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MediatRApi.Application.Commands
{
  public class DeleteProductCommand(int id) : IRequest
  {
    public int Id => id;
  }

  public class DeleteProductCommandHandler(AppDbContext context) : IRequestHandler<DeleteProductCommand>
  {
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
      var product = await context.Products
        .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

      if (product == null)
        throw new KeyNotFoundException($"Product with Id {request.Id} not found.");

      context.Products.Remove(product);
      await context.SaveChangesAsync(cancellationToken);
    }
  }
}
