using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.Exceptions;
using MediatR;

namespace CommerceCore.Application.Commands.Create;

public record CreateReviewCommand(
    int Rating,
    string Title,
    string Comment,
    Guid ProductId,
    string? FullName,
    string? PhoneNumber,
    string? Email
) : IRequest<ReviewResponse?>;

public class CreateReviewCommandHandler(IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateReviewCommand, ReviewResponse?>
{
    public async Task<ReviewResponse?> Handle(
        CreateReviewCommand request,
        CancellationToken cancellationToken
    )
    {
        var isPhoneNumberExists = await reviewRepository.AnyAsync(r =>
            r.ProductId == request.ProductId && r.PhoneNumber == request.PhoneNumber,
            cancellationToken
        );

        var isEmailExists = await reviewRepository.AnyAsync(r =>
            r.ProductId == request.ProductId && r.Email == request.Email,
            cancellationToken
        );

        if (isPhoneNumberExists || isEmailExists)
        {
            throw new AppException(400, "You have already submitted a review for this product with the same phone number or email.");
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
            Email = request.Email
        };

        await reviewRepository.AddAsync(review, cancellationToken);

        await unitOfWork.SaveAsync(cancellationToken);

        return review.ToDto();
    }
}
