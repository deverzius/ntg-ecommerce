using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Products.Dtos;

public class ReviewResponseDto(Review review)
{
    public Guid Id => review.Id;
    public int Rating => review.Rating;
    public string Title => review.Title;
    public string Comment => review.Comment;
    public DateTime CreatedDate => review.CreatedDate;
    public string? FullName => review.FullName;
    public string? PhoneNumber => review.PhoneNumber;
    public string? Email => review.Email;
}
