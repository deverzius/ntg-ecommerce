using CommerceCore.Application.Files.Dtos;
using MediatR;

namespace CommerceCore.Application.Files.Queries.GetPublicFileUrls;

public record GetPublicFileUrlsQuery(int Limit, int Offset) : IRequest<PublicUrlDto[]>
{
}