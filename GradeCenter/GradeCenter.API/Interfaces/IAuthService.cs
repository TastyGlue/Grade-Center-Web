namespace GradeCenter.API.Interfaces
{
    public interface IAuthService
    {
        Task<TokensResponse> Login(User user, string? role = null);
        Task<TokensResponse?> RefreshToken(string refreshToken);
    }
}
