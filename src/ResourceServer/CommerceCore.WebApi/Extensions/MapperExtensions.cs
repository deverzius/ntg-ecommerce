namespace CommerceCore.WebApi.Extensions;

public static class MapperExtensions
{
    public static async Task<byte[]> ToByteArray(this IFormFile? file)
    {
        if (file is null) return [];

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        return memoryStream.ToArray();
    }
}