using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Files.Dtos;
using MediatR;

namespace CommerceCore.Application.Files.Commands.UploadFile;

public class UploadFileCommandHandler(IStorageService storageService)
    : IRequestHandler<UploadFileCommand, FileUrlDto?>
{
    private readonly IStorageService _storageService = storageService;

    public async Task<FileUrlDto?> Handle(
        UploadFileCommand request,
        CancellationToken cancellationToken
    )
    {
        return await _storageService.UploadFileAsync(
            request.Name,
            request.Data,
            request.ContentType
        );
    }
}