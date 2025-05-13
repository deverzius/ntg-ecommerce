using System.Text.Json;

namespace CommerceCore.Web.CustomersSite.Shared.Helpers;

public static class JsonHelper
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = false
    };
}