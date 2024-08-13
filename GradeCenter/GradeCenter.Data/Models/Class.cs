namespace GradeCenter.Data.Models
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = default!;

        [Required]
        public int Grade { get; set; }

        [Required]
        [StringLength(2)]
        public string Signature { get; set; } = default!;

        public int? ClassTeacherId { get; set; }
        [ForeignKey(nameof(ClassTeacherId))]
        public Teacher? ClassTeacher { get; set; }
    }
}
