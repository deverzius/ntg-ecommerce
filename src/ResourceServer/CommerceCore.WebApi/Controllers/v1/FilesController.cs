using CommerceCore.Application.Files.Commands.UploadFile;
using CommerceCore.Application.Files.Dtos;
using CommerceCore.Application.Files.Queries.GetFileUrl;
using CommerceCore.Application.Files.Queries.GetFileUrls;
using CommerceCore.Application.Files.Queries.GetPublicFileUrls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebAPI.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class FilesController : ControllerBase
{
    [HttpPost("sign/list")]
    [ProducesResponseType(typeof(FileUrlDto[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFileUrls(
        ISender sender,
        [FromBody] GetFileUrlsQuery query
    )
    {
        var result = await sender.Send(query);

        return Ok(result);
    }

    [HttpGet("public/list")]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(typeof(PublicUrlDto[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPublicFileUrls(
        ISender sender,
        [FromQuery] GetPublicFileUrlsQuery query
    )
    {
        var result = await sender.Send(query);

        return Ok(result);
    }

    [HttpGet("sign/{filePath}")]
    [ProducesResponseType(typeof(FileUrlDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFileUrl(ISender sender, string filePath)
    {
        var result = await sender.Send(new GetFileUrlQuery(filePath));

        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(FileUrlDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadFile(
        ISender sender,
        [FromForm] string name,
        IFormFile file
    )
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        var command = new UploadFileCommand(name, memoryStream.ToArray(), file.ContentType);

        var result = await sender.Send(command);

        if (result == null) return BadRequest("Failed to upload file.");

        return Created(nameof(GetFileUrl), result);
    }
}