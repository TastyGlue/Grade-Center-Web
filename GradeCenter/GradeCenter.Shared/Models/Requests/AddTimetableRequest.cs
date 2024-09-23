namespace GradeCenter.Shared.Models.Requests
{
    public class AddTimetableRequest
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public int TeacherId { get; set; }
        public string Year { get; set; } = default!;
        public DayOfWeek? Day { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
