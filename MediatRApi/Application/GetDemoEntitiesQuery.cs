using AutoMapper;
using MediatR;
using MediatRApi.Application.Common.Models;
using MediatRApi.Data;
using MediatRApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatRApi.Application
{
    public class GetDemoEntitiesQuery : IRequest<IEnumerable<DemoEntityResponse>>
    { 
    }

    public class GetDemoEntitiesQueryHandler : IRequestHandler<GetDemoEntitiesQuery, IEnumerable<DemoEntityResponse>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetDemoEntitiesQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DemoEntityResponse>> Handle(GetDemoEntitiesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.DemoEntities.ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<DemoEntityResponse>>(entities);
        }
    }
}
