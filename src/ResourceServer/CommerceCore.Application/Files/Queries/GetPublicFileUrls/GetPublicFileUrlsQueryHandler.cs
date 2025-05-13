using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Files.Dtos;
using MediatR;

namespace CommerceCore.Application.Files.Queries.GetPublicFileUrls;

public class GetPublicFileUrlsQueryHandler(IStorageService storageService)
    : IRequestHandler<GetPublicFileUrlsQuery, PublicUrlDto[]>
{
    private readonly IStorageService _storageService = storageService;

    public async Task<PublicUrlDto[]> Handle(
        GetPublicFileUrlsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _storageService.GetPublicFilesAsync(request.Limit, request.Offset);
    }
}