namespace GradeCenter.Data.Models
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; } = default!;

        [Required]
        public string Token { get; set; } = default!;

        [Required]
        public bool IsUsed { get; set; }

        [Required]
        public DateTime ExpireOn { get; set; }
    }
}
