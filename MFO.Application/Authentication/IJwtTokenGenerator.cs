namespace MFO.Application.Authentication;

public interface IJwtTokenGenerator
{
    AuthResponse CreateToken(Guid userId, string email, string firstName, string lastName);
}