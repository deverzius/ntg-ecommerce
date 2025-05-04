using CommerceCore.Application.Files.Dtos;
using MediatR;

namespace CommerceCore.Application.Files.Queries.GetFileUrls;

public record GetFileUrlsQuery(string[] FilePaths) : IRequest<FileUrlDto[]> { }
