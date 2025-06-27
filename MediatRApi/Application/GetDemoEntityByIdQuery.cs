using MediatR;
using MediatRApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MediatRApi.Application
{
    public class GetDemoEntityByIdQuery : IRequest<Entities.DemoEntity?>
    {
        public int Id { get; set; }
    }

    public class GetDemoEntityByIdQueryHandler(AppDbContext context) : IRequestHandler<GetDemoEntityByIdQuery, Entities.DemoEntity?>
    {
        public async Task<Entities.DemoEntity?> Handle(GetDemoEntityByIdQuery request, CancellationToken cancellationToken)
        {
            return await context.DemoEntities.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
