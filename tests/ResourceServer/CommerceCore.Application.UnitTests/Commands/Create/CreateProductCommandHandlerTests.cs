using CommerceCore.Application.Commands.Create;
using CommerceCore.Application.Common.DTOs;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.Exceptions;
using Moq;

namespace CommerceCore.Application.UnitTests.Commands.Create;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock = new();
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    private CreateProductCommandHandler CreateHandler() =>
        new(_productRepositoryMock.Object, _categoryRepositoryMock.Object, _unitOfWorkMock.Object);

    [Fact]
    public async Task Handle_CategoryNotFound_ThrowsAppException()
    {
        // Arrange
        var command = new CreateProductCommand(
            new CreateProductDTO(
            "Test Product",
            "Test Description",
            100,
            Guid.NewGuid(),
            new List<CreateProductVariantDTO>())
        );

        _categoryRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Category)null!);

        var handler = CreateHandler();

        // Act & Assert
        await Assert.ThrowsAsync<AppException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ValidRequest_CreatesProductAndReturnsDto()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var category = new Category { Id = categoryId, Name = "Cat", Description = "Desc" };
        var variants = new List<CreateProductVariantDTO>
    {
        new("Color", "Red", "Red", new List<Guid>()),
        new("Size", "M", "Medium", new List<Guid>())
    };
        var command = new CreateProductCommand(
            new CreateProductDTO(
            "Test Product",
            "Test Description",
            100,
            categoryId,
            variants
            )
        );

        _categoryRepositoryMock.Setup(r => r.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);
        _productRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        _productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal("Test Product", result.Name);
        Assert.Equal(2, result.ProductVariants.Count());
        Assert.Contains(result.ProductVariants, v => v.Name == "Color" && v.Value == "Red");
        Assert.Contains(result.ProductVariants, v => v.Name == "Size" && v.Value == "M");
    }
}