namespace GradeCenter.API.Interfaces
{
    public interface ITeacherService
    {
        Task<Response<int>> AddTeacher(AddTeacherRequest request);
        Task<Response<string>> Edit(TeacherDto teacherDto);
        Task<IEnumerable<TeacherDto>> GetAll(string? schoolId);
        Task<TeacherDto?> GetById(int id);
    }
}
