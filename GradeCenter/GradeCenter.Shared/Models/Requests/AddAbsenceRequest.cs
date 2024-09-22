namespace GradeCenter.Shared.Models.Requests
{
    public class AddAbsenceRequest
    {
        public int StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public int TimetableId { get; set; }
        public DateOnly Date { get; set; }
    }
}
