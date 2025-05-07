using CommerceCore.Application.Products.Dtos;
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
) : IRequest<ReviewResponseDto?> { }
