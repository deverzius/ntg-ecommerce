using CommerceCore.Application.Commands.Create;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using Moq;

namespace CommerceCore.Application.UnitTests.Commands.Create;

public class CreateCategoryCommandHandlerTests
{
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    private CreateCategoryCommandHandler CreateHandler() =>
        new(_categoryRepositoryMock.Object, _unitOfWorkMock.Object);

    [Fact]
    public async Task Handle_ValidRequest_CreatesCategoryAndReturnsDto()
    {
        // Arrange
        var command = new CreateCategoryCommand("Electronics", "All electronic items", null);
        var category = new Category
        {
            Name = command.Name,
            Description = command.Description,
            ParentCategoryId = command.ParentCategoryId
        };

        _categoryRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Callback<Category, CancellationToken>((c, _) =>
            {
                c.Id = Guid.NewGuid();
            });

        _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        _categoryRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.Description, result.Description);
    }
}