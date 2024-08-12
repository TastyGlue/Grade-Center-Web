namespace GradeCenter.Data.Models
{
    public class Parent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = default!;
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = default!;

        public ICollection<StudentParent> StudentParents { get; set; } = new List<StudentParent>();
    }
}
