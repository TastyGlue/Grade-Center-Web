﻿namespace GradeCenter.Shared.Models.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Picture { get; set; }
        public bool IsActive { get; set; }
    }
}
