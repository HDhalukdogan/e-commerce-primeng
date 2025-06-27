using MediatR;
using MediatRApi.Data;
using MediatRApi.Entities;

namespace MediatRApi.Application
{
    public class UpdateDemoEntityCommand : IRequest<DemoEntity>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class UpdateDemoEntityCommandHandler(AppDbContext context) : IRequestHandler<UpdateDemoEntityCommand, DemoEntity>
    {
        public async Task<DemoEntity> Handle(UpdateDemoEntityCommand request, CancellationToken cancellationToken)
        {
            var entityEntry = context.Update(new Entities.DemoEntity
            {
                Id = request.Id,
                Name = request.Name
            });
            await context.SaveChangesAsync(cancellationToken);

            return entityEntry.Entity;
        }
    }
}
