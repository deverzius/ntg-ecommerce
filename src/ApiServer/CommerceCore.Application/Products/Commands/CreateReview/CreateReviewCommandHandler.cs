using CommerceCore.Application.Common.Mappings;
using CommerceCore.Application.Products.Dtos;
using CommerceCore.Domain.Entities;
using MediatR;

namespace CommerceCore.Application.Products.Commands.CreateReview;

public class CreateReviewCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateReviewCommand, ReviewResponseDto?>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ReviewResponseDto?> Handle(
        CreateReviewCommand request,
        CancellationToken cancellationToken
    )
    {
        var isPhoneNumberExists = _context
            .Reviews.Where(r =>
                r.ProductId == request.ProductId && r.PhoneNumber == request.PhoneNumber
            )
            .Any();

        var isEmailExists = _context
            .Reviews.Where(r => r.ProductId == request.ProductId && r.Email == request.Email)
            .Any();

        if (isPhoneNumberExists || isEmailExists)
        {
            return null;
        }

        var review = new Review
        {
            Rating = request.Rating,
            Title = request.Title,
            Comment = request.Comment,
            CreatedDate = DateTime.Now,
            ProductId = request.ProductId,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);

        return review.ToDto();
    }
}
