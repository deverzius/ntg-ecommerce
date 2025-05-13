using CommerceCore.Application.Files.Dtos;
using MediatR;

namespace CommerceCore.Application.Files.Commands.UploadFile;

public record UploadFileCommand(string Name, byte[] Data, string ContentType)
    : IRequest<FileUrlDto?>
{
}