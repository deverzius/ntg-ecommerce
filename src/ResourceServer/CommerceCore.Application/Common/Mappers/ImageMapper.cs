using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Mappers;

public static class ImageMapper
{
    public static ImageResponse ToDto(this AppImage image)
    {
        return new ImageResponse(
            image.Id,
            image.Name,
            image.Url,
            image.Type,
            image.UploadedDate,
            image.Tags
        );
    }
}
