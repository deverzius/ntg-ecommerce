using System.Text;
using CommerceCore.Application.Files.Commands.UploadFile;
using CommerceCore.Application.Files.Dtos;
using CommerceCore.Application.Files.Queries.GetFileUrl;
using CommerceCore.Application.Files.Queries.GetFileUrls;
using CommerceCore.WebApi.Controllers.v1;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class FilesControllerTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly FilesController _controller;

    public FilesControllerTests()
    {
        _senderMock = new Mock<ISender>();
        _controller = new FilesController();

        var httpContext = new DefaultHttpContext();
        _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
    }

    [Fact]
    public async Task GetFileUrls_ReturnsOkWithFileUrlDtoArray()
    {
        // Arrange
        var filePaths = new[] { "path/to/file1.jpg", "path/to/file2.png" };
        var query = new GetFileUrlsQuery(filePaths);
        var expectedUrls = new[]
        {
            new FileUrlDto
            {
                Path = "path/to/file1.jpg",
                SignedURL = "http://example.com/signed/file1.jpg",
            },
            new FileUrlDto
            {
                Path = "path/to/file2.png",
                SignedURL = "http://example.com/signed/file2.png",
            },
        };

        _senderMock
            .Setup(s => s.Send(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedUrls);

        // Act
        var result = await _controller.GetFileUrls(_senderMock.Object, query);

        // Assert
        var okResult = Assert.IsType<Ok<FileUrlDto[]>>(result);
        Assert.NotNull(okResult.Value);
        Assert.Equal(expectedUrls.Length, okResult.Value.Length);
        Assert.Equal(expectedUrls[0].SignedURL, okResult.Value[0].SignedURL);
        Assert.Equal(expectedUrls[0].Path, okResult.Value[0].Path);
        Assert.Equal(expectedUrls[1].SignedURL, okResult.Value[1].SignedURL);
        Assert.Equal(expectedUrls[1].Path, okResult.Value[1].Path);
    }

    [Fact]
    public async Task GetFileUrl_WhenFileExists_ReturnsOkWithFileUrlDto()
    {
        // Arrange
        var filePath = "test/image.png";
        var query = new GetFileUrlQuery(filePath);
        var expectedUrl = new FileUrlDto
        {
            Path = filePath,
            SignedURL = "http://example.com/signed/image.png",
        };

        _senderMock
            .Setup(s => s.Send(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedUrl);

        // Act
        var result = await _controller.GetFileUrl(_senderMock.Object, filePath);

        // Assert
        var okResult = Assert.IsType<Ok<FileUrlDto>>(result.Result);
        Assert.NotNull(okResult.Value);
        Assert.Equal(expectedUrl.SignedURL, okResult.Value.SignedURL);
        Assert.Equal(expectedUrl.Path, okResult.Value.Path);
    }

    [Fact]
    public async Task GetFileUrl_WhenFileDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var filePath = "nonexistent/file.txt";
        var query = new GetFileUrlQuery(filePath);

        _senderMock
            .Setup(s => s.Send(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync((FileUrlDto)null!);

        // Act
        var result = await _controller.GetFileUrl(_senderMock.Object, filePath);

        // Assert
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public async Task UploadFile_WithValidFile_ReturnsCreatedWithFileUrlDto()
    {
        // Arrange
        var formFileName = "upload.txt";
        var actualFileNameInFile = "test-upload.txt";
        var fileContent = "This is a test file.";
        var fileContentType = "text/plain";
        var fileBytes = Encoding.UTF8.GetBytes(fileContent);
        var memoryStream = new MemoryStream(fileBytes);

        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.FileName).Returns(actualFileNameInFile); // IFormFile.FileName
        mockFile.Setup(f => f.Length).Returns(memoryStream.Length);
        mockFile.Setup(f => f.ContentType).Returns(fileContentType);
        mockFile
            .Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .Callback<Stream, CancellationToken>((stream, token) => memoryStream.CopyTo(stream))
            .Returns(Task.CompletedTask);

        var expectedDto = new FileUrlDto
        {
            Path = formFileName,
            SignedURL = $"http://example.com/signed/{formFileName}",
        };

        _senderMock
            .Setup(s =>
                s.Send(
                    It.Is<UploadFileCommand>(cmd =>
                        cmd.Name == formFileName
                        && cmd.ContentType == fileContentType
                        && cmd.Data.SequenceEqual(fileBytes)
                    ),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(expectedDto);

        // Act
        var result = await _controller.UploadFile(
            _senderMock.Object,
            formFileName,
            mockFile.Object
        );

        // Assert
        var createdResult = Assert.IsType<Created<FileUrlDto>>(result.Result);
        Assert.NotNull(createdResult.Value);
        Assert.Equal(expectedDto.SignedURL, createdResult.Value.SignedURL);
        Assert.Equal(expectedDto.Path, createdResult.Value.Path); // Path should match the 'name' parameter if handler sets it that way
        Assert.Equal(nameof(_controller.GetFileUrl), createdResult.Location);
        mockFile.Verify(
            f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task UploadFile_WhenFileIsNull_ReturnsBadRequest()
    {
        // Arrange
        var formFileName = "test-upload.txt";

        // Act
        var result = await _controller.UploadFile(_senderMock.Object, formFileName, null!);

        // Assert
        var badRequestResult = Assert.IsType<BadRequest<string>>(result.Result);
        Assert.Equal("No file uploaded.", badRequestResult.Value);
    }

    [Fact]
    public async Task UploadFile_WhenFileIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        var formFileName = "empty-file.txt";
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.FileName).Returns("some-actual-name.txt");
        mockFile.Setup(f => f.Length).Returns(0);

        // Act
        var result = await _controller.UploadFile(
            _senderMock.Object,
            formFileName,
            mockFile.Object
        );

        // Assert
        var badRequestResult = Assert.IsType<BadRequest<string>>(result.Result);
        Assert.Equal("No file uploaded.", badRequestResult.Value);
    }

    [Fact]
    public async Task UploadFile_WhenSenderReturnsNull_ReturnsBadRequest()
    {
        // Arrange
        var formFileName = "test-upload.txt";
        var actualFileNameInFile = "test-upload.txt";
        var fileContent = "This is a test file.";
        var fileContentType = "text/plain";
        var fileBytes = Encoding.UTF8.GetBytes(fileContent);
        var memoryStream = new MemoryStream(fileBytes);

        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.FileName).Returns(actualFileNameInFile);
        mockFile.Setup(f => f.Length).Returns(memoryStream.Length);
        mockFile.Setup(f => f.ContentType).Returns(fileContentType);
        mockFile
            .Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .Callback<Stream, CancellationToken>((stream, token) => memoryStream.CopyTo(stream))
            .Returns(Task.CompletedTask);

        _senderMock
            .Setup(s =>
                s.Send(
                    It.Is<UploadFileCommand>(cmd =>
                        cmd.Name == formFileName
                        && cmd.ContentType == fileContentType
                        && cmd.Data.SequenceEqual(fileBytes)
                    ),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((FileUrlDto)null!);

        // Act
        var result = await _controller.UploadFile(
            _senderMock.Object,
            formFileName,
            mockFile.Object
        );

        // Assert
        var badRequestResult = Assert.IsType<BadRequest<string>>(result.Result);
        Assert.Equal("Failed to upload file.", badRequestResult.Value);
    }
}
