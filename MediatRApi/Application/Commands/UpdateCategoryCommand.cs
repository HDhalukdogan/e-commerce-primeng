using MediatR;
using MediatRApi.Data;
using MediatRApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MediatRApi.Application.Commands
{
  public class UpdateCategoryCommand : IRequest
  {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IFormFile? Image { get; set; }
  }

  public class UpdateCategoryCommandHandler(AppDbContext context) : IRequestHandler<UpdateCategoryCommand>
  {


    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
      var category = await context.Categories
        .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

      if (category == null)
        throw new KeyNotFoundException($"Category with Id {request.Id} not found.");

      if (request.Name is not null)
        category.Name = request.Name;

      if (request.Description is not null)
        category.Description = request.Description;

      if (request.Image != null)
      {
        using var memoryStream = new MemoryStream();
        await request.Image.CopyToAsync(memoryStream, cancellationToken);
        category.Image = memoryStream.ToArray();
      }

      await context.SaveChangesAsync(cancellationToken);
    }
  }
}
