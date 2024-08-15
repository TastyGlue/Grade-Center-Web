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
        public ICollection<Admin> Admins { get; set; } = new List<Admin>();
    }
}
