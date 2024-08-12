using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeCenter.Data.Models
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string StudentId { get; set; } = default!;
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = default!;

        [Required]
        public string SubjectId { get; set; } = default!;
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = default!;

        [Required]
        public string TeacherId { get; set; } = default!;
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; } = default!;

        [Required]
        public string Result { get; set; } = default!;

        [Required]
        public DateTime Date { get; set; }
    }
}
