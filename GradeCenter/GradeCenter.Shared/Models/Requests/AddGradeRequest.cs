namespace GradeCenter.Shared.Models.Requests
{
    public class AddGradeRequest
    {
        public int StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public int TeacherId { get; set; }
        public string Result { get; set; } = default!;
        public DateTime? Date { get; set; }
    }
}
