namespace GradeCenter.API.Interfaces
{
    public interface ITimetableService
    {
        Task<Response<Guid>> Add(AddTimetableRequest request);
        Task<Response<string>> Edit(TimetableDto timetableDto);
        Task<IEnumerable<TimetableDto>> GetAll(Guid? classId, Guid? teacherId);
        Task<TimetableDto?> GetById(Guid timetableId);
        Task<Response<string>> Delete(Guid timetableId);
    }
}
