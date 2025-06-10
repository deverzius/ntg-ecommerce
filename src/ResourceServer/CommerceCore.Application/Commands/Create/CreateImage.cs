using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Application.Common.Mappers;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Create;
using CommerceCore.Shared.DTOs.Responses;
using CommerceCore.Shared.Exceptions;
using MediatR;

namespace CommerceCore.Application.Commands.Create;

public record CreateImageCommand(
    CreateImageRequest Image,
    byte[] FileData
) : IRequest<ImageResponse>;

public class CreateImageCommandHandler(
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateImageCommand, ImageResponse>
{
    public async Task<ImageResponse> Handle(
        CreateImageCommand command,
        CancellationToken cancellationToken
    )
    {
        // TODO: handle later
        // if (command.Image.Data is not null) {}


        if (command.Image.Url is not null)
        {
            var image = new AppImage
            {
                Name = command.Image.Name,
                Url = command.Image.Url,
                Type = command.Image.Type,
                UploadedDate = DateTime.UtcNow,
                Tags = command.Image.Tags
            };

            await imageRepository.AddAsync(image, cancellationToken);
            await unitOfWork.SaveAsync(cancellationToken);

            return image.ToDto();
        }

        throw new AppException(400, "Data or url for image not found");
    }
}
