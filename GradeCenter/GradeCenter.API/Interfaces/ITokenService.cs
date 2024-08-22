namespace GradeCenter.API.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(User user, string roleName);
        string GenerateRefreshToken();
        TokenContent? GetTokenContentFromAuthHeader(StringValues authHeader);
    }
}
