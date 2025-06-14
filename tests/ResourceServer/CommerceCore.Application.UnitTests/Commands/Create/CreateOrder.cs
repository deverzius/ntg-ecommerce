using CommerceCore.Application.Commands.Create;
using CommerceCore.Application.Common.DTOs;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.Exceptions;
using Moq;

namespace CommerceCore.Application.UnitTests.Commands.Create;

public class CreateOrderCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
    private readonly Mock<ICartRepository> _cartRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    private CreateOrderCommandHandler CreateHandler() =>
        new(_orderRepositoryMock.Object, _cartRepositoryMock.Object, _unitOfWorkMock.Object);

    [Fact]
    public async Task Handle_CartNotFound_ThrowsAppException()
    {
        // Arrange
        var command = new CreateOrderCommand(Guid.NewGuid(), new CreateOrderDTO("123 Main St", "test@example.com", "Test User", new List<Guid>()));
        _cartRepositoryMock.Setup(r => r.GetCartByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Cart)null!);

        var handler = CreateHandler();

        // Act & Assert
        await Assert.ThrowsAsync<AppException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ProductVariantNotInCart_ThrowsAppException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var variantId = Guid.NewGuid();
        var cart = new Cart { UserId = Guid.NewGuid(), CartItems = new List<CartItem>() };
        var command = new CreateOrderCommand(userId, new CreateOrderDTO("123 Main St", "test@example.com", "Test User", new List<Guid> { variantId }));

        _cartRepositoryMock.Setup(r => r.GetCartByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);

        var handler = CreateHandler();

        // Act & Assert
        await Assert.ThrowsAsync<AppException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ValidRequest_CreatesOrderAndReturnsDto()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var variantId = Guid.NewGuid();
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Price = 10m,
            Description = "desc",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Category = new Category { Name = "Test Category", Description = "Category Description" }
        };
        var productVariant = new ProductVariant
        {
            Id = variantId,
            Name = "Color",
            Value = "Red",
            DisplayValue = "Red",
            Product = product
        };
        var cartItem = new CartItem { Quantity = 2, ProductVariant = productVariant };
        var cart = new Cart { UserId = Guid.NewGuid(), CartItems = new List<CartItem> { cartItem } };
        var command = new CreateOrderCommand(userId, new CreateOrderDTO("123 Main St", "test@example.com", "Test User", new List<Guid> { variantId }));

        _cartRepositoryMock.Setup(r => r.GetCartByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);
        _orderRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        _orderRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal("Test Product", result.OrderItems.ToList()[0].ProductName);
        Assert.Equal("Color", result.OrderItems.ToList()[0].ProductVariantName);
        Assert.Equal("Red", result.OrderItems.ToList()[0].ProductVariantValue);
        Assert.Equal(2, result.OrderItems.ToList()[0].Quantity);
    }
}