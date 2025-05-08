using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Web;
using Ardalis.GuardClauses;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Files.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace CommerceCore.Infrastructure.Services;

public class SupabaseStorageService(
    HttpClient httpClient,
    IConfiguration config,
    ILogger<SupabaseStorageService> logger
) : IStorageService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _storageBaseUrl =
        config["Supabase:StorageBaseUrl"]
        ?? Guard.Against.NullOrEmpty(config["Supabase:StorageBaseUrl"]);
    private readonly string _apiKey =
        config["Supabase:ApiKey"] ?? Guard.Against.NullOrEmpty(config["Supabase:ApiKey"]);
    private readonly string _bucketName =
        config["Supabase:BucketName"] ?? Guard.Against.NullOrEmpty(config["Supabase:BucketName"]);
    private readonly ILogger<SupabaseStorageService> _logger = logger;

    public async Task<FileUrlDto?> UploadFileAsync(
        string fileName,
        byte[] fileData,
        string contentType
    )
    {
        var filePath = "images/" + fileName;

        using var content = new ByteArrayContent(fileData);
        content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_storageBaseUrl}/storage/v1/object/{_bucketName}/{filePath}"
        )
        {
            Content = content,
        };

        request.Headers.Add("Authorization", $"Bearer {_apiKey}");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            _logger.LogError("Error when upload file: {errorMessage}", errorMessage);
        }

        return response.IsSuccessStatusCode ? await GetFileAsync(filePath) : null;
    }

    public async Task<FileUrlDto?> GetFileAsync(string filePath)
    {
        var body = new { expiresIn = 3600 };
        var json = JsonSerializer.Serialize(body);

        filePath = HttpUtility.UrlDecode(filePath);

        var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_storageBaseUrl}/storage/v1/object/sign/{_bucketName}/{filePath}"
        )
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
        };

        request.Headers.Add("Authorization", $"Bearer {_apiKey}");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadAsStringAsync();
            _logger.LogError("Error when get file: {errorResponseContent}", errorResponseContent);
        }

        try
        {
            FileUrlDto? imageUrlDto = await response.Content.ReadFromJsonAsync<FileUrlDto>();
            // Supabase does not return file path when retrieve single file
            imageUrlDto.Path = filePath;
            return imageUrlDto;
        }
        catch
        {
            return null;
        }
    }

    public async Task<FileUrlDto[]> GetFilesAsync(string[] filePaths)
    {
        var body = new { paths = filePaths, expiresIn = 3600 };
        var json = JsonSerializer.Serialize(body);

        var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_storageBaseUrl}/storage/v1/object/sign/{_bucketName}"
        )
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
        };

        request.Headers.Add("Authorization", $"Bearer {_apiKey}");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadAsStringAsync();
            _logger.LogError("Error when get files: {errorResponseContent}", errorResponseContent);
        }

        try
        {
            FileUrlDto[] imageUrlDtos =
                await response.Content.ReadFromJsonAsync<FileUrlDto[]>() ?? [];
            return imageUrlDtos;
        }
        catch
        {
            return [];
        }
    }

    public async Task<PublicUrlDto[]> GetPublicFilesAsync(int limit, int offset)
    {
        var body = new
        {
            prefix = "images",
            limit,
            offset,
        };
        var json = JsonSerializer.Serialize(body);

        var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_storageBaseUrl}/storage/v1/object/list/{_bucketName}"
        )
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
        };

        request.Headers.Add("Authorization", $"Bearer {_apiKey}");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadAsStringAsync();
            _logger.LogError("Error when get files: {errorResponseContent}", errorResponseContent);
        }

        try
        {
            var content = await response.Content.ReadAsStringAsync();
            var fileNameList = new List<string>();
            JArray fileListJson = JArray.Parse(content);

            foreach (var file in fileListJson)
            {
                if (file["name"]?.ToString() != ".emptyFolderPlaceholder")
                {
                    fileNameList.Add(file["name"]?.ToString());
                }
            }

            return
            [
                .. fileNameList.Select(fn => new PublicUrlDto
                {
                    Path = "images/" + fn,
                    PublicUrl =
                        $"{_storageBaseUrl}/storage/v1/object/public/{_bucketName}/images/{fn}",
                }),
            ];
        }
        catch
        {
            return [];
        }
    }
}
