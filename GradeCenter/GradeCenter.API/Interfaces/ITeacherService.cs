namespace GradeCenter.API.Interfaces
{
    public interface ITeacherService
    {
        Task<Response<Guid>> AddTeacher(AddTeacherRequest request);
        Task<Response<string>> Edit(TeacherDto teacherDto);
        Task<IEnumerable<TeacherDto>> GetAll(Guid? schoolId);
        Task<TeacherDto?> GetById(Guid id);
    }
}
