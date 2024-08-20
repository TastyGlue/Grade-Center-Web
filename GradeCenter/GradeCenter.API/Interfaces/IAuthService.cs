namespace GradeCenter.API.Interfaces
{
    public interface IAuthService
    {
        Task<TokensResponse> Login(User user);
        Task<TokensResponse?> RefreshToken(string refreshToken);
    }
}
