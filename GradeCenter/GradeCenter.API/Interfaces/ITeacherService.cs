namespace GradeCenter.API.Interfaces
{
    public interface ITeacherService
    {
        Task<CustomResult<Guid>> AddTeacher(AddTeacherRequest request);
        Task<CustomResult<string>> Edit(TeacherDto teacherDto);
        Task<IEnumerable<TeacherDto>> GetAll(Guid? schoolId);
        Task<TeacherDto?> GetById(Guid id);
    }
}
