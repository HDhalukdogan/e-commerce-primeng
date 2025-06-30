using MediatR;
using MediatRApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MediatRApi.Application.Commands
{
  public class DeleteCategoryCommand(int id) : IRequest
  {
    public int Id => id;
  }

  public class DeleteCategoryCommandHandler(AppDbContext context) : IRequestHandler<DeleteCategoryCommand>
  {
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
      var category = await context.Categories
        .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

      if (category == null)
        throw new KeyNotFoundException($"Category with Id {request.Id} not found.");

      context.Categories.Remove(category);
      await context.SaveChangesAsync(cancellationToken);

    }
  }
}
