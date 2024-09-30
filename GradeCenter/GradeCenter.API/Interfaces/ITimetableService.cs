namespace GradeCenter.API.Interfaces
{
    public interface ITimetableService
    {
        Task<CustomResult<Guid>> Add(AddTimetableRequest request);
        Task<CustomResult<string>> Edit(TimetableDto timetableDto);
        Task<IEnumerable<TimetableDto>> GetAll(Guid? classId, Guid? teacherId);
        Task<TimetableDto?> GetById(Guid timetableId);
        Task<CustomResult<string>> Delete(Guid timetableId);
    }
}
