using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappers;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;

namespace CommerceCore.Application.Products.Commands.CreateReview;

public record CreateReviewCommand(
    int Rating,
    string Title,
    string Comment,
    Guid ProductId,
    string? FullName,
    string? PhoneNumber,
    string? Email
) : IRequest<ReviewResponse?>;

public class CreateReview(IApplicationDbContext context)
    : IRequestHandler<CreateReviewCommand, ReviewResponse?>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ReviewResponse?> Handle(
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

        if (isPhoneNumberExists || isEmailExists) return null;

        var review = new Review
        {
            Rating = request.Rating,
            Title = request.Title,
            Comment = request.Comment,
            CreatedDate = DateTime.Now,
            ProductId = request.ProductId,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);

        return review.ToDto();
    }
}
