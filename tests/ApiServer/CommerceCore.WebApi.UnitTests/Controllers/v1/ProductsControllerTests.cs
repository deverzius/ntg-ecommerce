using CommerceCore.Application.Brands.Dtos;
using CommerceCore.Application.Categories.Dtos;
using CommerceCore.Application.Common.Models;
using CommerceCore.Application.Products.Commands.CreateProduct;
using CommerceCore.Application.Products.Commands.CreateReview;
using CommerceCore.Application.Products.Commands.DeleteProduct;
using CommerceCore.Application.Products.Commands.UpdateProduct;
using CommerceCore.Application.Products.Dtos;
using CommerceCore.Application.Products.Queries.GetProduct;
using CommerceCore.Application.Products.Queries.GetProductsWithPagination;
using CommerceCore.Application.Products.Queries.GetReviewsByProductId;
using CommerceCore.WebApi.Controllers.v1;
using MediatR;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

public class ProductsControllerTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly ProductsController _controller;
    private const string StorageBaseUrl = "http://fake.supabase.co";
    private const string BucketName = "my-bucket";
    private readonly string _expectedPublicStorageUrl =
        $"{StorageBaseUrl}/storage/v1/object/public/{BucketName}/";

    public ProductsControllerTests()
    {
        _senderMock = new Mock<ISender>();
        _configurationMock = new Mock<IConfiguration>();

        // Setup IConfiguration mock
        _configurationMock.Setup(c => c["Supabase:StorageBaseUrl"]).Returns(StorageBaseUrl);
        _configurationMock.Setup(c => c["Supabase:BucketName"]).Returns(BucketName);

        _controller = new ProductsController(_configurationMock.Object);

        var httpContext = new DefaultHttpContext();
        _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
    }

    [Fact]
    public async Task GetProductsWithPagination_ReturnsOkWithPaginatedViewModel()
    {
        // Arrange
        var query = new GetProductsWithPaginationQuery(1, 10);
        var productDtos = new List<ProductResponseDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Product 2",
                Description = "Desc 2",
                Price = 20,
                BrandId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Brand = new SimpleBrandResponseDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Brand2",
                    Description = "",
                },
                Category = new SimpleCategoryResponseDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Category2",
                    Description = "",
                },
                Images =
                [
                    new SimpleProductImageResponseDto
                    {
                        Name = "img2.jpg",
                        Path = "img2.jpg",
                        ProductId = Guid.NewGuid(),
                    },
                ],
            },
        };
        var paginatedListDto = new PaginatedList<ProductResponseDto>(productDtos, 1, 1, 10);

        _senderMock
            .Setup(s => s.Send(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginatedListDto);

        // Act
        var result = await _controller.GetProductsWithPagination(_senderMock.Object, query);

        // Assert
        var okResult = Assert.IsType<Ok<PaginatedListViewModel<ProductViewModel>>>(result);
        Assert.NotNull(okResult.Value);
        Assert.Single(okResult.Value.Items);
        Assert.Equal(productDtos[0].Name, okResult.Value.Items.First().Name);
        Assert.Equal(
            $"{_expectedPublicStorageUrl}{productDtos[0].Images.First().Path}",
            okResult.Value.Items.First().Images.First().PublicUrl
        );
        Assert.Equal(paginatedListDto.PageNumber, okResult.Value.PageNumber);
    }

    [Fact]
    public async Task GetProduct_WhenProductExists_ReturnsOkWithProductAndReviewsViewModel()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var productDtos = new List<ProductResponseDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Product 2",
                Description = "Desc 2",
                Price = 20,
                BrandId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Brand = new SimpleBrandResponseDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Brand2",
                    Description = "",
                },
                Category = new SimpleCategoryResponseDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Category2",
                    Description = "",
                },
                Images =
                [
                    new SimpleProductImageResponseDto
                    {
                        Name = "img2.jpg",
                        Path = "img2.jpg",
                        ProductId = Guid.NewGuid(),
                    },
                ],
            },
        };
        var reviewDtos = new List<ReviewResponseDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Rating = 5,
                Title = "Good",
                Comment = "Great!",
                CreatedDate = DateTime.UtcNow,
            },
        };

        _senderMock
            .Setup(s =>
                s.Send(
                    It.Is<GetProductQuery>(q => q.Id == productId),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(productDtos[0]);
        _senderMock
            .Setup(s =>
                s.Send(
                    It.Is<GetReviewsByProductIdQuery>(q => q.ProductId == productId),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(reviewDtos);

        // Act
        var result = await _controller.GetProduct(_senderMock.Object, productId);

        // Assert
        var okResult = Assert.IsType<Ok<ProductWithReviewsViewModel>>(result.Result);
        Assert.NotNull(okResult.Value);
        Assert.NotNull(okResult.Value.Product);
        Assert.Equal(productDtos[0].Name, okResult.Value.Product.Name);
        Assert.Equal(
            $"{_expectedPublicStorageUrl}{productDtos[0].Images.First().Path}",
            okResult.Value.Product.Images.First().PublicUrl
        );
        Assert.Single(okResult.Value.Reviews);
        Assert.Equal(reviewDtos[0].Comment, okResult.Value.Reviews.First().Comment);
    }

    [Fact]
    public async Task GetProduct_WhenProductDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _senderMock
            .Setup(s =>
                s.Send(
                    It.Is<GetProductQuery>(q => q.Id == productId),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((ProductResponseDto?)null);

        // Act
        var result = await _controller.GetProduct(_senderMock.Object, productId);

        // Assert
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public async Task PostReview_WhenIdMatchesCommand_ReturnsOkWithReviewViewModel()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new CreateReviewCommand(
            4,
            "Good product",
            "user2",
            productId,
            null,
            null,
            null
        );
        var reviewDto = new ReviewResponseDto()
        {
            Id = Guid.NewGuid(),
            Rating = command.Rating,
            Title = command.Title,
            Comment = command.Comment,
            CreatedDate = DateTime.UtcNow,
        };

        _senderMock
            .Setup(s => s.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(reviewDto);

        // Act
        var result = await _controller.PostReview(_senderMock.Object, command, productId);

        // Assert
        var okResult = Assert.IsType<Ok<ReviewViewModel>>(result.Result);
        Assert.NotNull(okResult.Value);
        Assert.Equal(reviewDto.Comment, okResult.Value.Comment);
        Assert.Equal(reviewDto.Rating, okResult.Value.Rating);
    }

    [Fact]
    public async Task PostReview_WhenIdDoesNotMatchCommand_ReturnsBadRequest()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var differentProductId = Guid.NewGuid();
        var command = new CreateReviewCommand(
            4,
            "Good product",
            "user2",
            differentProductId,
            null,
            null,
            null
        );

        // Act
        var result = await _controller.PostReview(_senderMock.Object, command, productId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequest<string>>(result.Result);
        Assert.Equal("Product Id does not match.", badRequestResult.Value);
    }

    [Fact]
    public async Task PostReview_WhenSenderReturnsNull_ReturnsBadRequest()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new CreateReviewCommand(
            4,
            "Good product",
            "user2",
            productId,
            null,
            null,
            null
        );

        _senderMock
            .Setup(s => s.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ReviewResponseDto?)null);

        // Act
        var result = await _controller.PostReview(_senderMock.Object, command, productId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequest<string>>(result.Result);
        Assert.Equal("Failed to create review.", badRequestResult.Value);
    }

    [Fact]
    public async Task PostProduct_ReturnsCreatedWithProductViewModel()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new CreateProductCommand(
            "New Product",
            "New Desc",
            99.99m,
            Guid.NewGuid(),
            Guid.NewGuid()
        );
        var productDto = new ProductResponseDto
        {
            Id = productId,
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            BrandId = command.BrandId,
            CategoryId = command.CategoryId,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Brand = new SimpleBrandResponseDto
            {
                Id = command.BrandId,
                Name = "NewBrand",
                Description = "",
            },
            Category = new SimpleCategoryResponseDto
            {
                Id = command.CategoryId,
                Name = "NewCategory",
                Description = "",
            },
            Images = new List<SimpleProductImageResponseDto>
            {
                new SimpleProductImageResponseDto
                {
                    Name = "Image1",
                    Path = "image1.jpg",
                    ProductId = productId,
                },
            },
        };

        _senderMock
            .Setup(s => s.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(productDto);

        // Act
        var result = await _controller.PostProduct(_senderMock.Object, command);

        // Assert
        var createdResult = Assert.IsType<Created<ProductViewModel>>(result);
        Assert.NotNull(createdResult.Value);
        Assert.Equal(productDto.Name, createdResult.Value.Name);
        Assert.Equal(
            $"{_expectedPublicStorageUrl}{productDto.Images.First().Path}",
            createdResult.Value.Images.First().PublicUrl
        );
        Assert.Equal(nameof(_controller.GetProduct), createdResult.Location);
    }

    [Fact]
    public async Task PutProduct_WhenIdMatchesCommandAndProductExists_ReturnsNoContent()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new UpdateProductCommand(
            productId,
            "Updated Product",
            "Updated Desc",
            120.50m,
            Guid.NewGuid(),
            Guid.NewGuid(),
            []
        );

        _senderMock.Setup(s => s.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _controller.PutProduct(_senderMock.Object, productId, command);

        // Assert
        Assert.IsType<NoContent>(result.Result);
    }

    [Fact]
    public async Task PutProduct_WhenIdDoesNotMatchCommand_ReturnsBadRequest()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var differentProductId = Guid.NewGuid();
        var command = new UpdateProductCommand(
            differentProductId,
            "Updated Product",
            "Updated Desc",
            120.50m,
            Guid.NewGuid(),
            Guid.NewGuid(),
            []
        );

        // Act
        var result = await _controller.PutProduct(_senderMock.Object, productId, command);

        // Assert
        Assert.IsType<BadRequest>(result.Result);
    }

    [Fact]
    public async Task PutProduct_WhenProductNotFound_ReturnsNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new UpdateProductCommand(
            productId,
            "Updated Product",
            "Updated Desc",
            120.50m,
            Guid.NewGuid(),
            Guid.NewGuid(),
            []
        );

        _senderMock.Setup(s => s.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _controller.PutProduct(_senderMock.Object, productId, command);

        // Assert
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public async Task DeleteProduct_WhenProductExists_ReturnsNoContent()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _senderMock
            .Setup(s =>
                s.Send(
                    It.Is<DeleteProductCommand>(cmd => cmd.Id == productId),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteProduct(_senderMock.Object, productId);

        // Assert
        Assert.IsType<NoContent>(result.Result);
    }

    [Fact]
    public async Task DeleteProduct_WhenProductNotFound_ReturnsNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _senderMock
            .Setup(s =>
                s.Send(
                    It.Is<DeleteProductCommand>(cmd => cmd.Id == productId),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteProduct(_senderMock.Object, productId);

        // Assert
        Assert.IsType<NotFound>(result.Result);
    }
}
