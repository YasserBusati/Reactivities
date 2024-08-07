using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities;

public class List
{
    public class Query: IRequest<List<Activity>> {}

    public class Handler : IRequestHandler<Query, List<Activity>>
    {
        private readonly AppDbContext _context;
        public Handler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Activities.ToListAsync();
        }
    }
}