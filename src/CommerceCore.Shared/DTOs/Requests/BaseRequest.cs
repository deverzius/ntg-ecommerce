namespace CommerceCore.Shared.DTOs.Requests;

public record BaseRequest(
    int Offset,
    int Limit,
    string Sort,
    bool Asc
);
