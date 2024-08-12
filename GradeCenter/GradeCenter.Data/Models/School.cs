﻿using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
