namespace GradeCenter.API.Interfaces
{
    public interface ITimetableService
    {
        Task<Response<int>> Add(AddTimetableRequest request);
        Task<Response<string>> Edit(TimetableDto timetableDto);
        Task<IEnumerable<TimetableDto>> GetAll(Guid? classId, int? teacherId);
        Task<TimetableDto?> GetById(int timetableId);
        Task<Response<string>> Delete(int timetableId);
    }
}
