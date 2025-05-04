using CommerceCore.Application.Files.Dtos;
using MediatR;

namespace CommerceCore.Application.Files.Queries.GetFileUrl;

public record GetFileUrlQuery(string FilePath) : IRequest<FileUrlDto?> { }
