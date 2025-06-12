namespace CommerceCore.Application.Common.Configurations;

public class SupabaseStorageConfigurations
{
    public required string ApiKey { get; set; }
    public required string BucketName { get; set; }
    public required string StorageBaseUrl { get; set; }
}