using CommerceCore.Application.Products.Dtos;
using CommerceCore.Domain.Entities;
using MediatR;

namespace CommerceCore.Application.Products.Commands.CreateReview;

public class CreateReviewCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateReviewCommand, ReviewResponseDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ReviewResponseDto> Handle(
        CreateReviewCommand request,
        CancellationToken cancellationToken
    )
    {
        var review = new Review
        {
            Id = request.Id,
            Rating = request.Rating,
            Title = request.Title,
            Comment = request.Comment,
            CreatedDate = request.CreatedDate,
            ProductId = request.ProductId,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);

        return new ReviewResponseDto(review);
    }
}
