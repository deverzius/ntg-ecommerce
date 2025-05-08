using CommerceCore.Application.Files.Dtos;

namespace CommerceCore.Application.Common.Interfaces;

public interface IStorageService
{
    Task<FileUrlDto?> UploadFileAsync(string fileName, byte[] fileData, string contentType);
    Task<FileUrlDto?> GetFileAsync(string filePath);
    Task<FileUrlDto[]> GetFilesAsync(string[] filePaths);
    Task<PublicUrlDto[]> GetPublicFilesAsync(int limit, int offset);
}
