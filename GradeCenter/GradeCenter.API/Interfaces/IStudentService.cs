namespace GradeCenter.API.Interfaces
{
    public interface IStudentService
    {
        Task<Response<int>> AddStudent(AddStudentRequest request);
        Task<Response<string>> Edit(int studentId, StudentDto studentDto);
        Task<IEnumerable<StudentDto>> GetAll(string? schoolId);
        Task<StudentDto?> GetById(int id);
    }
}
