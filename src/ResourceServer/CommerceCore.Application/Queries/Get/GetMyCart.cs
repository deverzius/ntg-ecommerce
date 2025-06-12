using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Queries.Get;

public record GetMyCartQuery(Guid UserId) : IRequest<CartResponse?>;

public class GetMyCartQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetMyCartQuery, CartResponse?>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<CartResponse?> Handle(
        GetMyCartQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await _context.Carts.FirstOrDefaultAsync(
            c => c.UserId == request.UserId,
            cancellationToken
        );

        return result?.ToDto();
    }
}
