namespace GradeCenter.Shared.Models.Responses
{
    public class TokensResponse
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
