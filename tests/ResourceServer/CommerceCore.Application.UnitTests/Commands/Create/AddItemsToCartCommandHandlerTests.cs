using CommerceCore.Application.Commands.Create;
using CommerceCore.Application.Common.DTOs;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.Exceptions;
using Moq;

namespace CommerceCore.Application.UnitTests.Commands.Create;

public class AddItemsToCartCommandHandlerTests
{
    private readonly Mock<ICartRepository> _cartRepositoryMock = new();
    private readonly Mock<IProductVariantRepository> _variantRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    private AddItemsToCartCommandHandler CreateHandler() =>
        new(_cartRepositoryMock.Object, _variantRepositoryMock.Object, _unitOfWorkMock.Object);

    [Fact]
    public async Task Handle_CartNotFound_ThrowsAppException()
    {
        // Arrange
        var command = new AddItemsToCartCommand(Guid.NewGuid(), new List<CreateCartItemDTO>());
        _cartRepositoryMock.Setup(r => r.GetCartByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Cart)null!);

        var handler = CreateHandler();

        // Act & Assert
        await Assert.ThrowsAsync<AppException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ProductVariantNotFound_ThrowsAppException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var cart = new Cart { UserId = Guid.NewGuid(), CartItems = new List<CartItem>() };
        var command = new AddItemsToCartCommand(userId, new List<CreateCartItemDTO>
        {
            new(Guid.NewGuid(), 1)
        });

        _cartRepositoryMock.Setup(r => r.GetCartByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);
        _variantRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductVariant)null!);

        var handler = CreateHandler();

        // Act & Assert
        await Assert.ThrowsAsync<AppException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ValidRequest_AddsItemsAndSaves()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var variantId = Guid.NewGuid();
        var cart = new Cart { UserId = Guid.NewGuid(), CartItems = new List<CartItem>() };
        var productVariant = new ProductVariant { Id = variantId, Name = "Size", Value = "M", DisplayValue = "Medium" };
        var command = new AddItemsToCartCommand(userId, new List<CreateCartItemDTO>
        {
            new(variantId, 2)
        });

        _cartRepositoryMock.Setup(r => r.GetCartByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);
        _variantRepositoryMock.Setup(r => r.GetByIdAsync(variantId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(productVariant);
        _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

        var handler = CreateHandler();

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Single(cart.CartItems);
        Assert.Equal(2, cart.CartItems.ToList()[0].Quantity);
        Assert.Equal(productVariant, cart.CartItems.ToList()[0].ProductVariant);
        _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
