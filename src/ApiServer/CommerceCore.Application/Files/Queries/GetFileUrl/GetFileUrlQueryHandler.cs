using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Files.Dtos;
using MediatR;

namespace CommerceCore.Application.Files.Queries.GetFileUrl;

public class GetFileUrlQueryHandler(IStorageService storageService)
    : IRequestHandler<GetFileUrlQuery, FileUrlDto?>
{
    private readonly IStorageService _storageService = storageService;

    public async Task<FileUrlDto?> Handle(
        GetFileUrlQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _storageService.GetFileAsync(request.FilePath);
    }
}
