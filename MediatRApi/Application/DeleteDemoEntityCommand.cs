using MediatR;
using MediatRApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MediatRApi.Application
{
    public class DeleteDemoEntityCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteDemoEntityCommandHandler(AppDbContext context) : IRequestHandler<DeleteDemoEntityCommand, bool>
    {
        public async Task<bool> Handle(DeleteDemoEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.DemoEntities.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            context.Remove(entity);
            return await context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
