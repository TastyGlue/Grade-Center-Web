namespace GradeCenter.Shared.Models.Requests
{
    public class AddAbsenceRequest
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TimetableId { get; set; }
        public DateOnly Date { get; set; }
    }
}
