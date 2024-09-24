namespace GradeCenter.API.Interfaces
{
    public interface IStudentService
    {
        Task<Response<Guid>> AddStudent(AddStudentRequest request);
        Task<Response<string>> Edit(StudentDto studentDto);
        Task<IEnumerable<StudentDto>> GetAll(Guid? schoolId);
        Task<StudentDto?> GetById(Guid id);
    }
}
