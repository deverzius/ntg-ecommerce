using System.ComponentModel.DataAnnotations;
using CommerceCore.Domain.Enums;

namespace CommerceCore.Domain.Entities;

public class Order
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public required decimal TotalPrice { get; init; }
    public required string ShippingAddress { get; init; }
    [EmailAddress]
    public required string CustomerEmail { get; init; }
    public required string CustomerName { get; init; }
    public DateTime CreatedDate { get; init; }
    public EOrderStatus Status { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; init; } = [];
}
