using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Files.Dtos;
using MediatR;

namespace CommerceCore.Application.Files.Queries.GetFileUrls;

public class GetFileUrlsQueryHandler(IStorageService storageService)
    : IRequestHandler<GetFileUrlsQuery, FileUrlDto[]>
{
    private readonly IStorageService _storageService = storageService;

    public async Task<FileUrlDto[]> Handle(
        GetFileUrlsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _storageService.GetFilesAsync(request.FilePaths);
    }
}