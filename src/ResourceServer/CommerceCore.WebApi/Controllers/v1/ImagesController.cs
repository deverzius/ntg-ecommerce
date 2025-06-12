using CommerceCore.Application.Commands.Create;
using CommerceCore.Shared.DTOs.Create;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebAPI.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class ImagesController : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ImageResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> PostImage(
        ISender sender,
        [FromForm] CreateImageRequest request,
        IFormFile? file
    )
    {
        var fileData = await file.ToByteArray();
        var command = new CreateImageCommand(request, fileData);

        var result = await sender.Send(command);

        // TODO: Create image retrieval endpoint
        // return CreatedAtAction("GetImage", new { id = result.Id }, result);
        return Created();
    }
}