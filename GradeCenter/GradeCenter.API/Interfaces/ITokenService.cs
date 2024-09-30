namespace GradeCenter.API.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, IList<string> roles);
        string GenerateRefreshToken();
        TokenContent? GetTokenContentFromAuthHeader(StringValues authHeader);
    }
}
