namespace GradeCenter.Shared.Models
{
    public class JwtSettings
    {
        public string SecurityKey { get; set; } = default!;
        public int TokenExpirationInMinutes { get; set; }
        public int RefreshTokenExpirationInDays { get; set; }
    }
}
