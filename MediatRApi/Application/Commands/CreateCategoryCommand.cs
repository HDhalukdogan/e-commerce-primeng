using MediatR;
using MediatRApi.Data;
using MediatRApi.Entities;

namespace MediatRApi.Application.Commands
{
  public class CreateCategoryCommand : IRequest
  {
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile? Image { get; set; }
  }

  public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand>
  {
    public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
      byte[]? imageBytes = null;
      if (request.Image != null)
      {
        using var memoryStream = new MemoryStream();
        await request.Image.CopyToAsync(memoryStream, cancellationToken);
        imageBytes = memoryStream.ToArray();
      }

      var category = new Category
      {
        Name = request.Name,
        Description = request.Description,
        Image = imageBytes
      };

      context.Categories.Add(category);
      await context.SaveChangesAsync(cancellationToken);
    }
  }
}
