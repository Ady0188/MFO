namespace MFO.Application.Authentication;

public sealed record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName);