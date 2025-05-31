using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

public class Order
{
    public Guid Id { get; init; }
    public Guid? UserId { get; init; }
    public required decimal TotalPrice { get; init; }
    public DateTime CreatedDate { get; init; }

    public virtual ICollection<OrderItem> OrderItems { get; init; } = [];
}
