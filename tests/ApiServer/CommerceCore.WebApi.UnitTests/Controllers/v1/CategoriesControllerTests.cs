using CommerceCore.Application.Categories.Commands.CreateCategory;
using CommerceCore.Application.Categories.Commands.DeleteCategory;
using CommerceCore.Application.Categories.Commands.UpdateCategory;
using CommerceCore.Application.Categories.Dtos;
using CommerceCore.Application.Categories.Queries.GetCategoriesWithPagination;
using CommerceCore.Application.Categories.Queries.GetCategory;
using CommerceCore.Application.Common.Models;
using CommerceCore.WebApi.Controllers.v1;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CommerceCore.WebApi.UnitTests.Controllers.v1;

public class CategoriesControllerTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly CategoriesController _controller;

    public CategoriesControllerTests()
    {
        _senderMock = new Mock<ISender>();
        _controller = new CategoriesController();

        var httpContext = new DefaultHttpContext();
        _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
    }

    [Fact]
    public async Task GetCategoriesWithPagination_ReturnsOkWithPaginatedViewModel()
    {
        // Arrange
        var query = new GetCategoriesWithPaginationQuery(1, 10);
        var categoryDtos = new List<CategoryResponseDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Category 1",
                Description = "Desc 1",
                ParentCategoryId = null,
            },
        };
        var paginatedListDto = new PaginatedList<CategoryResponseDto>(categoryDtos, 1, 1, 10);

        _senderMock
            .Setup(s => s.Send(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginatedListDto);

        // Act
        var result = await _controller.GetCategoriesWithPagination(_senderMock.Object, query);

        // Assert
        var okResult = Assert.IsType<Ok<PaginatedListViewModel<SimpleCategoryViewModel>>>(result);
        Assert.NotNull(okResult.Value);
        Assert.Single(okResult.Value.Items);
        Assert.Equal(categoryDtos[0].Name, okResult.Value.Items.First().Name);
        Assert.Equal(paginatedListDto.PageNumber, okResult.Value.PageNumber);
    }

    [Fact]
    public async Task GetCategory_WhenCategoryExists_ReturnsOkWithCategoryViewModel()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var categoryDto = new CategoryResponseDto
        {
            Id = Guid.NewGuid(),
            Name = "Category 1",
            Description = "Desc 1",
            ParentCategoryId = null,
        };

        _senderMock
            .Setup(s =>
                s.Send(
                    It.Is<GetCategoryQuery>(q => q.Id == categoryId),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(categoryDto);

        // Act
        var result = await _controller.GetCategory(_senderMock.Object, categoryId);

        // Assert
        var okResult = Assert.IsType<Ok<SimpleCategoryViewModel>>(result.Result); // TypedResults wraps the result
        Assert.NotNull(okResult.Value);
        Assert.Equal(categoryDto.Name, okResult.Value.Name);
    }

    [Fact]
    public async Task GetCategory_WhenCategoryDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        _senderMock
            .Setup(s =>
                s.Send(
                    It.Is<GetCategoryQuery>(q => q.Id == categoryId),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((CategoryResponseDto)null!);

        // Act
        var result = await _controller.GetCategory(_senderMock.Object, categoryId);

        // Assert
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public async Task PostCategory_ReturnsCreatedWithCategoryViewModel()
    {
        // Arrange
        var command = new CreateCategoryCommand("New Category", "New Desc", null);
        var categoryDto = new CategoryResponseDto
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            ParentCategoryId = command.ParentCategoryId,
        };

        _senderMock
            .Setup(s => s.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(categoryDto);

        // Act
        var result = await _controller.PostCategory(_senderMock.Object, command);

        // Assert
        var createdResult = Assert.IsType<Created<SimpleCategoryViewModel>>(result);
        Assert.NotNull(createdResult.Value);
        Assert.Equal(categoryDto.Name, createdResult.Value.Name);
        Assert.Equal(nameof(_controller.GetCategory), createdResult.Location);
    }

    [Fact]
    public async Task PutCategory_WhenIdMatchesCommandAndCategoryExists_ReturnsNoContent()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var command = new UpdateCategoryCommand(
            categoryId,
            "Updated Category",
            "Updated Desc",
            null
        );

        _senderMock.Setup(s => s.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(true); // Simulate successful update

        // Act
        var result = await _controller.PutCategory(_senderMock.Object, categoryId, command);

        // Assert
        Assert.IsType<NoContent>(result.Result);
    }

    [Fact]
    public async Task PutCategory_WhenIdDoesNotMatchCommand_ReturnsBadRequest()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var differentCategoryId = Guid.NewGuid();
        var command = new UpdateCategoryCommand(
            differentCategoryId,
            "Updated Category",
            "Updated Desc",
            null
        );

        // Act
        var result = await _controller.PutCategory(_senderMock.Object, categoryId, command);

        // Assert
        Assert.IsType<BadRequest>(result.Result);
    }

    [Fact]
    public async Task PutCategory_WhenIdEqualsParentCategoryId_ReturnsBadRequest()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var command = new UpdateCategoryCommand(
            categoryId,
            "Updated Category",
            "Updated Desc",
            categoryId
        );

        // Act
        var result = await _controller.PutCategory(_senderMock.Object, categoryId, command);

        // Assert
        Assert.IsType<BadRequest>(result.Result);
    }

    [Fact]
    public async Task PutCategory_WhenUpdateFailsInSender_ReturnsBadRequest()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var command = new UpdateCategoryCommand(
            categoryId,
            "Updated Category",
            "Updated Desc",
            null
        );

        _senderMock.Setup(s => s.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _controller.PutCategory(_senderMock.Object, categoryId, command);

        // Assert
        Assert.IsType<BadRequest>(result.Result);
    }

    [Fact]
    public async Task DeleteCategory_WhenCategoryExistsAndDeletionSucceeds_ReturnsNoContent()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        _senderMock
            .Setup(s =>
                s.Send(
                    It.Is<DeleteCategoryCommand>(cmd => cmd.Id == categoryId),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteCategory(_senderMock.Object, categoryId);

        // Assert
        Assert.IsType<NoContent>(result.Result);
    }

    [Fact]
    public async Task DeleteCategory_WhenDeletionFailsInSender_ReturnsBadRequest()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        _senderMock
            .Setup(s =>
                s.Send(
                    It.Is<DeleteCategoryCommand>(cmd => cmd.Id == categoryId),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteCategory(_senderMock.Object, categoryId);

        // Assert
        Assert.IsType<BadRequest>(result.Result);
    }
}
