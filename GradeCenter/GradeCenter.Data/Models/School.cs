namespace GradeCenter.Data.Models
{
    public class School
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = default!;

        [Required]
        [StringLength(150)]
        public string Address { get; set; } = default!;

        public ICollection<Headmaster> Headmasters { get; set; } = new List<Headmaster>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        public ICollection<Class> Classes { get; set; } = new List<Class>();
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
