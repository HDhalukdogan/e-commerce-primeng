using MediatR;
using MediatRApi.Data;
using MediatRApi.Entities;

namespace MediatRApi.Application
{
    public class CreateDemoEntityCommand : IRequest<DemoEntity>
    {
        public string Name { get; set; } = null!;
    }
    public class CreateDemoEntityCommandHandler(AppDbContext context) : IRequestHandler<CreateDemoEntityCommand, DemoEntity>
    {
        public async Task<DemoEntity> Handle(CreateDemoEntityCommand request, CancellationToken cancellationToken)
        {
           var entityEntry = await context.DemoEntities.AddAsync(new DemoEntity
            {
                Name = request.Name
            }, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return entityEntry.Entity;
        }
    }
}
