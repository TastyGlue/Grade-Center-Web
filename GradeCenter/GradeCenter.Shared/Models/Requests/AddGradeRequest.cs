﻿namespace GradeCenter.Shared.Models.Requests
{
    public class AddGradeRequest
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }
        public string Result { get; set; } = default!;
        public DateTime? Date { get; set; }
    }
}
