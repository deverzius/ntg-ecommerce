using CommerceCore.Application.Products.Dtos;
using MediatR;

namespace CommerceCore.Application.Products.Commands.CreateReview;

public record CreateReviewCommand(
    Guid Id,
    int Rating,
    string Title,
    string Comment,
    DateTime CreatedDate,
    Guid ProductId,
    string? FullName,
    string? PhoneNumber,
    string? Email
) : IRequest<ReviewResponseDto> { }
