using CommerceCore.Application.Files.Commands.UploadFile;
using CommerceCore.Application.Files.Dtos;
using CommerceCore.Application.Files.Queries.GetFileUrl;
using CommerceCore.Application.Files.Queries.GetFileUrls;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class FilesController() : ControllerBase
{
    [HttpPost("list")]
    [ProducesResponseType(typeof(Ok<FileUrlDto>), StatusCodes.Status200OK)]
    public async Task<Ok<FileUrlDto[]>> GetFileUrls(
        ISender sender,
        [FromBody] GetFileUrlsQuery query
    )
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    [HttpGet("{filePath}")]
    [ProducesResponseType(typeof(Ok<FileUrlDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Results<Ok<FileUrlDto>, NotFound>> GetFileUrl(ISender sender, string filePath)
    {
        var result = await sender.Send(new GetFileUrlQuery(filePath));

        return result == null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(Created<FileUrlDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Results<Created<FileUrlDto>, BadRequest<string>>> UploadFile(
        ISender sender,
        [FromForm] string name,
        IFormFile file
    )
    {
        if (file == null || file.Length == 0)
            return TypedResults.BadRequest("No file uploaded.");

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        var command = new UploadFileCommand(name, memoryStream.ToArray(), file.ContentType);

        var result = await sender.Send(command);

        if (result == null)
        {
            return TypedResults.BadRequest("Failed to upload file.");
        }

        return TypedResults.Created(nameof(GetFileUrl), result);
    }
}
